using System.ComponentModel.DataAnnotations;

namespace ChampionsLeagueTeamsApp.Models
{
    public class Coach
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Experience is required.")]
        [Range(0, 50, ErrorMessage = "Experience must be between 0 and 50 years.")]
        public int Experience { get; set; }

        [Required(ErrorMessage = "TeamId is required.")]
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
