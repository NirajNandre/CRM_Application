using CRM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CRM.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly AssetDbContext _db;
        public IndexModel(AssetDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public IEnumerable<Models.Users> Users { get; set; }
        public async Task OnGet()
        {
            // Users = await _db.Users.ToListAsync();
            Users = await _db.Users.Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var Users = await _db.Users.FindAsync(id);
            if (Users == null)
            {
                return NotFound();
            }
            else
            {
                Users.IsDeleted = true;
                await _db.SaveChangesAsync();

                return RedirectToPage("Index");

            }
        }

    }
}
