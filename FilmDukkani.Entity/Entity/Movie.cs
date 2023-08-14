using FilmDukkani.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmDukkani.Entity.Entity
{
    public class Movie:BaseEntity
    {
        [Required]
        [MaxLength(255)]
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
        public int UnitsInStock { get; set; }



        public List<Genre> Genres { get; set; } = new List<Genre>();
        public List<Actor> Actors { get; set; } = new List<Actor>();
        public List<Director> Directors { get; set; } = new List<Director>();

        [ForeignKey("Supplier")]
        public int? SupplierId { get; set; }
        public Supplier? Supplier { get; set; }

        public List<OrderMovie>? OrderMovies { get; set; }

    }

}

