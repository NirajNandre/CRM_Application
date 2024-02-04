using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Models
{
    public enum AssetCondition
    {
        Good = 0,
        Damaged = 1,
        Lost = 2
    }

    public class AssetMaster
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ManufacturerNumber { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string TeamverseAssetNumber { get; set; }
        [Required]
        public int AssetTypeId { get; set; }
        [ForeignKey("AssetTypeId")]
        public virtual AssetTypes? AssetTypes { get; set; }
        [Required]
        public DateTime PurchasedOn { get; set; } = DateTime.Now;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public AssetCondition Condition { get; set; }
        
        public bool IsDeleted { get; set; }
        [Required]
        public int LastUpdatedBy { get; set; }
        [ForeignKey("LastUpdatedBy")]
        public virtual Users? Users { get; set; }

    }
}
