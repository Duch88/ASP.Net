namespace ChampionsLeagueTeamsApp.Models
{
    public class Coah
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Experience { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
