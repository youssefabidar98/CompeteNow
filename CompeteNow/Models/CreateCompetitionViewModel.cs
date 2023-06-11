using System.ComponentModel.DataAnnotations;

namespace CompeteNow.Models
{
    public class CreateCompetitionViewModel
    {
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public List<(int Id, string Name)> Sports { get; set; }
    }
}
