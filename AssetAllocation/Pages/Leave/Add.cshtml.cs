using CRM.Business;
using CRM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CRM.Pages.Leave
{
    public class AddModel : PageModel
    {
        private readonly AssetDbContext _context;

        public AddModel(AssetDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public int? SelectedManagerId { get; set; }
        

        [BindProperty]
        public IList<Models.LeaveRecord> LeaveRecord { get; set; } = default!;
        [BindProperty]
        public Models.Leave Leave { get; set; } = default!;


        [BindProperty]
        public IList<Models.EmployeeMaster> EmployeeMaster { get; set; } = default!;

        [BindProperty]
        public IList<Models.Users> Users { get; set; } = new List<Models.Users>();

        public async Task<IActionResult> OnGet(int? id)
        {
            Models.Users loggedInUser = HttpContext.Session.GetCustomObjectFromSession<Models.Users>("LoggedUser");
            if (loggedInUser == null)
            {
                return RedirectToPage("/Login/Index");
            }

            if (id == null)
            {
                Leave = new Models.Leave();
                if (_context.EmployeeMaster != null)
                {
                    EmployeeMaster = await _context.EmployeeMaster.ToListAsync();

                }
                
            }
            else
            {
                Leave = await _context.Leave.FindAsync(id);
                if (Leave == null)
                {
                    return NotFound();
                }

                EmployeeMaster = await _context.EmployeeMaster.ToListAsync();
            }


            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            Models.Users loggedInUser = HttpContext.Session.GetCustomObjectFromSession<Models.Users>("LoggedUser");
            if (loggedInUser == null)
            {
                return RedirectToPage("/Login/Index");
            }
            int loggedInManagerId = loggedInUser.Id;

            var selEmployeeId = HttpContext.Request.Form["ddlEmployeeName"].ToString();
            int employeeId = Convert.ToInt32(selEmployeeId);
            int year = Convert.ToInt32(Leave.Year);

            // Fetch managed employee IDs for the logged-in manager
            var managedEmployeeIds = await _context.EmployeeMaster
                .Where(e => e.ManagerId == loggedInManagerId)
                .Select(e => e.Id)
                .ToListAsync();


            var existingLeave = await _context.Leave.FirstOrDefaultAsync(l => l.EmpId == employeeId && l.Year == year && managedEmployeeIds.Contains(l.EmpId));
            if (existingLeave != null)
            {
                var leaveRecords = await _context.LeaveRecord.Where(lr => lr.EmpId == employeeId).ToListAsync();
                existingLeave.LeavesTaken = LeaveRecord.Where(lr => lr.FromDate.Year == year).Sum(lr => lr.TotalLeaves);
                existingLeave.TotalLeaves = Leave.TotalLeaves;
                existingLeave.RemainingLeaves = existingLeave.TotalLeaves - existingLeave.LeavesTaken;
                existingLeave.AdditionalLeaves = Leave.AdditionalLeaves;
                existingLeave.Year = year;
                existingLeave.Manager = loggedInManagerId;

                _context.Leave.Update(existingLeave);
            }
            else
            {
                // Create a new Leave record for the new year
                var newLeave = new Models.Leave
                {
                    EmpId = employeeId,
                    Manager = loggedInManagerId,
                    TotalLeaves = Leave.TotalLeaves,
                    LeavesTaken = LeaveRecord.Where(lr => lr.FromDate.Year == year).Sum(lr => lr.TotalLeaves),
                    RemainingLeaves = Leave.TotalLeaves - Leave.LeavesTaken,
                    Year = year,
                };

                await _context.Leave.AddAsync(newLeave);
               
            }

            await _context.SaveChangesAsync();
            EmployeeMaster = _context.EmployeeMaster.ToList();

            return RedirectToPage("Index");
        }
    }
}
