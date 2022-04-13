using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovaScotiaWoodworks.Data;
using NovaScotiaWoodworks.Models;
using System.Threading.Tasks;

namespace NovaScotiaWoodworks.Pages.Products
{
    public class TablesModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public OrderModel Order { get; set; }
        
        public TablesModel(ApplicationDbContext db)
        {
            db = _db;
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

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("NoAccount", "Invalid input");
                return Page();
            }

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

            return Redirect("/Products/Tables");

        }
    }
}
