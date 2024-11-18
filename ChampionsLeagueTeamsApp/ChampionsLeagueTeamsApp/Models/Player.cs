namespace ChampionsLeagueTeamsApp.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Position { get; set; } = null!;
        public int Goals { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
