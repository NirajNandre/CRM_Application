using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CRM.Business;
using CRM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CRM.Pages.LeaveRecord
{
    public class ApprovalModel : PageModel
    {
        private readonly AssetDbContext _context;

        public ApprovalModel(AssetDbContext context)
        {
            _context = context;
        }

        public int LoggedInUserId { get; set; }
        public IList<Models.LeaveRecord> PendingLeaveRecords { get; set; }

        private async Task<List<int>> GetManagedEmployeesIds(int loggedInUserId)
        {
            var managedEmployees = await _context.EmployeeMaster
                .Where(emp => emp.ManagerId == loggedInUserId)
                .Select(emp => emp.Id)
                .ToListAsync();

            return managedEmployees;
        }

        public async Task<IActionResult> OnGet()
        {
            // Get the LoggedInUser's ID
            Models.Users user = HttpContext.Session.GetCustomObjectFromSession<Models.Users>("LoggedUser");
            if (user == null)
            {
                return RedirectToPage("/Login/Index");
            }

            LoggedInUserId = user.Id;

            List<int> managedEmployeeIds = await GetManagedEmployeesIds(LoggedInUserId);

            // Fetch the pending LeaveRecords of those employees
            PendingLeaveRecords = await _context.LeaveRecord
                .Where(lr => managedEmployeeIds.Contains(lr.EmpId) && lr.LeaveStatus == Status.pending)
                .Include(lr => lr.EmployeeMaster)
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int leaveRecordId, int approvalStatus)
        {
            Models.Users user = HttpContext.Session.GetCustomObjectFromSession<Models.Users>("LoggedUser");
            if (user == null)
            {
                return RedirectToPage("/Login/Index");
            }

            // Find the LeaveRecord to update
            var leaveRecord = await _context.LeaveRecord.FindAsync(leaveRecordId);

            if (leaveRecord == null)
            {
                return RedirectToPage("Approval");
            }
            else
            {
                // Update the approval status based on the user's action
                if (approvalStatus == 1)
                {
                    leaveRecord.LeaveStatus = Status.approved;
                    leaveRecord.ApprovedBy = user.Id;
                }
                else if (approvalStatus == 2)
                {
                    leaveRecord.LeaveStatus = Status.rejected;
                    leaveRecord.ApprovedBy = user.Id; // Set ApprovedBy to null when rejected
                }

                await _context.SaveChangesAsync();
            }

            // Redirect back to the Approval page
            return RedirectToPage("Approval");
        }
    }
}


