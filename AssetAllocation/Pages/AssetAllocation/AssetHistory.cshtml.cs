using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CRM.Business;
using CRM.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.Pages.AssetAllocation
{
    public class AssetHistoryModel : PageModel
    {
        private readonly AssetDbContext _context;

        public AssetHistoryModel(AssetDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.AssetAllocation AssetAllocation { get; set; }

        [BindProperty]
        public List<Models.AssetMaster> AssetMaster { get; set; }

        [BindProperty]
        public List<Models.AssetAllocation> AssetHistory { get; set; }

        [BindProperty]
        public int SelectedAssetId { get; set; }

        public List<SelectListItem> AssetOptions { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            AssetHistory = await _context.AssetAllocation
                .Include(aa => aa.AssetMaster)
                .Include(aa => aa.EmployeeMaster)
                .Where(aa => aa.Status == AssetStatus.Deallocated)
                .OrderByDescending(aa => aa.DeAllocatedOn)
                .ToListAsync();

            //AssetOptions = _context.AssetMaster
            //   .Select(asset => new SelectListItem
            //   {
            //       Value = asset.Id.ToString(),
            //       Text = asset.Name
            //   })
            //   .ToList();


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
                return RedirectToPage("AssetHistory");
            }
        }
    }
}





//public async Task<IActionResult> OnPostDeleteAsync(int id)
//        {
//            var assetAllocation = await _context.AssetAllocation.FindAsync(id);
//            if (assetAllocation != null)
//            {
//                _context.AssetAllocation.Remove(assetAllocation);
//                await _context.SaveChangesAsync();
//            }

//            return RedirectToPage("AssetHistory");
//        }
//    }
//}
