using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovaScotiaWoodworks.Data;
using NovaScotiaWoodworks.Models;
using System.Collections.Generic;
using System.Linq;

namespace NovaScotiaWoodworks.Pages.Account
{
    [Authorize]
    public class OrdersModel : PageModel
    {
        [BindProperty]
        public IEnumerable<OrderModel> OrderList { get; set; }
        private readonly ApplicationDbContext _db;

        public OrdersModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            //Return the full set of orders 
            OrderList = _db.Orders.Where(x => x.Username == User.Identity.Name);

            System.Threading.Thread.Sleep(1000);
        }
    }
}
