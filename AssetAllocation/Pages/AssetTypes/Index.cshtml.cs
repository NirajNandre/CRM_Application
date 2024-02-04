using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using CRM.Models;

namespace CRM.Pages.AssetTypes
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
        public IList<Models.AssetTypes> AssetTypes { get; set; } = default!;

        public async Task OnGetAsync()
        {
            AssetTypes = await _context.AssetTypes.ToListAsync();
        }


        public async Task<IActionResult> OnPostDelete(int id)
        {
            var Asset = await _context.AssetTypes.FindAsync(id);

            if (Asset == null)
            {
                return NotFound();
            }

            _context.AssetTypes.Remove(Asset);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }

    }
}
