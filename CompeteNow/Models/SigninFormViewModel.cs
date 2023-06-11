using System.ComponentModel.DataAnnotations;
using static CompeteNow.Infrastructure.Enumerations;

namespace CompeteNow.Models
{
    public class SigninFormViewModel
    {
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required, DataType(DataType.Text)]
        public string Genre { get; set; }
    }
}