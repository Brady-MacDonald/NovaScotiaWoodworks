using DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovaScotiaWoodworks.Data;
using NovaScotiaWoodworks.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NovaScotiaWoodworks.Pages.Account
{
    [Authorize]
    public class OrdersModel : PageModel
    {
        [BindProperty]
        public IEnumerable<DataAccess.Models.OrderModel> OrderList { get; set; }
        private readonly IUserData _data;

        public OrdersModel(IUserData data)
        {
            _data = data;
        }
        public async Task OnGet()
        {
            OrderList = await _data.GetOrderByUserName(User.Identity.Name);
        }
    }
}
