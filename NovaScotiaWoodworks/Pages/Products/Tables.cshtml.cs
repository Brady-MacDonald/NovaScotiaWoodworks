using AspNetCoreHero.ToastNotification.Abstractions;
using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovaScotiaWoodworks.Models;
using System.Linq;
using System.Threading.Tasks;

namespace NovaScotiaWoodworks.Pages.Products
{
    public class TablesModel : PageModel
    {
        [BindProperty]
        public DataAccess.Models.OrderModel Order { get; set; }
        public bool DisableSquare { get; set; }
        public bool DisableCoffee { get; set; }
        private readonly IUserData _data;
        private readonly INotyfService _notyf;

        public TablesModel(IUserData data, INotyfService notyf)
        {
            _data = data;
            _notyf = notyf;
            Order = new DataAccess.Models.OrderModel();
        }

        public async Task OnGet()
        {
            DataAccess.Models.OrderModel orderCoffeeTable = await _data.GetOrderByProduct("Coffee Table");
            if (orderCoffeeTable != null)
            {
                ModelState.AddModelError("CoffeeTablePurchased", " (SOLD)");
                DisableCoffee = true;
            }

            DataAccess.Models.OrderModel orderSquareTable = await _data.GetOrderByProduct("Square Table");
            if (orderSquareTable != null)
            {
                ModelState.AddModelError("SquareTablePurchased", " (SOLD)");
                DisableSquare = true;
            }
        }

        public async Task<IActionResult> OnPost()
        {
            if(!User.Identity.IsAuthenticated)
            {
                ModelState.AddModelError("OrderError", "Must be signed in to place order");
                return Page();
            }

            //Add the username to the order
            Order.UserName = User.Identity.Name;
            Order.Product = "Custome Table";
            Order.OrderTime = System.DateTime.Now;
            Order.Status = "Order Placed";

            try
            {
                await _data.InsertOrder(Order);
            }
            catch
            {
                ModelState.AddModelError("OrderError", "Unable to place order");
                return Page();
            }
            _notyf.Success("Order Placed");
            return Page();
        }
    }
}
