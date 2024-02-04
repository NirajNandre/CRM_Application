using CRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CRM.Pages.Users
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly AssetDbContext _db;
        public CreateModel(AssetDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Models.Users Users { get; set; }
        

        

    }
}
