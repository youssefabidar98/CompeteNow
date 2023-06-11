namespace CompeteNow.Models
{
    public class AddCompetitionEventViewModel
    {
        public string CompetitionName { get; set; }
        public string Name { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public int MaxParticipants { get; set; }
        public List<(int Id, string Genre)> Genres { get; set; } 
            = new List<(int Id, string Genre)>();
        public int CompetitionId { get; set; }
        public List<string> CompetitionEvents { get; set; } = new List<string>();
    }
}
