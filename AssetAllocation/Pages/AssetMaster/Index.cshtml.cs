using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using CRM.Models;
using CRM.Business;

namespace CRM.Pages.AssetMaster
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly AssetDbContext _context;

        public IndexModel(AssetDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public IList<Models.AssetMaster> AssetMaster { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            Models.Users user = HttpContext.Session.GetCustomObjectFromSession<Models.Users>("LoggedUser");
            if (user == null)
            {
                return RedirectToPage("/Login/Index");
            }
            if (_context.AssetMaster != null)
            {
                AssetMaster = await _context.AssetMaster
                .Include(a => a.AssetTypes)
                .Include(a => a.Users).ToListAsync();
                return Page();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var asset = await _context.AssetMaster.FindAsync(id);
            if (asset == null)
            {
                return NotFound();
            }
            else
            {
                _context.AssetMaster.Remove(asset);
                await _context.SaveChangesAsync();
                return RedirectToPage("Index");
            }
        }
    }
}
