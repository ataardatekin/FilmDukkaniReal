using FilmDukkani.Entity.Entity;
using FilmDukkani.Entity.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FilmDukkani.Entity.Base;

namespace FilmDukkani.MVC.DTO
{
    public class MovieDTO
    {
        [Required]
        public int Id { get; set; }
        public string MovieName { get; set; }
        public string? MovieRealName { get; set; }
        public string? Description { get; set; }
        public string? YearOfMovie { get; set; }
        public bool? IsTurkishSubtitles { get; set; }
        public bool? IsTurkishDubbing { get; set; }
        public bool? IsSurround { get; set; }
        public string? MovieTrailer { get; set; }
        public string? MovieTrophies { get; set; }
        public string? BarcodeNumber { get; set; }
        public string? PicturePath { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int UnitsInStock { get; set; }

        public Status Status { get; set; }

        public List<GenreDTO>? GenreList { get; set; }
        public List<ActorDTO>? ActorList { get; set; }
        public List<DirectorDTO>? DirectorList { get; set; }

        public int? SupplierId { get; set; }

        public List<int> Genres { get; set; }
        public List<int> Actors { get; set; }
        public List<int> Directors { get; set; }




    }
}
