using System.ComponentModel.DataAnnotations;

namespace ChampionsLeagueTeamsApp.Models
{
    public class Title
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        [Range(1900, 2100, ErrorMessage = "Year must be between 1900 and 2100.")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Team is required.")]
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
