using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography.X509Certificates;
using CRM.Models;
using CRM.Business;
using CRM.Migrations;

namespace CRM.Pages.EmployeeMaster
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
        public Models.EmployeeMaster EmployeeMaster { get; set; } = default!;

        public int SelectedManagerId { get; set; }

        public List<SelectListItem> ManagerOptions { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            EmployeeMaster = new Models.EmployeeMaster();
            Models.Users user = HttpContext.Session.GetCustomObjectFromSession<Models.Users>("LoggedUser");

            if (user == null)
            {
                return RedirectToPage("/Login/Index");
            }

            loggedUsername = user.FullName;

            ManagerOptions = await _context.EmployeeMaster
            .Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.EmployeeName
            })
            .ToListAsync();

            ManagerOptions.Insert(0, new SelectListItem { Value = "", Text = "Select", Selected = (id == null) }); // Add "Select" option for new records

            if (id == null)
            {
                //create
                ViewData["LastUpdatedBy"] = new SelectList(_context.Users.Where(u => u.FullName == loggedUsername), "Id", "FullName");
                return Page();
            }
           

            EmployeeMaster = await _context.EmployeeMaster.FirstOrDefaultAsync(u => u.Id == id);
            if (EmployeeMaster == null)
            {
                return NotFound();
            }

            SelectedManagerId = EmployeeMaster.ManagerId;

            ViewData["LastUpdatedBy"] = new SelectList(_context.Users.Where(u => u.FullName == loggedUsername), "Id", "FullName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Models.Users user = HttpContext.Session.GetCustomObjectFromSession<Models.Users>("LoggedUser");
            if (user == null)
            {
                return RedirectToPage("/Login/Index");
            }
            loggedUsername = user.FullName;
            int LastUpdatedBy = _context.Users.Where(u => u.FullName == loggedUsername).Select(u => u.Id).FirstOrDefault();
            EmployeeMaster.Users = await _context.Users.FindAsync(LastUpdatedBy);


            if (ModelState.IsValid)
            {
                EmployeeMaster.LastUpdatedBy = LastUpdatedBy;
                if (EmployeeMaster.Id == 0)
                {
                    var employee = _context.EmployeeMaster.Where(f => f.EmployeeId == EmployeeMaster.EmployeeId).FirstOrDefault();
                    if (employee != null)
                    {
                        ViewData["EmpIdExistMessage"] = "Employee Id is already Exists!";
                        ManagerOptions = await _context.EmployeeMaster
                            .Where(e => e.IsActive)
                            .Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.EmployeeName })
                            .ToListAsync();
                        return Page();
                    }

                    await _context.EmployeeMaster.AddAsync(EmployeeMaster);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("Index");
                }
                else
                {
                    
                    var existingEmployee = await _context.EmployeeMaster
                        .Include(e => e.Users)  
                        .FirstOrDefaultAsync(e => e.Id == EmployeeMaster.Id);

                    if (existingEmployee == null)
                    {
                        return NotFound();
                    }

                    // Update the existing record with new values
                    existingEmployee.EmployeeName = EmployeeMaster.EmployeeName;
                    existingEmployee.Email = EmployeeMaster.Email;
                    existingEmployee.DateOfJoining = EmployeeMaster.DateOfJoining;
                    existingEmployee.PrimarySkills = EmployeeMaster.PrimarySkills;
                    existingEmployee.ManagerId = EmployeeMaster.ManagerId;
                    existingEmployee.IsActive = EmployeeMaster.IsActive;
                    existingEmployee.Designation = EmployeeMaster.Designation;
                    existingEmployee.BankName = EmployeeMaster.BankName;
                    existingEmployee.Acc_No = EmployeeMaster.Acc_No;
                    existingEmployee.pan = EmployeeMaster.pan;
                    existingEmployee.UAN = EmployeeMaster.UAN;
                    existingEmployee.Users = EmployeeMaster.Users; 

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!EmployeeMasterExists(EmployeeMaster.Id))
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
                ManagerOptions = await _context.EmployeeMaster
                    .Where(e => e.IsActive)
                    .Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.EmployeeName })
                    .ToListAsync();
                return Page();
            }
        }

        private bool EmployeeMasterExists(int id)
        {
            return (_context.EmployeeMaster?.Any(e => e.Id == id)).GetValueOrDefault();
        }


    }
}
