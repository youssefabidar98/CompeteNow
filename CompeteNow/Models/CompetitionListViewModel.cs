using System.ComponentModel.DataAnnotations;

namespace CompeteNow.Models
{
    public class CompetitionListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string SportName { get; set; }
    }
}
