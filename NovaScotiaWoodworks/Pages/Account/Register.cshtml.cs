using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovaScotiaWoodworks.AccountManager;
using NovaScotiaWoodworks.Data;
using NovaScotiaWoodworks.Models;
using System;

namespace NovaScotiaWoodworks.Pages.Account
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public UserModel CurrentUser { get; set; }
        private readonly ApplicationDbContext _db;
        private readonly INotyfService _notyf;
        private readonly int _saltSize = 32;
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
                return Page();
            }

            string salt = PasswordHash.CreateSalt(_saltSize);
            string hashedPassword = PasswordHash.GetStringSha256Hash(CurrentUser.Password, salt);
            CurrentUser.Salt = salt;
            CurrentUser.Password = hashedPassword;

            try
            {
                _db.Users.Add(CurrentUser);
                _db.SaveChanges();
            }
            catch(Exception e)
            {
                ModelState.AddModelError("RegisterError", "That username is already in use by another user!" + e.InnerException.Message);
                return Page();
            }
            _notyf.Success("Account Created");
            return Redirect("/Account/Login");
        }
    }
}
