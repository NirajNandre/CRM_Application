using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Models
{
    public class SalarySheet
    {
        [Key]
        public int SID { get; set; }

        [Required]
        public int EmpId { get; set; }
        [ForeignKey("EmpId")]
        public virtual EmployeeMaster? EmployeeMaster { get; set; }

        [Required]
        public int struct_Id { get; set; }
        [ForeignKey("struct_Id")]
        public virtual SalaryMaster? SalaryMaster { get; set; }

        [Required]
        public int SalYear { get; set; }

        public string SalMonth { get; set; }


        [Required]
        public long NetSal { get; set; }



        [Required]

        public long GrossSalary_M { get; set; }


        public long TotalDeduction_M { get; set; }

        [Required]

        public long TDS_M { get; set;}


        [Required]
        public long Part_B_ProfessionalTax_Y { get; set; }
        public long Part_B_ProfessionalTax_M { get; set; }

        [Required]

        public long TaxableIncome_M { get; set; }


        [Required]
        public int workingDays { get; set; }

        public int PaidDays { get; set; }

        public int LOPDays { get; set; }

        [Required]
        public int assuredPayoutPaid { get; set; }

        public Boolean isAssuredPayoutPaid { get; set; }

        public long BaseSalary_M { get; set; }

        public long HRA_M { get; set; }
        public long SpecialAllowances_M { get; set; }
        public long EPF_M { get; set; }


        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        [Required]
        public int CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual Users? Users { get; set; }

        public bool IsEnablePT { get; set; }


    }
}
