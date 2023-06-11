namespace CompeteNow.Data.Models
{
    public class Participation : Entity
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public CompetitionEvent CompetitionEvent { get; set; }
        public int CompetitionEventId { get; set; }
    }
}
