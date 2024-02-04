using CRM.Business;
using CRM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;

namespace CRM.Pages.SalarySheet
{
    public class createModel : PageModel
    {
        private readonly AssetDbContext _db;

        public createModel(AssetDbContext db)
        {
            _db = db;
        }
        [BindProperty(SupportsGet = true)]
        public int StructId { get; set; }

        [BindProperty]
        public int CurrentYear { get; set; }


        [BindProperty]
        public Models.SalarySheet SalarySheet { get; set; }

        [BindProperty]
        public IList<Models.Leave> Leave { get; set; } = default!;

        [BindProperty]
        public IList<Models.EmployeeMaster> EmployeeMaster { get; set; } = default!;

        [BindProperty]
        public IList<Models.LeaveRecord> LeaveRecord { get; set; } = default!;

        [BindProperty]
        public IList<Models.Users> Users { get; set; } = new List<Models.Users>();

        // List of month names
        public string[] MonthNames { get; } = new string[]
        {
            "January", "February", "March", "April", "May", "June",
            "July", "August", "September", "October", "November", "December"
        };

        [BindProperty]
        public IList<Models.SalaryMaster> SalaryMaster { get; set; }

        
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public decimal BaseSalary_M { get; set; }
        public decimal HRA_M { get; set; }
        public decimal SpecialAllowance { get; set; }
        public decimal EPF_M { get; set; }

        public long additionalBonus { get; set; }


        public async Task<IActionResult> OnGetAsync(int? SID, int structId)
        
        {
            Models.Users loggedInUser = HttpContext.Session.GetCustomObjectFromSession<Models.Users>("LoggedUser");
            if (loggedInUser == null)
            {
                return RedirectToPage("/Login/Index");
            }

            // Fetch the selected SalaryMaster record
            var SalaryMaster = await _db.SalaryMaster.FindAsync(structId);

            Models.EmployeeMaster employee = await _db.EmployeeMaster
                .FirstOrDefaultAsync(e => e.Id == SalaryMaster.EmpId);

            if (employee == null)
            {
                return NotFound();
            }

            CurrentYear = DateTime.Now.Year;
            EmployeeName = employee.EmployeeName;
            Designation = employee.Designation;

            // Fetch SalaryMaster records
            if (SID == null)
            {
                
                //Set the ApplicableYear
                
                BaseSalary_M = SalaryMaster.BaseSalary_M;
                HRA_M = SalaryMaster.HRA_M;
                SpecialAllowance = SalaryMaster.SpecialAllowance_M;
                EPF_M = SalaryMaster.EPF_M;
                additionalBonus = SalaryMaster.AssuredPayout;


                //create
                var grossSalary = BaseSalary_M + HRA_M + SpecialAllowance;
                SalarySheet = new Models.SalarySheet
                {
                    SalYear = CurrentYear,
                    EmpId = SalaryMaster.EmpId,
                    GrossSalary_M = (int)grossSalary,
                    assuredPayoutPaid = (int)additionalBonus,
                };

                

            }
            else
            {
                //update
                SalarySheet = await _db.SalarySheet.FindAsync(SID);
                BaseSalary_M = SalarySheet.BaseSalary_M;
                HRA_M = SalarySheet.HRA_M;
                SpecialAllowance = SalarySheet.SpecialAllowances_M;
                EPF_M = SalarySheet.EPF_M;
                additionalBonus = SalarySheet.assuredPayoutPaid;
                
                if (SalarySheet == null)
                {
                    return NotFound();
                }

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
            SalarySheet.CreatedBy = loggedInUser.Id;
            EmployeeMaster = await _db.EmployeeMaster.ToListAsync();
            var structId = StructId;
            SalarySheet.struct_Id = structId;
            var SalaryMaster = await _db.SalaryMaster.FindAsync(structId);

            var selectedEmpId = SalarySheet.EmpId;
            Models.EmployeeMaster employee = await _db.EmployeeMaster
                .FirstOrDefaultAsync(e => e.Id == SalarySheet.EmpId);

            // Explicitly set the properties from the form data to the SalarySheet object
            

            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (SalarySheet.IsEnablePT)
            {
                SalarySheet.Part_B_ProfessionalTax_Y = 2500;
            }
            else
            {
                SalarySheet.Part_B_ProfessionalTax_Y = 0;
             }
            if(SalarySheet.SID != 0) 
            {
                var existingRecord = await _db.SalarySheet.AsNoTracking().FirstOrDefaultAsync(s => s.SID == SalarySheet.SID);

                if (existingRecord != null)
                {
                    existingRecord.IsEnablePT = SalarySheet.IsEnablePT;
                    existingRecord.workingDays = SalarySheet.workingDays;
                    existingRecord.LOPDays = SalarySheet.LOPDays;   
                    existingRecord.PaidDays = SalarySheet.PaidDays;
                    existingRecord.GrossSalary_M = SalarySheet.GrossSalary_M;
                    existingRecord.NetSal = SalarySheet.NetSal;
                    existingRecord.TDS_M = SalarySheet.TDS_M;
                    existingRecord.Part_B_ProfessionalTax_M = SalarySheet.Part_B_ProfessionalTax_M;
                    existingRecord.TotalDeduction_M = SalarySheet.TotalDeduction_M;
                    existingRecord.isAssuredPayoutPaid = SalarySheet.isAssuredPayoutPaid;
                    existingRecord.assuredPayoutPaid = SalarySheet.assuredPayoutPaid;
                    existingRecord.BaseSalary_M = SalarySheet.BaseSalary_M;
                    existingRecord.SpecialAllowances_M = SalarySheet.SpecialAllowances_M;
                    existingRecord.HRA_M = SalarySheet.HRA_M;
                    existingRecord.EPF_M = SalarySheet.EPF_M;


                    
                    existingRecord.CreatedBy = loggedInUser.Id;
                    _db.Attach(existingRecord).State = EntityState.Modified;
                    await _db.SaveChangesAsync();
                    return RedirectToPage("Detail", new { structId });
                }
                
            }
            // Save the new SalarySheet record
            SalarySheet.CreatedOn = DateTime.Now;
            SalarySheet.BaseSalary_M = Convert.ToInt32(Request.Form["SalarySheet.BaseSalary_M"]);
            SalarySheet.HRA_M = Convert.ToInt32(Request.Form["SalarySheet.HRA_M"]);
            SalarySheet.SpecialAllowances_M = Convert.ToInt32(Request.Form["SalarySheet.SpecialAllowances_M"]);
            SalarySheet.EPF_M = Convert.ToInt32(Request.Form["SalarySheet.EPF_M"]);
            _db.SalarySheet.Add(SalarySheet);
            await _db.SaveChangesAsync();
            return RedirectToPage("Detail", new { structId });

        }
    }

}

// for professional taxes

////SalaryMaster.ProfessionalTax_Y = 2500m;
////SalaryMaster.ProfessionalTax_M = SalaryMaster.ProfessionalTax_Y / 12;
////existingRecord.ProfessionalTax_Y = SalaryMaster.ProfessionalTax_Y;
////existingRecord.ProfessionalTax_M = SalaryMaster.ProfessionalTax_Y / 12;

//for Income Tax 
//decimal ctcYearly = SalaryMaster.TotalCTC_Y;
//decimal incomeTax = CalculateIncomeTax(ctcYearly);
//SalaryMaster.TDS_Y = incomeTax;
//SalaryMaster.TDS_M = incomeTax / 12;

//private decimal CalculateIncomeTax(decimal ctcYearly)
//{
//    decimal taxableIncome = ctcYearly;

//    if (taxableIncome <= 250000)
//    {
//        return 0;
//    }
//    else if (taxableIncome <= 500000)
//    {
//        return 0.05m * (taxableIncome);
//    }
//    else if (taxableIncome <= 750000)
//    {
//        return 0.1m * (taxableIncome);
//    }
//    else if (taxableIncome <= 1000000)
//    {
//        return 0.15m * (taxableIncome);
//    }
//    else if (taxableIncome <= 1250000)
//    {
//        return 0.2m * (taxableIncome);
//    }
//    else if (taxableIncome <= 1500000)
//    {
//        return 0.25m * (taxableIncome);
//    }
//    else
//    {
//        return 0.3m * (taxableIncome);
//    }
//}
