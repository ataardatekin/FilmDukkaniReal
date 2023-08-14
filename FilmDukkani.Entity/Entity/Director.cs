using FilmDukkani.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmDukkani.Entity.Entity
{
    public class Director:BaseEntity
    {
        public string DirectorName { get; set; }



        public List<Movie>? Movies { get; set; }
    }
}
