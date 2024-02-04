using CRM.Business;
using CRM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CRM.Pages.LeaveRecord
{
    public class DetailModel : PageModel
    {
        private readonly AssetDbContext _db;

        public DetailModel(AssetDbContext db)
        {
            _db = db;
        }

        public string LoggedUsername { get; set; }

        public Models.Leave Leave { get; set; }

        [BindProperty]
        public IList<Models.EmployeeMaster> EmployeeMaster { get; set; } = default!;

        public IList<Models.LeaveRecord> LeaveRecord { get; set; }

        public IList<Models.LeaveRecord> ApprovedLeaves { get; set; }

        public IList<Models.LeaveRecord> RejectedLeaves { get; set; }

        public int LeavesTaken { get; set; }

        public int TotalLeaves { get; set; }

        public int RemainingLeaves { get; set; }

        public int AdditionalLeaves { get; set; }

        public int EmployeeId { get; set; }

        public async Task<IActionResult> OnGet(int? employeeId, int year)
        {

            if (employeeId == null)
            {
                return NotFound();
            }
            EmployeeId = (int)employeeId;

            var employee = await _db.EmployeeMaster.FirstOrDefaultAsync(e => e.Id == EmployeeId);
            if (employee == null)
            {
                return NotFound(); // Employee not found, handle the error
            }

            EmployeeMaster = new List<Models.EmployeeMaster> { employee };
            Leave = await _db.Leave.FirstOrDefaultAsync(l => l.EmpId == employeeId && l.Year == year);
            

            if (Leave == null)
            {
                return NotFound();
            }

            // Extract the year from the Leave's FromDate
            int leaveYear = Leave.Year;

            LeaveRecord = await _db.LeaveRecord
            .Where(lr => lr.EmpId == EmployeeId && lr.FromDate.Year == leaveYear)
            .Include(lr => lr.Users)
            .ToListAsync();

            ApprovedLeaves = await _db.LeaveRecord.Where(lr => lr.EmpId == employeeId && lr.LeaveStatus == Status.approved && lr.FromDate.Year == year).ToListAsync();
            RejectedLeaves = await _db.LeaveRecord.Where(lr => lr.EmpId == employeeId && lr.LeaveStatus == Status.rejected && lr.FromDate.Year == year).ToListAsync();


            LeavesTaken = ApprovedLeaves.Sum(lr => lr.TotalLeaves);
            TotalLeaves = Leave.TotalLeaves;

            RemainingLeaves = TotalLeaves - LeavesTaken;
            AdditionalLeaves = RemainingLeaves < 0 ? -RemainingLeaves : 0;
            if (RemainingLeaves < 0)
            {
                RemainingLeaves = 0;
            }
            

            return Page();
        }
        public async Task<IActionResult> OnPostEditLeave(int leaveRecordId)
        {
            var leaveRecord = await _db.LeaveRecord.FindAsync(leaveRecordId);

            if (leaveRecord == null)
            {
                
                return RedirectToPage("Detail"); // Redirect back to the Detail page
            }

            return RedirectToPage("Create", new { leaveRecordId = leaveRecordId }); 
        }

    }
}