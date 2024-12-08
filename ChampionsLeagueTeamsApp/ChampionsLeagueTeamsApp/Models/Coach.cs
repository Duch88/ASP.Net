﻿using System.ComponentModel.DataAnnotations;

namespace ChampionsLeagueTeamsApp.Models
{
    public class Coach
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = null!;

        [Range(0, 50, ErrorMessage = "Experience must be between 0 and 50 years.")]
        public int Experience { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
