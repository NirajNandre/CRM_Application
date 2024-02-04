using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Models
{
    public class EmployeeMaster
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [ForeignKey(nameof(EmployeeName))]

        [Required]
        public string Email { get; set; }

        [Required]
        public string EmployeeId { get; set; }
        
        [Required]
        public string DateOfJoining { get; set; }

        [Required]
        public string PrimarySkills { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        [Required]
        public int LastUpdatedBy { get; set; }
        [ForeignKey("LastUpdatedBy")]
        public virtual Users? Users { get; set; }

        [Required]
        public int ManagerId { get; set; }

        

        public bool IsActive { get; set; } = true;

        public string Designation { get; set; }

        public string BankName { get; set; }
        public long Acc_No { get; set; }

        public string pan { get; set; }

        public string UAN { get; set; }

    }
}
