using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace NovaScotiaWoodworks.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            //Destroy cookie and sign out user
            await HttpContext.SignOutAsync("AuthenticationCookie");
            return RedirectToPage("/Index");
        }
    }
}
