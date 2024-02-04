using System;
using CRM.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Models
{
    public class SalaryMaster
    {
        // this model is used for creating and adding the the employee value in index page 
        [Key]
        public int Struct_ID { get; set; }

        
        public int EmpId { get; set; }
        [ForeignKey("EmpId")]
        public virtual EmployeeMaster? EmployeeMaster { get; set; }
        [Required]
        public long TotalCTC_Y { get; set; }
        public long TotalCTC_M { get; set; }
        
        [Required]
        public long BaseSalary_Y { get; set; }
        public long BaseSalary_M { get; set; }
        [Required]
        public long HRA_Y { get; set; }
        public long HRA_M { get; set; }
        [Required]
        public long SpecialAllowance_Y { get; set; }
        public long SpecialAllowance_M { get; set; }
        
        [Required]
        public long EPF_Y { get; set; }
        public long EPF_M { get; set; }

        [Required]
        public long AssuredPayout { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual Users? Users { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.Now;


        public bool IsEnableEPF { get; set; }

        [Required]
        public DateTime ApplicableFrom { get; set; }
        public DateTime ApplicableTo { get; set; }

    }
}
