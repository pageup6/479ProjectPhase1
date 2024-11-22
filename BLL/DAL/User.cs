using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string UserName { get; set; }

        [Required, MaxLength(100)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }

        // Navigation property
        public Role Role { get; set; }
    }
}