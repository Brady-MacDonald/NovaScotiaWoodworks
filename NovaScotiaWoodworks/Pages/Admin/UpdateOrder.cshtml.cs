using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovaScotiaWoodworks.Data;
using NovaScotiaWoodworks.Models;

namespace NovaScotiaWoodworks.Pages.Admin
{
    [Authorize(Policy = "AdminOnly")]
    public class UpdateOrderModel : PageModel
    { 
        [BindProperty]
        public OrderModel CurrentOrder { get; set; }
        private readonly ApplicationDbContext _db;

        public UpdateOrderModel(ApplicationDbContext db)
        {
            _db = db;
            //CurrentOrder = new OrderModel();
        }

        public IActionResult OnGet(int id)
        {
            CurrentOrder = _db.Orders.Find(id);

            System.Threading.Thread.Sleep(1000);

            if (CurrentOrder == null)
            {
                ModelState.AddModelError("UpdateError", "Unable to load order information");
            }
            return Page();
        }
        public IActionResult OnPost(OrderModel currentOrder)
        {
            //Unable to locate order
            if (CurrentOrder == null)
                return Redirect("/Index");

            try
            {
                _db.Orders.Update(CurrentOrder);
                _db.SaveChanges();
            }
            catch
            {
                return Page();
            }

            return Redirect("/Admin/ListOrders");
        }
    }
}
