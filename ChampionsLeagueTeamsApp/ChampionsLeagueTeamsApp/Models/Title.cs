namespace ChampionsLeagueTeamsApp.Models
{
    public class Title
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
