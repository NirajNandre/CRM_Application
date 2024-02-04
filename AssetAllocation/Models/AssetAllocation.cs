using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Models
{
    public enum AssetStatus
    {
        Allocated,
        Deallocated

    }
    public class AssetAllocation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int AssetId { get; set; }
        
        [ForeignKey("AssetId")]
        public virtual AssetMaster? AssetMaster { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual EmployeeMaster? EmployeeMaster { get; set; }
        public DateTime AllocatedOn { get; set; }

        public DateTime DeAllocatedOn { get; set; }
        public AssetStatus Status { get; set; }

    }
}
