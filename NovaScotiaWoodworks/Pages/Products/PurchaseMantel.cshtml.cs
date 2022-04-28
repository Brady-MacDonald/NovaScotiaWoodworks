using AspNetCoreHero.ToastNotification.Abstractions;
using DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovaScotiaWoodworks.Data;
using NovaScotiaWoodworks.Models;

namespace NovaScotiaWoodworks.Pages.Products
{
    [Authorize]
    public class PurchaseMantelModel : PageModel
    {
        [BindProperty]
        public DataAccess.Models.OrderModel Order { get; set; }
        private readonly IUserData _data;
        private readonly INotyfService _notyf;

        public PurchaseMantelModel(IUserData data, INotyfService notyf)
        {
            _data = data;
            _notyf = notyf;
            Order = new DataAccess.Models.OrderModel();
        }

        public IActionResult OnPost(string purchase)
        {
            //Add the username to the order
            Order.UserName = User.Identity.Name;
            Order.Product = purchase;
            Order.OrderTime = System.DateTime.Now;
            Order.Status = "Order Placed";

            if (Order.EmailAddress == null)
            {
                _notyf.Error("Enter email address");
                return Redirect("/Products/Mantels");
            }

            if (!User.Identity.IsAuthenticated)
            {
                _notyf.Error("Must be signed in to place order");
                return Redirect("/Accounts/Login");
            }

            try
            {
                _data.InsertOrder(Order);
            }
            catch
            {
                _notyf.Error("Unable to place order");
                return Redirect("/Products/Mantels");
            }
            _notyf.Success("Order Placed");
            return Redirect("/Products/Mantels");
        }
    }
}
