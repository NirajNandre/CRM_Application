using System;
using CRM.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Models
{
    public enum Status
    {
        pending = 0,
        approved = 1,
        rejected = 2
    }
    public class LeaveRecord
    {
        [Key]
        public int LeaveRecordId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }

        [Required]
        
        public int EmpId { get; set; }

        [ForeignKey("EmpId")]
        public virtual EmployeeMaster? EmployeeMaster { get; set; }

        [Required]
        public int TotalLeaves { get; set; }

        [Required]
        public string LeaveReason { get; set; }

        [Required]
        public Status LeaveStatus { get; set; }


        [Required]
        public int ApprovedBy { get; set; }
        [ForeignKey("ApprovedBy")]
        public virtual Users? Users { get; set; }



        // Other properties
    }
}
