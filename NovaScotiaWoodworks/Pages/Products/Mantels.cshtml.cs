using AspNetCoreHero.ToastNotification.Abstractions;
using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovaScotiaWoodworks.Data;
using NovaScotiaWoodworks.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NovaScotiaWoodworks.Pages.Products
{
    public class MantelsModel : PageModel
    {
        [BindProperty]
        public DataAccess.Models.OrderModel Order { get; set; }
        public bool DisbaleClassic { get; set; }
        public bool DisbaleModern { get; set; }
        private readonly IUserData _data;
        private readonly INotyfService _notyf;

        public MantelsModel(IUserData data, INotyfService notyf)
        {
            _data = data;
            _notyf = notyf;
            Order = new DataAccess.Models.OrderModel();
        }

        public async Task OnGet()
        {
            DataAccess.Models.OrderModel orderClassicMantel = await _data.GetOrderByProduct("Classic Mantel");
            if (orderClassicMantel != null)
            {
                ModelState.AddModelError("ClassicMantelPurchased", " (SOLD)");
                DisbaleClassic = true;
            }
            DataAccess.Models.OrderModel orderModernMantel = await _data.GetOrderByProduct("Modern Mantel");
            if (orderModernMantel != null)
            {
                ModelState.AddModelError("ModernMantelPurchased", " (SOLD)");
                DisbaleModern = true;
            }
        }

        public IActionResult OnPost()
        {
            if (!User.Identity.IsAuthenticated)
            {
                ModelState.AddModelError("OrderError", "Must be signed in to place order");
                return Page();
            }

            //Add the username to the order
            Order.UserName = User.Identity.Name;
            Order.Product = "Custom Mantel";
            Order.OrderTime = System.DateTime.Now;
            Order.Status = "Order Placed";

            try
            {
                _data.InsertOrder(Order);
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
