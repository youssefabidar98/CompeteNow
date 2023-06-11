using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static CompeteNow.Infrastructure.Enumerations;

namespace CompeteNow.Data.Models
{
    public class User : Entity
    {
        [Required, StringLength(50)]
        public string Email { get; set; }
        [Required]
        public string HashedPassword { get; set; }
        public DateTime BirthDate { get; set; }
        public UserGenre Genre { get; set; }
        public List<Role> Roles { get; set; } = new List<Role>();
        public List<Participation> Participations { get; set; } = new List<Participation>();
    }
}
