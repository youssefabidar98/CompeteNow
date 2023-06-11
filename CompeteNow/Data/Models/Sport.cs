namespace CompeteNow.Data.Models
{
    public class Sport : Entity
    {
        public Sport()
        {

        }
        public Sport(string sportName)
        {
            SportName = sportName;
        }

        public string SportName { get; set; }
    }
}
