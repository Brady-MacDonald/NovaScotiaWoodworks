using AspNetCoreHero.ToastNotification.Abstractions;
using DataAccess.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
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
        [BindProperty]
        public UserModel CurrentUser { get; set; }
        private readonly IConfiguration _config;
        private readonly IUserData _data;
        private readonly INotyfService _notyf;

        public LoginModel(INotyfService notyf, IConfiguration config, IUserData data)
        {
            _data = data;
            _notyf = notyf;
            _config = config;
            CurrentUser = new UserModel();
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) 
            {
                return Page();
            }

            DataAccess.Models.UserModel dbUser = new DataAccess.Models.UserModel();

            //UserModel dbUser = _db.Users.Find(CurrentUser.Username);
            dbUser = await _data.GetUser(CurrentUser.Username);

            if (dbUser == null)
            {
                //Unable to locate user account
                ModelState.AddModelError("AccountError", "Unable to locate account");
                return Page();
            }

            //Get password hash with salt
            string hashedPassword = PasswordHash.GetStringSha256Hash(CurrentUser.Password, dbUser.Salt);

            //Verify the credentials
            if (CurrentUser.Username == dbUser.UserName && hashedPassword == dbUser.Password)
            {
                ClaimsPrincipal claimsPrincipal;

                if (CurrentUser.Username == "admin")
                {   //Apply admin credentials
                    claimsPrincipal = CreateClaims("true");
                }
                else { claimsPrincipal = CreateClaims("false"); }

                var authProperties = new AuthenticationProperties
                {
                    //Configures if user should stay signed in on browser close
                    IsPersistent = CurrentUser.RememberMe
                };
                //Uses the IAuthenticationService interface and allow asp.net to implement interface through DI
                await HttpContext.SignInAsync(_config["Cookie"], claimsPrincipal, authProperties);
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
            
            var identity = new ClaimsIdentity(claims, _config["Cookie"]);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            return claimsPrincipal;
        }
    }
}