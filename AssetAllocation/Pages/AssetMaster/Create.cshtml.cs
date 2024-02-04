using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using CRM.Models;
using CRM.Business;

namespace CRM.Pages.AssetMaster
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
        [BindProperty]
        public Models.AssetMaster AssetMaster { get; set; } = default!;
        public async Task<IActionResult> OnGet(int? id)
        {
            AssetMaster = new Models.AssetMaster();
            Models.Users user = HttpContext.Session.GetCustomObjectFromSession<Models.Users>("LoggedUser");
            if (user == null)
            {
                RedirectToPage("/Login/Index");
            }
            loggedUsername = user.UserName;
            if (id == null)
            {
                //create         
                ViewData["AssetTypeId"] = new SelectList(_context.AssetTypes, "Id", "Name");
                ViewData["LastUpdatedBy"] = new SelectList(_context.Users.Where(u => u.UserName == loggedUsername), "Id", "FullName");
                return Page();
            }

#pragma warning disable CS8601 // Possible null reference assignment.
            AssetMaster = await _context.AssetMaster.FirstOrDefaultAsync(u => u.Id == id);
#pragma warning restore CS8601 // Possible null reference assignment.
            if (AssetMaster == null)
            {
                return NotFound();
            }

            ViewData["AssetTypeId"] = new SelectList(_context.AssetTypes, "Id", "Name");
            ViewData["LastUpdatedBy"] = new SelectList(_context.Users.Where(u => u.UserName == loggedUsername), "Id", "FullName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["AssetTypeId"] = new SelectList(_context.AssetTypes, "Id", "Name");
            AssetMaster.AssetTypes = await _context.AssetTypes.FindAsync(AssetMaster.AssetTypes);
            Models.Users user = HttpContext.Session.GetCustomObjectFromSession<Models.Users>("LoggedUser");
            if (user == null)
            {
                RedirectToPage("/Login/Index");
            }
            loggedUsername = user.UserName;
            int LastUpdatedBy = _context.Users.Where(u => u.UserName == loggedUsername).Select(u => u.Id).FirstOrDefault();
            AssetMaster.Users = await _context.Users.FindAsync(LastUpdatedBy);

            if (ModelState.IsValid)
            {
                if (AssetMaster.Id == 0)
                {
                    var manufacturerno = _context.AssetMaster.Where(f => f.ManufacturerNumber == AssetMaster.ManufacturerNumber).FirstOrDefault();
                    if (manufacturerno != null)
                    {
                        ViewData["ManufacturerNoExistMessage"] = "Manufacturer Number is already Taken!";

                        return Page();

                    }

                    var asset = _context.AssetMaster.Where(f => f.TeamverseAssetNumber == AssetMaster.TeamverseAssetNumber).FirstOrDefault();
                    if (asset != null)
                    {
                        ViewData["TVANExistMessage"] = "TeamverseAssetNumber is already Taken!";

                        return Page();

                    }
                    AssetMaster.IsDeleted = false;  
                    await _context.AssetMaster.AddAsync(AssetMaster);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("Index");
                }
                else
                {
                    _context.Attach(AssetMaster).State = EntityState.Modified;
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AssetMasterExists(AssetMaster.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                    return RedirectToPage("./Index");

                }
            }
            else
            {
                return Page();
            }
        }
        private bool AssetMasterExists(int id)
        {
            return (_context.AssetMaster?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
