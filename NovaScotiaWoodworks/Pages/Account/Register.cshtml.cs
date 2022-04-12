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

        [BindProperty]
        public UserModel CurrentUser { get; set; }

        public RegisterModel(ApplicationDbContext db)
        {
            //Get db context from the dependency injection
            _db = db;
            CurrentUser = new UserModel();
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Redirect("/Privacy");

            string hashedPassword = PasswordHash.GetStringSha256Hash(CurrentUser.Password);
            CurrentUser.Password = hashedPassword;

            _db.Users.Add(CurrentUser);
            _db.SaveChanges();
            return Redirect("/Index");
        }
    }
}
