using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovaScotiaWoodworks.AccountManager;
using NovaScotiaWoodworks.Data;
using NovaScotiaWoodworks.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace NovaScotiaWoodworks.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public UserModel CurrentUser { get; set; }

        public LoginModel(ApplicationDbContext db)
        {
            _db = db;
            CurrentUser = new UserModel();
            //CurrentUser.Username = "test";
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) 
            {
                ModelState.AddModelError("NoAccount", "Invalid input");
                return Page();
            }
            

            UserModel dbUser = _db.Users.Find(CurrentUser.Username);

            if (dbUser == null)
            {
                //Unable to locate user account
                ModelState.AddModelError("NoAccount", "Unable to locate account");
                return Page();
            }

            string hashedPassword = PasswordHash.GetStringSha256Hash(CurrentUser.Password);

            //Verify the credentials
            if (CurrentUser.Username == dbUser.Username && hashedPassword == dbUser.Password)
            {
                ClaimsPrincipal claimsPrincipal;

                //Apply admin credentials
                if (CurrentUser.Username == "admin")
                {
                    claimsPrincipal = CreateClaims("true");
                }
                else
                {
                    claimsPrincipal = CreateClaims("false");
                }
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = CurrentUser.RememberMe
                };
                //Uses the IAuthenticationService interface
                //We must implement the event handler 
                //We let asp.net implement the iterface and use the dependency injection
                await HttpContext.SignInAsync("AuthenticationCookie", claimsPrincipal, authProperties);
                
                return Redirect("/Index");
            }
            ModelState.AddModelError("NoAccount", "Incorrect username or password");
            return Page();
        }
        
        public ClaimsPrincipal CreateClaims(string admin)
        {
            //Create security context
            //We just need the primary identity
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, CurrentUser.Username),
                    new Claim("AccountType", "Business"),
                    new Claim("Admin", admin),
                };
            var identity = new ClaimsIdentity(claims, "AuthenticationCookie");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            return claimsPrincipal;
        }
    }
}
