using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovaScotiaWoodworks.Data;
using NovaScotiaWoodworks.Models;
using System.Collections.Generic;
using System.Linq;

namespace NovaScotiaWoodworks.Pages.Products
{
    public class MantelsModel : PageModel
    {
        [BindProperty]
        public OrderModel Order { get; set; }
        public bool DisbaleClassic { get; set; }
        public bool DisbaleModern { get; set; }
        private readonly ApplicationDbContext _db;
        private readonly INotyfService _notyf;

        public MantelsModel(ApplicationDbContext db, INotyfService notyf)
        {
            _db = db;
            _notyf = notyf;
            Order = new OrderModel();
        }

        public void OnGet()
        {
            OrderModel orderClassicMantel = _db.Orders.FirstOrDefault(x => x.Product == "Classic Mantel");
            if (orderClassicMantel != null)
            {
                ModelState.AddModelError("MantelPurchased", " (SOLD)");
                DisbaleClassic = true;
            }
            OrderModel orderModernMantel = _db.Orders.FirstOrDefault(x => x.Product == "Modern Mantel");
            if (orderModernMantel != null)
            {
                ModelState.AddModelError("MantelPurchased", " (SOLD)");
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
            Order.Username = User.Identity.Name;
            Order.Product = "Custom Mantel";
            Order.OrderTime = System.DateTime.Now;

            try
            {
                _db.Orders.Add(Order);
                _db.SaveChanges();
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
