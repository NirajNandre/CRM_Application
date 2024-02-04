using CRM.Business;
using CRM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;


namespace CRM.Pages.SalaryMaster
{
    public class createModel : PageModel
    {
        
        private readonly AssetDbContext _context;

        public createModel(AssetDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public int? SelectedManagerId { get; set; }


        [BindProperty]
        public Models.SalaryMaster SalaryMaster { get; set; }


        [BindProperty]
        public IList<Models.EmployeeMaster> EmployeeMaster { get; set; } = default!;

        [BindProperty]
        public IList<Models.Users> Users { get; set; } = new List<Models.Users>();


        public string EmployeeName { get; set; }
        public async Task<IActionResult> OnGet(int? struct_Id)
        {
            Models.Users loggedInUser = HttpContext.Session.GetCustomObjectFromSession<Models.Users>("LoggedUser");
            if (loggedInUser == null)
            {
                return RedirectToPage("/Login/Index");
            }
            EmployeeMaster = await _context.EmployeeMaster.ToListAsync();

            if (struct_Id == null)
            {
                //create
                SalaryMaster = new Models.SalaryMaster();
                SalaryMaster.ApplicableFrom = DateTime.Now;
                SalaryMaster.ApplicableTo = DateTime.Now;
                
                if (_context.EmployeeMaster != null)
                {
                    EmployeeMaster = await _context.EmployeeMaster.ToListAsync();

                }
                

            }
            else
            {
                //update
                SalaryMaster = await _context.SalaryMaster.FindAsync(struct_Id);
                Models.EmployeeMaster employee = await _context.EmployeeMaster
               .FirstOrDefaultAsync(e => e.Id == SalaryMaster.EmpId);
                EmployeeName = employee.EmployeeName;

                if (SalaryMaster == null)
                {
                    return NotFound();
                }

                EmployeeMaster = await _context.EmployeeMaster.ToListAsync();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
           
            EmployeeMaster = await _context.EmployeeMaster.ToListAsync();
            Models.Users user = HttpContext.Session.GetCustomObjectFromSession<Models.Users>("LoggedUser");
            if (user == null)
            {
                return RedirectToPage("/Login/Index");
            }

            SalaryMaster.CreatedBy = user.Id;


            ////Calculate all the yearly values

            //SalaryMaster.BaseSalary_Y = 0.4m * SalaryMaster.TotalCTC_Y;
            //SalaryMaster.HRA_Y = 0.4m * SalaryMaster.BaseSalary_Y;
            //SalaryMaster.SpecialAllowance_Y = 0.3m * SalaryMaster.TotalCTC_Y;




            //// Calculate EPF contribution if enabled
            //decimal epfContribution = SalaryMaster.IsEnableEPF ? 0.12m * SalaryMaster.BaseSalary_Y : 0;
            var newApplicableFrom = SalaryMaster.ApplicableFrom;
            var newApplicableTo = SalaryMaster.ApplicableTo;

            

            // Check if there's a record for the same employee with overlapping date range


            if (SalaryMaster.Struct_ID == 0)
            {
                // Add a new record
                SalaryMaster.IsActive = true;
                SalaryMaster.EmpId = int.Parse(Request.Form["ddlEmployeeName"]);
                var overlappingRecord = await _context.SalaryMaster
                .FirstOrDefaultAsync(s => s.EmpId == SalaryMaster.EmpId
                    && ((newApplicableFrom >= s.ApplicableFrom && newApplicableFrom <= s.ApplicableTo)
                        || (newApplicableTo >= s.ApplicableFrom && newApplicableTo <= s.ApplicableTo)
                        || (newApplicableFrom <= s.ApplicableFrom && newApplicableTo >= s.ApplicableTo)));

                if (overlappingRecord != null)
                {
                    // A record with overlapping date range already exists
                    ModelState.AddModelError(string.Empty, "A salary structure with overlapping date range already exists.");
                    EmployeeMaster = await _context.EmployeeMaster.ToListAsync();
                    return Page();
                }

                _context.SalaryMaster.Add(SalaryMaster);
            }
            else
            {
                // Update existing record
                var existingRecord = _context.SalaryMaster.FirstOrDefault(s => s.EmpId == SalaryMaster.EmpId && s.ApplicableTo.Year == SalaryMaster.ApplicableTo.Year);
                if (existingRecord != null)
                {
                    
                    existingRecord.EmpId = SalaryMaster.EmpId;
                    existingRecord.CreatedBy = user.Id; // Replace with actual user
                    existingRecord.CreatedOn = DateTime.Now;
                    existingRecord.TotalCTC_Y = SalaryMaster.TotalCTC_Y;
                    existingRecord.TotalCTC_M = SalaryMaster.TotalCTC_Y /12 ;
                    existingRecord.BaseSalary_Y = SalaryMaster.BaseSalary_Y;
                    existingRecord.BaseSalary_M = SalaryMaster.BaseSalary_Y /12 ;
                    existingRecord.HRA_Y = SalaryMaster.HRA_Y;
                    existingRecord.HRA_M = SalaryMaster.HRA_Y / 12 ;
                    existingRecord.SpecialAllowance_Y = SalaryMaster.SpecialAllowance_Y;
                    existingRecord.SpecialAllowance_M = SalaryMaster.SpecialAllowance_M;
                    existingRecord.AssuredPayout = SalaryMaster.AssuredPayout;
                    existingRecord.IsEnableEPF = SalaryMaster.IsEnableEPF;
                    existingRecord.EPF_M = SalaryMaster.EPF_M;
                    existingRecord.EPF_Y = SalaryMaster.EPF_Y;
                    existingRecord.ApplicableFrom = newApplicableFrom;
                    existingRecord.ApplicableTo = newApplicableTo;

                    if (DateTime.Now.Date >= newApplicableFrom && DateTime.Now.Date <= newApplicableTo)
                    {
                        existingRecord.IsActive = true;
                    }


                    _context.SalaryMaster.Update(existingRecord);
                }
            }
            
            if (!ModelState.IsValid)
            {
                // If ModelState is not valid, re-populate the EmployeeMaster list and return the page
                EmployeeMaster = await _context.EmployeeMaster.ToListAsync();
                return Page();
            }


            await _context.SaveChangesAsync();
            EmployeeMaster = await _context.EmployeeMaster.ToListAsync();
            return RedirectToPage("./Index");
        }
    }
}
