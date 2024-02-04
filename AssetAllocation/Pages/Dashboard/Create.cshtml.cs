using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CRM.Pages.AssetTypes
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly AssetDbContext _context;

        public CreateModel(AssetDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGet(int? id)
        {
            AssetTypes = new Models.AssetTypes();
            if (id == null)
            {
                //create
                return Page();
            }

#pragma warning disable CS8601 // Possible null reference assignment.
            AssetTypes = await _context.AssetTypes.FirstOrDefaultAsync(u => u.Id == id);
#pragma warning restore CS8601 // Possible null reference assignment.
            if (AssetTypes == null)
            {
                return NotFound();
            }
            return Page();
        }

        [BindProperty]
        public Models.AssetTypes AssetTypes { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid || _context.AssetTypes == null || AssetTypes == null)
            {
                return Page();
            }
            else
            {
                if (AssetTypes.Id == 0)
                {
                    _context.AssetTypes.Add(AssetTypes);
                    await _context.SaveChangesAsync();

                    return RedirectToPage("./Index");
                }
                else
                {
                    _context.Attach(AssetTypes).State = EntityState.Modified;
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AsseTypesExists(AssetTypes.Id))
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
        }
        private bool AsseTypesExists(int id)
        {
            return (_context.AssetTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
