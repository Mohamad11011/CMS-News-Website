using System.ComponentModel.DataAnnotations;

namespace DgPadCMS.Models
{
    public class UserRole
    {
        [Required]
        public string RoleName { get; set; }

        public string RoleId { get; set; }

        public string[] ?AddIds { get; set; }

        public string[] ?DeleteIds { get; set; }
    }
}
