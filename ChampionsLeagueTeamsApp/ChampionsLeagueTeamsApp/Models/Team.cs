using System.ComponentModel.DataAnnotations;

namespace ChampionsLeagueTeamsApp.Models
{
    public class Team
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Country is required.")]
        [StringLength(100, ErrorMessage = "Country cannot exceed 100 characters.")]
        public string Country { get; set; } = null!;

        [Range(0, int.MaxValue, ErrorMessage = "Champions League Wins must be a non-negative number.")]
        public int ChampionsLeagueWins { get; set; }

        public ICollection<Coach> Coaches { get; set; } = new List<Coach>();

        public List<Title> Titles { get; set; } = new List<Title>();
    }
}