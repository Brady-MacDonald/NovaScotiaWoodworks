using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovaScotiaWoodworks.Data;
using NovaScotiaWoodworks.Models;

namespace NovaScotiaWoodworks.Pages.Account
{
    [Authorize]
    public class CancelOrderModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly INotyfService _notyf;

        public CancelOrderModel(ApplicationDbContext db, INotyfService notyf)
        {
            _db = db;
            _notyf = notyf;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost(int id)
        {
            OrderModel dbOrder = _db.Orders.Find(id);

            if (dbOrder == null)
            {
                _notyf.Error("Unable to locate order");
                return Redirect("/Account/Orders");
            }

            try
            {
                _db.Orders.Remove(dbOrder);
                _db.SaveChanges();
            }
            catch
            {
                _notyf.Error("Error cancelling order");
            }
            _notyf.Success("Order Cancelled");
            return Redirect("/Account/Orders");
        }
    }
}
