using CRM.Business;
using CRM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CRM.Pages.SalaryMaster
{
    public class IndexModel : PageModel
    {
          private readonly AssetDbContext _context;

            public IndexModel(AssetDbContext context)
            {
            _context = context;
             }

            public IList<Models.SalaryMaster> SalaryMasterRecords { get; set; }

        //public async Task<IActionResult> OnGetAsync()
        //{
        //Models.Users loggedInUser = HttpContext.Session.GetCustomObjectFromSession<Models.Users>("LoggedUser");
        //if (loggedInUser == null)
        //{
        //    RedirectToPage("/Login/Index");
        //}

        //int loggedInManagerId = loggedInUser.Id;

        //var today = DateTime.Now.Date;

        //var managedEmployeeIds = await _context.EmployeeMaster
        //    .Where(e => e.ManagerId == loggedInManagerId)
        //    .Select(e => e.Id)
        //    .ToListAsync();

        //SalaryMasterRecords = await _context.SalaryMaster
        //    .Include(s => s.EmployeeMaster)
        //    .Where(s => managedEmployeeIds.Contains(s.EmpId))
        //    .OrderByDescending(s => s.ApplicableFrom.Year) // Order by descending to show recent records first
        //    .ToListAsync();
        //foreach (var Structure in SalaryMasterRecords)
        //{
        //    Structure.IsActive = today >= Structure.ApplicableFrom.Date && today <= Structure.ApplicableTo.Date;
        //}


        //await _context.SaveChangesAsync();
        //return Page();


        //}
        public async Task<IActionResult> OnGetAsync()
        {
            Models.Users loggedInUser = HttpContext.Session.GetCustomObjectFromSession<Models.Users>("LoggedUser");
            if (loggedInUser == null)
            {
                RedirectToPage("/Login/Index");
            }

            int loggedInManagerId = loggedInUser.Id;

            var today = DateTime.Now.Date;

            // Fetch all records from SalaryMaster
            SalaryMasterRecords = await _context.SalaryMaster
                .Include(s => s.EmployeeMaster)
                .OrderByDescending(s => s.ApplicableFrom.Year)
                .ToListAsync();

            // Update the IsActive property for each record
            foreach (var structure in SalaryMasterRecords)
            {
                structure.IsActive = today >= structure.ApplicableFrom.Date && today <= structure.ApplicableTo.Date;
            }

            // Separate active and inactive records
            var activeRecords = SalaryMasterRecords.Where(s => s.IsActive).ToList();
            var inactiveRecords = SalaryMasterRecords.Where(s => !s.IsActive).ToList();

            // Concatenate the lists to display active records first
            SalaryMasterRecords = activeRecords.Concat(inactiveRecords).ToList();

            return Page();
        }


    }
}

