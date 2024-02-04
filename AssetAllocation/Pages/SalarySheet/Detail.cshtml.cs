using CRM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CRM.Pages.SalarySheet
{
    public class DetailModel : PageModel
    {
        private readonly AssetDbContext _db;

        public DetailModel(AssetDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public IList<Models.SalarySheet> SalarySheet { get; set; }

        public IList<Models.EmployeeMaster> EmployeeMaster { get; set; } = default!;

        public IList<Models.SalaryMaster> SalaryMaster { get; set; }

        public String EmployeeName { get; set; }

        

        public async Task<IActionResult> OnGet(int? structId)
        {
             SalarySheet = _db.SalarySheet
                .Include(sm => sm.SalaryMaster)
                .Include(e => e.EmployeeMaster)
        .Where(s => s.struct_Id == structId)
        .ToList();

            if (SalarySheet.Count > 0)
            {
                int empId = SalarySheet[0].EmpId;
                EmployeeMaster = await _db.EmployeeMaster
                    .Where(e => e.Id == empId)
                    .ToListAsync();

                if (EmployeeMaster.Count > 0)
                {
                    EmployeeName = EmployeeMaster[0].EmployeeName;
                }
            }

            return Page();
        }

    }
}
