using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovaScotiaWoodworks.AccountManager;
using NovaScotiaWoodworks.Data;
using NovaScotiaWoodworks.Models;

namespace NovaScotiaWoodworks.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly INotyfService _notyf;


        [BindProperty]
        public UserModel CurrentUser { get; set; }

        public RegisterModel(ApplicationDbContext db, INotyfService notyf)
        {
            _db = db;
            _notyf = notyf;
            CurrentUser = new UserModel();
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid input");
                return Page();
            }

            string hashedPassword = PasswordHash.GetStringSha256Hash(CurrentUser.Password);
            CurrentUser.Password = hashedPassword;

            try
            {
                _db.Users.Add(CurrentUser);
                _db.SaveChanges();
            }
            catch
            {
                ModelState.AddModelError("DuplicateUser", "That username is already in use by another user!");
                return Page();
            }
            _notyf.Success("Account Created");
            return Redirect("/Account/Login");
        }
    }
}
