using AspNetCoreHero.ToastNotification.Abstractions;
using DataAccess.Data;
using DataAccess.DbAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NovaScotiaWoodworks.AccountManager;
using NovaScotiaWoodworks.Models;
using System;
using System.Threading.Tasks;

namespace NovaScotiaWoodworks.Pages.Account
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public CreateUserModel CurrentUser { get; set; }
        private readonly INotyfService _notyf;
        private readonly IUserData _data;
        private readonly ILogger<RegisterModel> _logger;
        private readonly int _saltSize = 32;
        public RegisterModel(INotyfService notyf, IUserData data, ILogger<RegisterModel> logger)
        {
            _notyf = notyf;
            _data = data;
            _logger = logger;
            CurrentUser = new CreateUserModel();
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            string salt = PasswordHash.CreateSalt(_saltSize);
            string hashedPassword = PasswordHash.GetStringSha256Hash(CurrentUser.Password, salt);
            CurrentUser.Salt = salt;
            CurrentUser.Password = hashedPassword;

            DataAccess.Models.UserModel user = new DataAccess.Models.UserModel
            {
        
                FirstName = CurrentUser.FirstName,
                LastName = CurrentUser.LastName,
                UserName = CurrentUser.Username,
                EmailAddress = CurrentUser.EmailAddress,
                Password = CurrentUser.Password,
                Salt = salt,
            };

            try
            {
                await _data.InsertUser(user);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("RegisterError", "Error: " + ex.Message);
                _logger.LogError(ex.Message, "Unable to register");
                return Page();
            }
            _notyf.Success("Account Created");
            return Redirect("/Account/Login");
        }
    }
}
