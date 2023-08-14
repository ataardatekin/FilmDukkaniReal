using FilmDukkani.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmDukkani.Entity.Entity
{
    public class Order:BaseEntity
    {
        public Order()
        {
            IsShipped = false;
        }

        public string OrderNumber { get; set; }
        public bool IsShipped { get; set; }


        public string UserId { get; set; }
        public User User { get; set; }


        public List<OrderMovie> OrderMovies { get; set; } = new List<OrderMovie>();

        


    }
}
