using CRM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CRM.Pages.SalarySheet
{
    public class printModel : PageModel
    {

        private readonly AssetDbContext _db;

        public printModel(AssetDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Models.SalarySheet SalarySheet { get; set; }

        [BindProperty]
        public IList<Models.EmployeeMaster> EmployeeMaster { get; set; } = default!;

        [BindProperty]
        public IList<Models.SalaryMaster> SalaryMaster { get; set; }


        //properties
        public string employeeName { get; set; }
        public string Designation { get; set; }

        public string employeeId { get; set; }
        public string DateOfJoining { get; set; }

        public long TotalEarning { get; set; }

        public long TotalDeduction { get; set; }

        public string ConvertToWords(int num)
        {
            string[] one = new string[]{ "", "One ", "Two ", "Three ", "Four ",
            "Five ", "Six ", "Seven ", "Eight ",
            "Nine ", "Ten ", "Eleven ", "Twelve ",
            "Thirteen ", "Fourteen ", "Fifteen ",
            "Sixteen ", "Seventeen ", "Eighteen ",
            "Nineteen " };

            string[] ten = new string[]{ "", "", "Twenty ", "Thirty ", "Forty ",
            "Fifty ", "Sixty ", "Seventy ", "Eighty ",
            "Ninety " };

            string NumToWords(int n, string s)
            {
                string str = "";
                // if n is more than 19, divide it
                if (n > 19)
                    str += ten[n / 10] + one[n % 10];
                else
                    str += one[n];

                // if n is non-zero
                if (n > 0)
                    str += s;

                return str;
            }

            // stores word representation of given number n
            string outStr = "";

            // handles digits at ten millions and hundred
            // millions places (if any)
            outStr += NumToWords(num / 10000000, "Crore ");

            // handles digits at hundred thousands and one
            // millions places (if any)
            outStr += NumToWords((num / 100000) % 100, "Lakh ");

            // handles digits at thousands and tens thousands
            // places (if any)
            outStr += NumToWords((num / 1000) % 100, "Thousand ");

            // handles digit at hundreds places (if any)
            outStr += NumToWords((num / 100) % 10, "Hundred ");

            if (num > 100 && num % 100 > 0)
                outStr += "and ";

            // handles digits at ones and tens places (if any)
            outStr += NumToWords(num % 100, "");

            // Handling the num=0 case
            if (outStr == "")
                outStr = "Zero";

            return outStr;
        }


        public async Task<IActionResult> OnGetAsync(int? SID, int structId)
        {
            if(SID == null)
            {

                string script = "<script>alert('Content not loaded!');</script>";
                Response.WriteAsync(script);

            }

            //fetching all three Models
            var SalaryMaster = await _db.SalaryMaster.FindAsync(structId);

            Models.EmployeeMaster employee = await _db.EmployeeMaster
                .FirstOrDefaultAsync(e => e.Id == SalaryMaster.EmpId);

            employeeName = employee.EmployeeName;
            Designation = employee.Designation;
            employeeId = employee.EmployeeId;
            DateOfJoining = employee.DateOfJoining;

            
            SalarySheet = await _db.SalarySheet.FindAsync(SID);
            if (SalarySheet == null)
            {
                return NotFound();
            }

            TotalEarning = SalarySheet.GrossSalary_M + SalarySheet.assuredPayoutPaid;

            TotalDeduction = SalarySheet.TotalDeduction_M + SalarySheet.TDS_M;

            int netSal = Convert.ToInt32(SalarySheet.NetSal);

            string netSalInWords = ConvertToWords(netSal);
            ViewData["NetSalInWords"] = netSalInWords + " Rupees Only";

            return Page();  
        }
    }
}
