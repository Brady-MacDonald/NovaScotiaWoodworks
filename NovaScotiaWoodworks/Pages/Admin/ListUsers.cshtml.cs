using DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovaScotiaWoodworks.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserModel = DataAccess.Models.UserModel;

namespace NovaScotiaWoodworks.Pages.Admin
{
    [Authorize(Policy = "AdminOnly")]
    public class ListUsersModel : PageModel
    {
        [BindProperty]
        public IEnumerable<UserModel> UserList { get; set; }
        private readonly IUserData _data;

        public ListUsersModel(IUserData data)
        {
            _data = data;
            UserList = new List<UserModel>();
        }
        public async Task OnGet()
        {
            UserList = await _data.GetUsers();
        }
    }
}
