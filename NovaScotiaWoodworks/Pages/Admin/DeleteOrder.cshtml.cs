using AspNetCoreHero.ToastNotification.Abstractions;
using DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovaScotiaWoodworks.Data;
using NovaScotiaWoodworks.Models;

namespace NovaScotiaWoodworks.Pages.Admin
{
    [Authorize(Policy = "AdminOnly")]
    public class DeleteOrderModel : PageModel
    {
        private readonly IUserData _data;
        private readonly INotyfService _notyf;

        public DeleteOrderModel(IUserData data, INotyfService notyf)
        {
            _data = data;
            _notyf = notyf;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost(int id)
        {
            try
            {
                _data.DeleteOrder(id);
            }
            catch
            {
                _notyf.Error("Error deleting order");
            }

            _notyf.Success("Order Deleted");
            return Redirect("/Admin/ListOrders");
        }
    }
}
