using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Models
{
    public enum UserRoles
    {
        Admin = 1,
        SuperAdmin = 2

    }

    public class Users
    {
        [Key]
        [DisplayFormat(NullDisplayText = "", ApplyFormatInEditMode = true)]

        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [NotMapped]
        [Required]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Role is required.")]
        public UserRoles Role { get; set; }

        [Required]
        public string FullName { get; set; }
        public bool IsActive { get; set; } = true;
        [DataType(DataType.Date)]
        public DateTime LastLogin { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        
    }
}
