using FilmDukkani.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmDukkani.Entity.Entity
{
    public class Membership:BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int MovieChange { get; set; }
        public int MovieNumber { get; set; }




        public List<User> Users { get; set; } = new List<User>();
    }
}
