using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using CRM.Business;

namespace CRM.Pages.LeaveRecord
{
    public class CreateModel : PageModel
    {
        private readonly AssetDbContext _context;
        public string loggedUsername = "";

        public CreateModel(AssetDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.LeaveRecord LeaveRecord { get; set; }

        [BindProperty]
        public IList<Models.EmployeeMaster> EmployeeMaster { get; set; } = default!;

        [BindProperty]
        public IList<Models.SalaryMaster> SalaryMaster { get; set; } = default!;

        [BindProperty]
        public IList<Models.Leave> Leave { get; set; } = default!;
        public int EmployeeId { get; set; }

        public async Task<IActionResult> OnGet(int? employeeId, int? leaveRecordId)
        {

            Models.Users user = HttpContext.Session.GetCustomObjectFromSession<Models.Users>("LoggedUser");
            if (user == null)
            {
                RedirectToPage("/Login/Index");
            }

            if (employeeId != null)
            {
                EmployeeId = (int)employeeId;
                
            }
            else
            {
                LeaveRecord = await _context.LeaveRecord.FirstOrDefaultAsync(e => e.LeaveRecordId == leaveRecordId);
                EmployeeId = LeaveRecord.EmpId;
            }

            if (leaveRecordId == null)
            {
                // This is the scenario when creating a new LeaveRecord
                var employee = await _context.EmployeeMaster.FirstOrDefaultAsync(e => e.Id == EmployeeId);
                if (employee == null)
                {
                    return NotFound(); // Employee not found, handle the error
                }

                // EmployeeMaster = new List<Models.EmployeeMaster> { employee }; // Add the employee to the list

                LeaveRecord = new Models.LeaveRecord
                {
                    EmployeeMaster = employee,
                    EmpId = EmployeeId,
                    FromDate = DateTime.Now.Date,
                    ToDate = DateTime.Now.Date,
                    TotalLeaves = 1,
                    LeaveStatus = Status.pending,
                    ApprovedBy = user.Id
                };
            }
            else
            {
                // This is the scenario when updating an existing LeaveRecord
                LeaveRecord = await _context.LeaveRecord.FirstOrDefaultAsync(u => u.LeaveRecordId == leaveRecordId);
                if (LeaveRecord == null || LeaveRecord.EmpId != EmployeeId)
                {
                    return NotFound();
                }

                // Fetch the EmployeeMaster for the LeaveRecord's EmpId
                var employee = await _context.EmployeeMaster.FirstOrDefaultAsync(e => e.Id == LeaveRecord.EmpId);
                if (employee == null)
                {
                    return NotFound(); // Employee not found, handle the error
                }

                EmployeeMaster = new List<Models.EmployeeMaster> { employee }; // Add the employee to the list
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? employeeId)
        {
            Models.Users user = HttpContext.Session.GetCustomObjectFromSession<Models.Users>("LoggedUser");
            if (user == null)
            {
                RedirectToPage("/Login/Index");
            }

            

            if (employeeId != null)
            {
                EmployeeId = (int)employeeId;
                LeaveRecord.ApprovedBy = user.Id;
                LeaveRecord.EmpId = EmployeeId;
                if (ModelState.IsValid)
                {
                    // Validate FromDate is before ToDate
                    if (LeaveRecord.FromDate > LeaveRecord.ToDate)
                    {
                        ModelState.AddModelError("LeaveRecord.ToDate", "ToDate should be after FromDate.");
                        EmployeeMaster = await _context.EmployeeMaster.ToListAsync();
                        return Page();
                    }

                    if (LeaveRecord.LeaveRecordId == 0)
                    {
                        // Add the new LeaveRecord
                        LeaveRecord.TotalLeaves = LeaveRecord.ToDate.Subtract(LeaveRecord.FromDate).Days + 1;
                        _context.LeaveRecord.Add(LeaveRecord);
                        await _context.SaveChangesAsync();
                    }
                    return RedirectToPage("Approval");

                }
            }
            else
            {
                int leaveRecordIdToUpdate = LeaveRecord.LeaveRecordId;
                EmployeeId = LeaveRecord.EmpId;

                if (ModelState.IsValid)
                {
                    if (LeaveRecord.LeaveRecordId != 0)
                    {
                        // Updating the existing LeaveRecord
                        var existingLeaveRecord = await _context.LeaveRecord.FindAsync(leaveRecordIdToUpdate);

                        if (existingLeaveRecord == null || existingLeaveRecord.EmpId != LeaveRecord.EmpId)
                        {
                            return NotFound();
                        }

                        // Update existingLeaveRecord properties
                        existingLeaveRecord.FromDate = LeaveRecord.FromDate;
                        existingLeaveRecord.ToDate = LeaveRecord.ToDate;
                        existingLeaveRecord.TotalLeaves = LeaveRecord.ToDate.Subtract(LeaveRecord.FromDate).Days + 1;
                        existingLeaveRecord.LeaveReason = LeaveRecord.LeaveReason;
                        existingLeaveRecord.LeaveStatus = LeaveRecord.LeaveStatus;  
                        existingLeaveRecord.ApprovedBy = user.Id;  

                        // Save changes
                        await _context.SaveChangesAsync();

                        return RedirectToPage("Approval");
                    }
                }

            }

            EmployeeMaster = await _context.EmployeeMaster.ToListAsync();
            return Page();
        }
    }
}





