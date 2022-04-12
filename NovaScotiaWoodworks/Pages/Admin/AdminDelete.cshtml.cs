using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovaScotiaWoodworks.Data;
using NovaScotiaWoodworks.Models;

namespace NovaScotiaWoodworks.Pages.Admin
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminDeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

		public AdminDeleteModel(ApplicationDbContext db)
		{
            _db = db;
		}
        public void OnGet()
        {
        }

        public IActionResult OnPost(string username)
		{
            UserModel dbUser = _db.Users.Find(username);

            if (dbUser == null)
                //Unable to locate user account
                return Redirect("Index");

            _db.Users.Remove(dbUser);
            _db.SaveChanges();

            return Redirect("/Admin/ListUsers");
        }
    }
}
