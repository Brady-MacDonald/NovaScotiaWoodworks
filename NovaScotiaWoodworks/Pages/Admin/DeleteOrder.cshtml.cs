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
        private readonly ApplicationDbContext _db;

        public DeleteOrderModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost(int id)
        {
            OrderModel dbOrder = _db.Orders.Find(id);

            if (dbOrder == null)
                //Unable to locate user account
                return Redirect("/Index");

            try
            {
                _db.Orders.Remove(dbOrder);
                _db.SaveChanges();
            }
            catch
            {

            }

            return Redirect("/Admin/ListOrders");
        }
    }
}
