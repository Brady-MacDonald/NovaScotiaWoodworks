using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovaScotiaWoodworks.AccountManager;
using NovaScotiaWoodworks.Data;
using NovaScotiaWoodworks.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NovaScotiaWoodworks.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly INotyfService _notyf;

        [BindProperty]
        public UserModel CurrentUser { get; set; }

        public LoginModel(ApplicationDbContext db, INotyfService notyf)
        {
            _db = db;
            _notyf = notyf;
            CurrentUser = new UserModel();
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) 
            {
                ModelState.AddModelError("AccountError", "Invalid input");
                return Page();
            }

            UserModel dbUser = _db.Users.Find(CurrentUser.Username);

            if (dbUser == null)
            {
                //Unable to locate user account
                ModelState.AddModelError("AccountError", "Unable to locate account");
                return Page();
            }

            string hashedPassword = PasswordHash.GetStringSha256Hash(CurrentUser.Password);

            //Verify the credentials
            if (CurrentUser.Username == dbUser.Username && hashedPassword == dbUser.Password)
            {
                ClaimsPrincipal claimsPrincipal;

                if (CurrentUser.Username == "admin")
                {   //Apply admin credentials
                    claimsPrincipal = CreateClaims("true");
                }
                else {claimsPrincipal = CreateClaims("false");}

                var authProperties = new AuthenticationProperties
                {
                    //Configures if user should stay signed in on browser close
                    IsPersistent = CurrentUser.RememberMe
                };
                //Uses the IAuthenticationService interface and allow asp.net to implement interface through DI
                await HttpContext.SignInAsync("AuthenticationCookie", claimsPrincipal, authProperties);
                _notyf.Success("Signed In");
                return Redirect("/Index");
            }
            ModelState.AddModelError("AccountError", "Incorrect password");
            return Page();
        }

        /// <summary>
        /// Create security context
        /// </summary>
        /// <param name="admin">Determines is user receives admin authorization</param>
        /// <returns></returns>
        public ClaimsPrincipal CreateClaims(string admin)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, CurrentUser.Username),
                new Claim("Admin", admin),
            };
            
            var identity = new ClaimsIdentity(claims, "AuthenticationCookie");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            return claimsPrincipal;
        }
    }
}