using static CompeteNow.Infrastructure.Enumerations;

namespace CompeteNow.Data.Models
{
    public class CompetitionEvent : Entity
    {
        public string Name { get; set; }
        public int MinimumAge { get; set; }
        public int MaximumAge { get; set; }
        public CompetitionGenre Genre { get; set; }
        public int MaxParticipants { get; set; }
        public Competition Competition { get; set; }
        public int CompetitionId { get; set; }
        public List<Participation> Participations = new List<Participation>();
    }
}