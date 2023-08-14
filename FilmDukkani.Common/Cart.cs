using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmDukkani.Common
{
    public class Cart
    {
        public Dictionary<int, CartItem> _myCart = new Dictionary<int, CartItem>();


        public void AddItem(CartItem cartItem)
        {
            if (_myCart.ContainsKey(cartItem.Id))
            {
                return;
            }
            _myCart.Add(cartItem.Id, cartItem);
        }

    }
}
