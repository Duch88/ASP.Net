﻿using System.ComponentModel.DataAnnotations;

namespace CinemaWebApp.Models.ViewModels
{
    public class CinemaCreateViewModel
    { 

        [Required(ErrorMessage = "Cinema name is required.")]
        [StringLength(80, ErrorMessage = "Cinema name is too long")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(50, ErrorMessage = "Location is too long")]
        public string Location { get; set; } = null!;
    }
}
