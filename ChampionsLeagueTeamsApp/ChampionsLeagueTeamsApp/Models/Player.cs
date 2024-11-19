using System.ComponentModel.DataAnnotations;

namespace ChampionsLeagueTeamsApp.Models
{
    public class Player
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Position is required.")]
        [StringLength(50, ErrorMessage = "Position cannot exceed 50 characters.")]
        public string Position { get; set; } = null!;
        public int Goals { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
