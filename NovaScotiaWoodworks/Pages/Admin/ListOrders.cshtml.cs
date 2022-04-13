using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovaScotiaWoodworks.Data;
using NovaScotiaWoodworks.Models;
using System.Collections.Generic;

namespace NovaScotiaWoodworks.Pages.Admin
{
    [Authorize(Policy = "AdminOnly")]
    public class ListOrdersModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public IEnumerable<OrderModel> OrderList { get; set; }

        public ListOrdersModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            //Return the full set of orders 
            OrderList = _db.Orders;
            System.Threading.Thread.Sleep(1000);
        }
    }
}
