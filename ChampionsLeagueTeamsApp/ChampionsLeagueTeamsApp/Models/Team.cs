namespace ChampionsLeagueTeamsApp.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int ChampionsLeagueWins { get; set; }
        public List<Title> Titles { get; set; }
    }
}
