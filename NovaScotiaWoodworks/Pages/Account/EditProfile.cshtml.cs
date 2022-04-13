using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovaScotiaWoodworks.Data;
using NovaScotiaWoodworks.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NovaScotiaWoodworks.Pages.Account
{
    [Authorize]
    public class EditProfileModel : PageModel
    {
        [BindProperty]
        public UserModel CurrentUser { get; set; }
        private readonly ApplicationDbContext _db;
        private readonly INotyfService _notyf;

        public EditProfileModel(ApplicationDbContext db, INotyfService notyf)
        {
            _db = db;
            _notyf = notyf;
            CurrentUser = new UserModel();
        }
        public IActionResult OnGet()
        {
            CurrentUser = _db.Users.Find(User.Identity.Name);

            System.Threading.Thread.Sleep(1000);

            if (CurrentUser == null)
            {
                ModelState.AddModelError("EditError", "Unable to load account information");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            UserModel dbUser = _db.Users.Find(User.Identity.Name);
            var OrderList = _db.Orders.Where(x => x.Username == User.Identity.Name);
            try
            {
                //Updates existing entry based off key
                _db.Users.Update(CurrentUser);
                //Goes to db to push changes
                _db.SaveChanges();
            }
            catch
            {
                ModelState.AddModelError("EditError", "Unable to update account");
                return Page();
            }

            _notyf.Success("Account Updated");
            return Page();
        }
    }
}
