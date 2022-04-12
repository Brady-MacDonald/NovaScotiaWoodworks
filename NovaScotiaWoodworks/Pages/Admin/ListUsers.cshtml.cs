using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovaScotiaWoodworks.Data;
using NovaScotiaWoodworks.Models;
using System.Collections.Generic;

namespace NovaScotiaWoodworks.Pages.Admin
{
    [Authorize(Policy = "AdminOnly")]
    public class ListUsersModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public IEnumerable<UserModel> UserList { get; set; }

        public ListUsersModel(ApplicationDbContext db)
        {
            _db = db;
            UserList = new List<UserModel>();
        }
        public void OnGet()
        {
            //Return the full set of contacts 
            UserList = _db.Users;
            System.Threading.Thread.Sleep(2000);
        }
    }
}
