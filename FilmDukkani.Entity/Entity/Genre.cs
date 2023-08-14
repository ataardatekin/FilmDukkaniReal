using FilmDukkani.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmDukkani.Entity.Entity
{
    public class Genre:BaseEntity
    {
        public string GenreName { get; set; }
        public string? Description { get; set; }


        public List<Movie> Movies { get; set; }



    }
}
