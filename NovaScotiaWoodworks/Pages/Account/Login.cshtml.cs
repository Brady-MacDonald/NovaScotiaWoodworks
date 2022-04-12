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
            CurrentUser = new UserModel();
            _db = db;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            UserModel dbUser = _db.Users.Find(2);
            if (dbUser == null)
                //Unable to locate user account
                return Redirect("/Privacy");

            string hashedPassword = PasswordHash.GetStringSha256Hash(CurrentUser.Password);

            //Verify the credentials
            if (CurrentUser.Username == dbUser.Username && hashedPassword == dbUser.Password)
            {
                //Create security context
                //We just need the primary identity
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim("AccountType", "Business"),
                    new Claim("Admin", "true"),
                };
                var identity = new ClaimsIdentity(claims, "AuthenticationCookie");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);    

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

            return Page();
        }
    }
}
