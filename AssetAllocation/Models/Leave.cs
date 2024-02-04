using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Models
{
    public class Leave
    {
        [Key]
        public int LeaveId { get; set; }

        [Required]
        public int? Manager { get; set; }
        [ForeignKey("Manager")]

        [Required]
        public int EmpId { get; set; }
        [ForeignKey("EmpId")]
        public virtual EmployeeMaster? EmployeeMaster { get; set; }

        [Required]
        public int Year { get; set; }  

        [Required]
        public int TotalLeaves { get; set; }

        [Required]
        public int LeavesTaken { get; set; }

        [Required]
        public int RemainingLeaves { get; set; }

        [Required]
        public int AdditionalLeaves { get; set; }

        

    }
}
