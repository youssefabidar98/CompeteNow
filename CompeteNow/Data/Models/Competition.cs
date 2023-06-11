using System.ComponentModel.DataAnnotations;

namespace CompeteNow.Data.Models
{
    public class Competition : Entity
    {
        [StringLength(50)]
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Sport Sport { get; set; }
        public int SportId { get; set; }
        public List<CompetitionEvent> CompetitionEvents { get; set; } = new List<CompetitionEvent>();
    }
}
