using System.ComponentModel.DataAnnotations;

namespace CinemaWebApp.Models
{
    //comment
    public class Cinema
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public ICollection<CinemaMovie> CinemaMovies { get; set; } = new List<CinemaMovie>();
    }
}
