using CRM.Business;
using CRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CRM.Pages.AssetAllocation
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly AssetDbContext _context;
        public string loggedUsername = "";
        public CreateModel(AssetDbContext context)
        {
            _context = context;
        }
        public List<SelectListItem> Options { get; set; }
        [BindProperty]
        public IList<Models.EmployeeMaster> EmployeeMaster { get; set; } = default!;

        [BindProperty]
        public IList<Models.AssetMaster> AssetMaster { get; set; } = default!;
        [BindProperty]
        public Models.AssetAllocation AssetAllocation { get; set; } = default!;
        public async Task<IActionResult> OnGet(int? id)
        {

            AssetAllocation = new Models.AssetAllocation();
            Models.Users user = HttpContext.Session.GetCustomObjectFromSession<Models.Users>("LoggedUser");
            if (user == null)
            {
                RedirectToPage("/Login/Index");
            }
            loggedUsername = user.UserName;
            if (id == null)
            {
                //create     
                AssetMaster = await _context.AssetMaster
                        .Where(am => !_context.AssetAllocation.Any(aa => aa.AssetId == am.Id && aa.Status == AssetStatus.Allocated))
                        .ToListAsync();

                EmployeeMaster = await _context.EmployeeMaster.ToListAsync();
                
                return Page();
            }

#pragma warning disable CS8601 // Possible null reference assignment.
            AssetAllocation = await _context.AssetAllocation.FirstOrDefaultAsync(u => u.Id == id);
#pragma warning restore CS8601 // Possible null reference assignment.
            if (AssetAllocation == null)
            {
                return NotFound();
            }

            if (_context.AssetMaster != null)
            {

                //AssetMaster = await _context.AssetMaster.ToListAsync();
                int editedEmployeeId = await (
                from allocation in _context.AssetAllocation
                where allocation.Id == id
                select allocation.EmployeeId
            ).FirstOrDefaultAsync();

                var allocatedAssets = await (
                from allocation in _context.AssetAllocation
                where allocation.EmployeeId == editedEmployeeId
                select allocation.AssetMaster
            ).ToListAsync();

                var unallocatedAssets = await (
                from asset in _context.AssetMaster
                join allocation in _context.AssetAllocation
                    on asset.Id equals allocation.AssetId into gj
                from suballocation in gj.DefaultIfEmpty()
                where suballocation == null || (suballocation.EmployeeId == editedEmployeeId && !allocatedAssets.Contains(asset))
                select asset
                    ).ToListAsync();



                AssetMaster = allocatedAssets.Concat(unallocatedAssets).ToList();

                if (_context.EmployeeMaster != null)
                {
                    EmployeeMaster = await _context.EmployeeMaster.ToListAsync();

                    return Page();
                }
                return Page();
            }

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            Models.Users user = HttpContext.Session.GetCustomObjectFromSession<Models.Users>("LoggedUser");
            if (user == null)
            {
                RedirectToPage("/Login/Index");
            }

            if (ModelState.IsValid)
            {
                if (AssetAllocation.Id == 0)
                {
                    // Allocate a new asset to an employee
                    var selAssetId = HttpContext.Request.Form["ddlAssetName"].ToString();
                    AssetAllocation.AssetId = Convert.ToInt32(selAssetId);

                    var selEmployeeId = HttpContext.Request.Form["ddlEmployeeName"].ToString();
                    AssetAllocation.EmployeeId = Convert.ToInt32(selEmployeeId);

                    AssetAllocation.AllocatedOn = DateTime.Now;
                    AssetAllocation.Status = AssetStatus.Allocated;

                    await _context.AssetAllocation.AddAsync(AssetAllocation);
                }
                else
                {
                    // Deallocate an asset from an employee
                    var assetAllocationFromDB = await _context.AssetAllocation.FindAsync(AssetAllocation.Id);
                    var selAssetId = HttpContext.Request.Form["ddlAssetName"].ToString();
                    assetAllocationFromDB.AssetId = Convert.ToInt32(selAssetId);

                    var selEmployeeId = HttpContext.Request.Form["ddlEmployeeName"].ToString();
                    assetAllocationFromDB.EmployeeId = Convert.ToInt32(selEmployeeId);

                    if (assetAllocationFromDB.Status == AssetStatus.Allocated && AssetAllocation.Status == AssetStatus.Deallocated)
                    {
                        // Set deallocated date if the status is being changed to deallocated
                        assetAllocationFromDB.DeAllocatedOn = DateTime.Now;
                        // Create a new record for reallocation
                        var newAllocation = new Models.AssetAllocation
                        {
                            AssetId = assetAllocationFromDB.AssetId,
                            EmployeeId = assetAllocationFromDB.EmployeeId,
                            AllocatedOn = DateTime.Now,
                            Status = AssetStatus.Allocated
                        };
                        await _context.AssetAllocation.AddAsync(newAllocation);
                    }

                    

                    
                }

                await _context.SaveChangesAsync();
                return RedirectToPage("Index");
            }

            return Page();
        }


        //public async Task<IActionResult> OnPostAsync()
        //{

        //    Models.Users user = HttpContext.Session.GetCustomObjectFromSession<Models.Users>("LoggedUser");
        //    if (user == null)
        //    {
        //        RedirectToPage("/Login/Index");
        //    }


        //    if (ModelState.IsValid)
        //    {
        //            if (AssetAllocation.Id == 0)
        //            {
        //                var selAssetId = HttpContext.Request.Form["ddlAssetName"].ToString();
        //                AssetAllocation.AssetId = Convert.ToInt32(selAssetId);

        //                var selEmployeeId = HttpContext.Request.Form["ddlEmployeeName"].ToString();
        //                AssetAllocation.EmployeeId = Convert.ToInt32(selEmployeeId);
        //                AssetAllocation.AllocatedOn = DateTime.Now;
        //                AssetAllocation.Status = AssetStatus.Allocated;

        //                await _context.AssetAllocation.AddAsync(AssetAllocation);
        //                await _context.SaveChangesAsync();
        //                return RedirectToPage("Index");
        //            }
        //            else
        //            {
        //                var AssetsAllocationFromDB = await _context.AssetAllocation.FindAsync(AssetAllocation.Id);
        //                var selAssetId = HttpContext.Request.Form["ddlAssetName"].ToString();
        //                AssetsAllocationFromDB.AssetId = Convert.ToInt32(selAssetId);

        //                var selEmployeeId = HttpContext.Request.Form["ddlEmployeeName"].ToString();
        //                AssetsAllocationFromDB.EmployeeId = Convert.ToInt32(selEmployeeId);
        //                AssetsAllocationFromDB.AllocatedOn = DateTime.Now;
        //                AssetsAllocationFromDB.Status = AssetStatus.Allocated;

        //                await _context.SaveChangesAsync();
        //                return RedirectToPage("Index");
        //            }
        //    }
        //    return Page();
        //}
    }
}
