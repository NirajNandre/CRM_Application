using CRM.Business;
using CRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CRM.Pages.AssetAllocation
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
        public IList<Models.EmployeeMaster> EmployeeMaster { get; set; } = default!;

        [BindProperty]
        public IList<Models.AssetMaster> AssetMaster { get; set; } = default!;
        [BindProperty]
        public IList<Models.AssetAllocation> AssetAllocation { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            Models.Users user = HttpContext.Session.GetCustomObjectFromSession<Models.Users>("LoggedUser");
            if (user == null)
            {
                return RedirectToPage("/Login/Index");
            }
            if (_context.AssetAllocation != null)
            {
              
                AssetAllocation = await _context.AssetAllocation
                .Include(a => a.AssetMaster)
                .Include(a => a.EmployeeMaster)
                .ToListAsync();
                return Page();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var assetallocation = await _context.AssetAllocation.FindAsync(id);
            if (assetallocation == null)
            {
                return NotFound();
            }
            else
            {
                _context.AssetAllocation.Remove(assetallocation);
                await _context.SaveChangesAsync();
                return RedirectToPage("Index");
            }
        }
        public async Task<IActionResult> OnPostDeallocate(int id)
        {
            var assetAllocation = await _context.AssetAllocation.FindAsync(id);

            if (assetAllocation == null)
            {
                return NotFound();
            }

            // Update the status and deallocation date
            assetAllocation.Status = AssetStatus.Deallocated;
            assetAllocation.DeAllocatedOn = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }

    }
}
