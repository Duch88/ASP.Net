namespace ChampionsLeagueTeamsApp.Models
{
    public class Stadium
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Capacity { get; set; }
        public string Location { get; set; } = null!;
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
