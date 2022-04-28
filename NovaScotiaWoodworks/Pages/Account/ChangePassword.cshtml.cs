using AspNetCoreHero.ToastNotification.Abstractions;
using DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovaScotiaWoodworks.AccountManager;
using NovaScotiaWoodworks.Models;
using UserModel = DataAccess.Models.UserModel;
using System.Threading.Tasks;

namespace NovaScotiaWoodworks.Pages.Account
{
    [Authorize]
    public class ChangePasswordModel : PageModel
    {
        [BindProperty]
        public PasswordChangeModel ChangePassword { get; set; }
        public UserModel CurrentUser { get; set; }
        private readonly IUserData _data;
        private readonly INotyfService _notyf;
        private readonly int _saltSize = 32;
        
        public ChangePasswordModel(IUserData data, INotyfService notyf)
        {
            _data= data;
            _notyf = notyf;
            CurrentUser = new UserModel();
            ChangePassword = new PasswordChangeModel();
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //Get the users currently stored password hash
            CurrentUser = await _data.GetUser(User.Identity.Name);

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
                await _data.UpdateUser(CurrentUser);
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
