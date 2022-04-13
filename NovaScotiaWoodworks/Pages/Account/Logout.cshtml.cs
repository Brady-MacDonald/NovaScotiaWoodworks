using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace NovaScotiaWoodworks.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly INotyfService _notyf;
        public LogoutModel(INotyfService notyf)
        {
            _notyf = notyf;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            //Destroy cookie and sign out user
            await HttpContext.SignOutAsync("AuthenticationCookie");
            _notyf.Warning("Signed Out");
            return RedirectToPage("/Index");
        }
    }
}
