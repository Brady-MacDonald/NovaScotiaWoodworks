using AspNetCoreHero.ToastNotification.Abstractions;
using DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovaScotiaWoodworks.Data;
using NovaScotiaWoodworks.Models;
using System;
using System.Threading.Tasks;

namespace NovaScotiaWoodworks.Pages.Account
{
    [Authorize]
    public class CancelOrderModel : PageModel
    {
        private readonly IUserData _data;
        private readonly INotyfService _notyf;

        public CancelOrderModel(IUserData data, INotyfService notyf)
        {
            _data = data;
            _notyf = notyf;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(int id)
        {
            try
            {
                await _data.DeleteOrder(id);
            }
            catch (Exception ex)
            {
                _notyf.Error("Error cancelling order");
                return Redirect("Orders");
            }
            _notyf.Success("Order Cancelled");
            return Page();
        }
    }
}
