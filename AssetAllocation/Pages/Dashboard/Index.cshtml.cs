using CRM.Models;
using CRM.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CRM.Pages.Dashboard
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            Models.Users user = HttpContext.Session.GetCustomObjectFromSession<Models.Users>("LoggedUser");
            if (user == null)
            {
                return RedirectToPage("/Login/Index");
            }
            // do whatever you want with user object.

            ViewData["LoggedUser"] = user.FullName;
            ViewData["LoggedUserRole"] = user.Role;

            return Page();
        }
    }
}
