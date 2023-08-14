using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmDukkani.Common
{
    public class CartItem
    {
        public CartItem()
        {
            Quantity = 1;
        }


        public int Id { get; set; }
        public string MovieName { get; set; }
        public short Quantity { get; set; }
    }
}
