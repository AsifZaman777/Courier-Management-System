using System.ComponentModel.DataAnnotations;

namespace Courier_Management_System.Models.Domain
{
    public class Admin
    {
        [Key]
        public string AdminEmail { get; set; }
        public string AdminPassword { get; set; } = string.Empty;
        public string AdminName { get; set; }
    }
}
