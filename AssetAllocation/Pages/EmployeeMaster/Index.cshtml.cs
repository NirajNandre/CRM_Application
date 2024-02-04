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

namespace CRM.Pages.EmployeeMaster
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

        public string GetManagerName(int managerId)
        {
            var manager = _context.EmployeeMaster.FirstOrDefault(e => e.Id == managerId);
            return manager != null ? manager.EmployeeName : "N/A";
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Models.Users user = HttpContext.Session.GetCustomObjectFromSession<Models.Users>("LoggedUser");
            if (_context.EmployeeMaster != null)
            {
                EmployeeMaster = await _context.EmployeeMaster
                    .Include(e => e.Users)
                    .ToListAsync();

                // Set LastUpdatedBy for each EmployeeMaster
                foreach (var employee in EmployeeMaster)
                {
                    employee.LastUpdatedBy = user.Id;
                }

                return Page();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var employee = await _context.EmployeeMaster.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                _context.EmployeeMaster.Remove(employee);
                await _context.SaveChangesAsync();

                return RedirectToPage("Index");

            }
        }

     

    }
}
