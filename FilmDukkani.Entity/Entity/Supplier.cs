using FilmDukkani.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmDukkani.Entity.Entity
{
    public class Supplier:BaseEntity
    {
        public string CompanyName { get; set; }
        public string? ContactName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }



        public List<Movie> Movies { get; set; } = new List<Movie>();
    }
}
