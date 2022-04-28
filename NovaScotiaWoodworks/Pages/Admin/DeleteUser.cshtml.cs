using DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovaScotiaWoodworks.Models;
using System;
using System.Threading.Tasks;

namespace NovaScotiaWoodworks.Pages.Admin
{
    [Authorize(Policy = "AdminOnly")]
    public class DeleteUserModel : PageModel
    {
        private readonly IUserData _data;

		public DeleteUserModel(IUserData data)
		{
            _data = data;
		}
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(int id)
		{
            try
            {
                await _data.DeleteUser(id);
            }
            catch (Exception ex)
            {
                //Add logging
                return Redirect("/Index");
            }

            return Redirect("/Admin/ListUsers");
        }
    }
}
