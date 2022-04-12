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
    public class EditProfileModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public UserModel CurrentUser { get; set; }

        public EditProfileModel(ApplicationDbContext db)
        {
            _db = db;
            CurrentUser = new UserModel();
        }
        public void OnGet()
        {
            CurrentUser = _db.Users.Find(User.Identity.Name);

            System.Threading.Thread.Sleep(1000);

            if (CurrentUser == null)
            {
                Redirect("/AccessDenied");
            }

            //return Redirect("/Index");
        }

        public IActionResult OnPost()
        {
            //Updates existing entry based off key
            _db.Users.Update(CurrentUser);
            //Goes to db to push changes
            _db.SaveChanges();
            //Looks for index action inside same controller
            return Redirect("/Index");
        }
    }
}
