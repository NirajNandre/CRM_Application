using System.ComponentModel.DataAnnotations;

namespace CRM.Models
{
    public class AssetTypes
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Prefix { get; set; } = "Teamverse";
        
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
