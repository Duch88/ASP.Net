using System.ComponentModel.DataAnnotations;

namespace ChampionsLeagueTeamsApp.Models
{
    public class Stadium
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = null!;

        [Range(1000, 100000, ErrorMessage = "Capacity must be between 1,000 and 100,000.")]
        public int Capacity { get; set; }
        public string Location { get; set; } = null!;

        public string Details { get; set; } = null!;
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
