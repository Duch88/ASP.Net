namespace ChampionsLeagueTeamsApp.Models
{
    public class Coach
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Experience { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
