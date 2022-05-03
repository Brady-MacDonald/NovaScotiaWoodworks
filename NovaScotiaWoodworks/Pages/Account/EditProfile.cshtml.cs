using AspNetCoreHero.ToastNotification.Abstractions;
using DataAccess.Data;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NovaScotiaWoodworks.Pages.Account
{
    [Authorize]
    public class EditProfileModel : PageModel
    {
        [BindProperty]
        public UserModel CurrentUser { get; set; }
        private readonly IUserData _data;
        private readonly INotyfService _notyf;
        private readonly ILogger<EditProfileModel> _logger;

        public EditProfileModel(INotyfService notyf, IUserData data, ILogger<EditProfileModel> logger)
        {
            _data = data;
            _notyf = notyf;
            _logger = logger;
            CurrentUser = new UserModel();
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
                _logger.LogError("Unable to locate user to edit", ex);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            UserModel dbUser = await _data.GetUser(User.Identity.Name);

            if(dbUser == null)
            {
                ModelState.AddModelError("EditError", "Unable to locate account");
                return Page();
            }

            try
            {
                CurrentUser.Id = dbUser.Id;
                await _data.UpdateUser(CurrentUser);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("EditError", "Unable to update account" + ex.Message);
                _notyf.Error("Unable to update account");
                _logger.LogError("Unable to update account", ex);
                _logger.LogError("Unable to update, Message: ", ex.Message);
                return Page();
            }

            _notyf.Success("Account Updated");
            return Page();
        }
    }
}
