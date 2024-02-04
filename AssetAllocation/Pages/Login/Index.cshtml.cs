using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using CRM.Models;
using CRM.Business;

namespace CRM.Pages.Login
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly AssetDbContext _db;
        public IndexModel(AssetDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }
        public class InputModel
        {
            [Required]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            public string? Role { get; set; }

        }


        public async Task OnGetAsync(string returnUrl)
        {
            // return url is where the user will redirect after login
            // if return url founds to be null then it will be assignd the value of url.content
            // If user tries to enter the url example - "/assestAllocation/create" on login page then the user will redirect to that page after login 
            returnUrl ??= Url.Content("~/");

            // from below line it make sure to clear the cookie after getting signout 
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            ReturnUrl = returnUrl;
            ViewData["ReturnUrl"] = returnUrl;


        }
        public async Task<IActionResult> OnPostAsync(string? returnUrl)
        {
            // returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                //Check User Login 
                var user = _db.Users.Where(f => f.UserName == Input.UserName && f.Password == Input.Password && f.IsDeleted == false).FirstOrDefault();
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid Email or Password");
                    return Page();
                }

                //for last login 
                // Update the LastLogin property with the current date and time
                user.LastLogin = DateTime.Now;
                await _db.SaveChangesAsync();
                //for the session after logging
                // the value of loggeduser is stored in user object
                HttpContext.Session.SetObjectInSession("LoggedUser", user);
                //creating Security Context
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("UserDefined", "whatever"),
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        principal,
                        new AuthenticationProperties { IsPersistent = true });
                // return LocalRedirect(returnUrl);
                if (returnUrl != null)
                {
                    return LocalRedirect(returnUrl);

                }
                else
                {
                    return RedirectToPage("/Dashboard/Index");
                }
            }

            return Page();
        }
    }

}

