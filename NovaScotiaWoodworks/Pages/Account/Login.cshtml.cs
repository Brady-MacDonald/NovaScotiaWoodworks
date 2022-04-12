using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovaScotiaWoodworks.Data;
using NovaScotiaWoodworks.Models;
using System.Collections.Generic;
using System.Security.Claims;
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

            UserModel currentUser = _db.Users.Find(1);

            if(currentUser == null)
                //Unable to locate user account
                return Redirect("/Privacy");

            //Verify the credentials
            if (CurrentUser.Username == currentUser.Username && CurrentUser.Password == currentUser.Password)
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
