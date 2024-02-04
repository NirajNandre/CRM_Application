using CRM.Business;
using CRM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Text.RegularExpressions;

namespace CRM.Pages.Leaves
{
    public class IndexModel : PageModel
    {
        private readonly AssetDbContext _db;

        public IndexModel(AssetDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public IList<Models.EmployeeMaster> EmployeeMaster { get; set; } = default!;

        [BindProperty]
        public IList<Models.Users> Users { get; set; }

        [BindProperty]
        public IList<Models.Leave> Leave { get; set; } = default!;
        [BindProperty]
        public IList<Models.LeaveRecord> LeaveRecord { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            Models.Users loggedInUser = HttpContext.Session.GetCustomObjectFromSession<Models.Users>("LoggedUser");
            if (loggedInUser == null)
            {
                return RedirectToPage("/Login/Index");
            }

            int loggedInManagerId = loggedInUser.Id;

            var loggedInUserEmployee = await _db.EmployeeMaster.FirstOrDefaultAsync(e => e.ManagerId == loggedInUser.Id);
            if (loggedInUserEmployee == null || loggedInUserEmployee.ManagerId == null)
            {
                // If the logged-in user is not a manager, return an empty list of leaves.
                return Page();
            }

            // Fetch employee IDs for employees managed by the logged-in manager
            var managedEmployeeIds = await _db.EmployeeMaster
                .Where(e => e.ManagerId == loggedInManagerId)
                .Select(e => e.Id)
                .ToListAsync();

            Leave = await _db.Leave
                .Include(l => l.EmployeeMaster)
                .Where(l => managedEmployeeIds.Contains(l.EmpId))
                .ToListAsync();

            // Fetch LeaveRecords for the managed employees
            var leaveRecords = await _db.LeaveRecord
                .Where(lr => managedEmployeeIds.Contains(lr.EmpId) && lr.LeaveStatus == Status.approved)
                .GroupBy(lr => new { lr.EmpId, lr.FromDate.Year }) // Group by EmpId and Year
                .Select(g => new { EmpId = g.Key.EmpId, Year = g.Key.Year, LeavesTaken = g.Sum(lr => lr.TotalLeaves) })
                .ToListAsync();

            foreach (var leave in Leave)
            {
                var empId = leave.EmpId;
                var year = leave.Year;
                var leaveRecord = leaveRecords.FirstOrDefault(lr => lr.EmpId == empId && lr.Year == year);
                if (leaveRecord != null)
                {
                    leave.LeavesTaken = leaveRecord.LeavesTaken;
                    leave.RemainingLeaves = leave.TotalLeaves - leave.LeavesTaken;
                    leave.AdditionalLeaves = leave.RemainingLeaves < 0 ? -leave.RemainingLeaves : 0;

                    if (leave.RemainingLeaves < 0)
                    {
                        leave.RemainingLeaves = 0;
                    }
                }
            }
            await _db.SaveChangesAsync();   

            return Page();
        }
    }
}










