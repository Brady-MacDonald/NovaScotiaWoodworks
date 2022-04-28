using AspNetCoreHero.ToastNotification.Abstractions;
using DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovaScotiaWoodworks.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NovaScotiaWoodworks.Pages.Account
{
    [Authorize]
    public class EditProfileModel : PageModel
    {
        [BindProperty]
        public DataAccess.Models.UserModel CurrentUser { get; set; }
        private readonly IUserData _data;
        private readonly INotyfService _notyf;

        public EditProfileModel(INotyfService notyf, IUserData data)
        {
            _data = data;
            _notyf = notyf;
            CurrentUser = new DataAccess.Models.UserModel();
        }
        public async Task<IActionResult> OnGet()
        {
            try
            {
                CurrentUser = await _data.GetUser(User.Identity.Name);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("EditError", "Error: " + ex.Message);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            DataAccess.Models.UserModel dbUser = await _data.GetUser(User.Identity.Name);

            if(dbUser == null)
            {
                ModelState.AddModelError("EditError", "Unable to locate account");
                return Page();
            }

            try
            {
                //CurrentUser.Password = dbUser.Password;
                //CurrentUser.Username = dbUser.Username;
                CurrentUser.Id = dbUser.Id;
                //Unable to make changes to database due to db context being shared by multiple requests
                await _data.UpdateUser(CurrentUser);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("EditError", "Unable to update account");
                _notyf.Error("Unable to update account");
                return Page();
            }

            _notyf.Success("Account Updated");
            return Page();
        }
    }
}
