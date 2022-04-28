using AspNetCoreHero.ToastNotification.Abstractions;
using DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace NovaScotiaWoodworks.Pages.Products
{
    [Authorize]
    public class PurchaseTableModel : PageModel
    {
        [BindProperty]
        public DataAccess.Models.OrderModel Order { get; set; }
        private readonly IUserData _data;
        private readonly INotyfService _notyf;

        public PurchaseTableModel(IUserData data, INotyfService notyf)
        {
            _data = data;
            _notyf = notyf;
            Order = new DataAccess.Models.OrderModel();
        }

        public async Task<IActionResult> OnPost(string purchase)
        {
            //Add the username to the order
            Order.UserName = User.Identity.Name;
            Order.Product = purchase;
            Order.OrderTime = DateTime.Now;
            Order.Status = "Order Placed";

            if (Order.EmailAddress == null)
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
                await _data.InsertOrder(Order);
            }
            catch (Exception ex)
            {
                _notyf.Error("Unable to place order");
                return Redirect("/Products/Tables");
            }

            _notyf.Success("Order Placed");
            return Redirect("/Products/Tables");
        }
    }
}
