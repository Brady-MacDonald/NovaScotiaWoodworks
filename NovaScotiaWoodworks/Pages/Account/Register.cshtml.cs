using AspNetCoreHero.ToastNotification.Abstractions;
using DataAccess.Data;
using DataAccess.DbAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovaScotiaWoodworks.AccountManager;
using NovaScotiaWoodworks.Data;
using NovaScotiaWoodworks.Models;
using System;
using System.Threading.Tasks;

namespace NovaScotiaWoodworks.Pages.Account
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public UserModel CurrentUser { get; set; }
        private readonly ApplicationDbContext _db;
        private readonly INotyfService _notyf;
        private readonly IUserData _data;
        private readonly IDatabaseAccess _dbAccess;
        private readonly int _saltSize = 32;
        public RegisterModel(ApplicationDbContext db, INotyfService notyf, IUserData data, IDatabaseAccess dbAccess)
        {
            _db = db;
            _notyf = notyf;
            _data = data;
            _dbAccess = dbAccess;
            CurrentUser = new UserModel();
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

            string salt = PasswordHash.CreateSalt(_saltSize);
            string hashedPassword = PasswordHash.GetStringSha256Hash(CurrentUser.Password, salt);
            CurrentUser.Salt = salt;
            CurrentUser.Password = hashedPassword;

            DataAccess.Models.UserModel user = new DataAccess.Models.UserModel
            {
        
                FirstName = CurrentUser.FirstName,
                LastName = CurrentUser.LastName,
                UserName = CurrentUser.Username,
                EmailAddress = "test@testing.com",
                Password = CurrentUser.Password,
                Salt = salt,
            };

            try
            {
                _db.Users.Add(CurrentUser);
                _db.SaveChanges();
                await _data.InsertUser(user);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("RegisterError", "That username is already in use by another user!" + e.InnerException.Message);
                return Page();
            }
            _notyf.Success("Account Created");
            return Redirect("/Account/Login");
        }
    }
}
