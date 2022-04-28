using AspNetCoreHero.ToastNotification.Abstractions;
using DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovaScotiaWoodworks.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NovaScotiaWoodworks.Pages.Admin
{
    [Authorize(Policy = "AdminOnly")]
    public class ListOrdersModel : PageModel
    {
        [BindProperty]
        public IEnumerable<DataAccess.Models.OrderModel> OrderList { get; set; }
        private readonly IUserData _data;

        public ListOrdersModel(IUserData data)
        {
            _data = data;
            OrderList = new List<DataAccess.Models.OrderModel>();
        }

        public async Task OnGet()
        {
            OrderList = await _data.GetOrders();
        }
    }
}
