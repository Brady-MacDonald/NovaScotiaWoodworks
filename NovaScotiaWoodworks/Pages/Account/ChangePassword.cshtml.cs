using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovaScotiaWoodworks.AccountManager;
using NovaScotiaWoodworks.Data;
using NovaScotiaWoodworks.Models;

namespace NovaScotiaWoodworks.Pages.Account
{
    [Authorize]
    public class ChangePasswordModel : PageModel
    {
        [BindProperty]
        public PasswordChangeModel ChangePassword { get; set; }
        public UserModel CurrentUser { get; set; }
        private readonly ApplicationDbContext _db;
        private readonly INotyfService _notyf;
        private readonly int _saltSize = 32;
        public ChangePasswordModel(ApplicationDbContext db, INotyfService notyf)
        {
            _db = db;
            _notyf = notyf;
            CurrentUser = new UserModel();
            ChangePassword = new PasswordChangeModel();
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //Get the users currently stored password hash
            CurrentUser = _db.Users.Find(User.Identity.Name);

            //Convert the user entered password into its hash
            string currentPasswordHash = PasswordHash.GetStringSha256Hash(ChangePassword.CurrentPassword, CurrentUser.Salt);

            if(CurrentUser.Password != currentPasswordHash)
            {
                ModelState.AddModelError("PasswordError", "Incorrect password");
                return Page();
            }

            if (ChangePassword.NewPassword != ChangePassword.ConfirmPassword)
            {
                ModelState.AddModelError("PasswordError", "Passwords did not match");
                return Page();
            }

            //User has confirmed old password and entered matching new password
            //Proceed with updating the stored password in db
            string newSalt = PasswordHash.CreateSalt(_saltSize);
            string newPasswordHash = PasswordHash.GetStringSha256Hash(ChangePassword.NewPassword, newSalt);
            CurrentUser.Password = newPasswordHash;
            CurrentUser.Salt = newSalt;
            try
            {
                _db.Users.Update(CurrentUser);
                _db.SaveChanges();
            }
            catch
            {
                _notyf.Error("Unable to update");
                return Page();
            }
            _notyf.Success("Password Updated");
            return Redirect("/Index");
        }
    }
}
