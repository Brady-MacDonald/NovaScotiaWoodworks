using DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovaScotiaWoodworks.Models;
using System.Threading.Tasks;
using OrderModel = DataAccess.Models.OrderModel;

namespace NovaScotiaWoodworks.Pages.Admin
{
    [Authorize(Policy = "AdminOnly")]
    public class UpdateOrderModel : PageModel
    { 
        [BindProperty]
        public OrderModel CurrentOrder { get; set; }
        private readonly IUserData _data;

        public UpdateOrderModel(IUserData data)
        {
            _data = data;
            CurrentOrder = new OrderModel();
        }

        public async Task<IActionResult> OnGet(int id)
        {
            //CurrentOrder = await _data.GetOrders();


            if (CurrentOrder == null)
            {
                ModelState.AddModelError("UpdateError", "Unable to load order information");
            }
            return Page();
        }
        public async Task<IActionResult> OnPost(OrderModel currentOrder)
        {
            //Unable to locate order
            if (CurrentOrder == null)
                return Redirect("/Index");

            try
            {
                //await _data.UpdateOrder(CurrentOrder);
            }
            catch
            {
                return Page();
            }

            return Redirect("/Admin/ListOrders");
        }
    }
}
