using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmDukkani.Entity.Entity
{
    public class OrderMovie
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
