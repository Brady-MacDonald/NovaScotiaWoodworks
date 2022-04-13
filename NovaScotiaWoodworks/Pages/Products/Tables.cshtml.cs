using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovaScotiaWoodworks.Data;
using NovaScotiaWoodworks.Models;
using System.Threading.Tasks;

namespace NovaScotiaWoodworks.Pages.Products
{
    public class TablesModel : PageModel
    {
        [BindProperty]
        public OrderModel Order { get; set; }
        private readonly ApplicationDbContext _db;
        private readonly INotyfService _notyf;

        public TablesModel(ApplicationDbContext db, INotyfService notyf)
        {
            _db = db;
            _notyf = notyf;
            Order = new OrderModel();
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if(!User.Identity.IsAuthenticated)
            {
                ModelState.AddModelError("OrderError", "Must be signed in to place order");
                return Page();
            }

            //Add the username to the order
            Order.Username = User.Identity.Name;
            Order.Product = "Table";
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
