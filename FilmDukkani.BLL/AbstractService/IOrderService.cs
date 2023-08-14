using FilmDukkani.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmDukkani.BLL.AbstractService
{
    public interface IOrderService
    {
        string CreateOrder(Order order);


        IEnumerable<Order> GetAllOrders();


        string UpdateOrder(Order order);
        Order GetOrderByOrderNumber(string orderNumber);


    }
}
