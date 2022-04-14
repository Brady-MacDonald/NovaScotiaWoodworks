using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovaScotiaWoodworks.Data;
using NovaScotiaWoodworks.Models;

namespace NovaScotiaWoodworks.Pages.Products
{
    [Authorize]
    public class PurchaseTableModel : PageModel
    {
        [BindProperty]
        public OrderModel Order { get; set; }
        private readonly ApplicationDbContext _db;
        private readonly INotyfService _notyf;

        public PurchaseTableModel(ApplicationDbContext db, INotyfService notyf)
        {
            _db = db;
            _notyf = notyf;
            Order = new OrderModel();
        }

        public IActionResult OnPost(string purchase)
        {
            //Add the username to the order
            Order.Username = User.Identity.Name;
            Order.Product = purchase;
            Order.OrderTime = System.DateTime.Now;
            Order.Status = "Order Placed";

            if (Order.Email == null)
            {
                _notyf.Error("Enter email address");
                return Redirect("/Products/Tables");
            }

            if (!User.Identity.IsAuthenticated)
            {
                _notyf.Error("Must be signed in to place order");
                return Redirect("/Accounts/Login");
            }

            try
            {
                _db.Orders.Add(Order);
                _db.SaveChanges();
            }
            catch
            {
                _notyf.Error("Unable to place order");
                return Redirect("/Products/Tables");
            }
            _notyf.Success("Order Placed");
            return Redirect("/Products/Tables");
        }
    }
}
