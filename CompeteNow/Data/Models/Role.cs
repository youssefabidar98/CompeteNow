using System.ComponentModel.DataAnnotations;

namespace CompeteNow.Data.Models
{
    public class Role : Entity
    {
        [Required, StringLength(20)]
        public string RoleName { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}