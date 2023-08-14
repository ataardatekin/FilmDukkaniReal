using FilmDukkani.BLL.Abstract;
using FilmDukkani.BLL.AbstractService;
using FilmDukkani.DAL.Context;
using FilmDukkani.Entity.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmDukkani.BLL.Service
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly FilmDukkaniContext _context;

        public OrderService(IRepository<Order> repository, FilmDukkaniContext context)
        {
            _orderRepository = repository;
            _context = context;
        }

        public string CreateOrder(Order order)
        {
            try
            {
                return _orderRepository.Create(order);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _orderRepository.GetAll().ToList();
        }



        public Order GetOrderByOrderNumber(string orderNumber)
        {
            return _context.Orders
                .Include(o => o.OrderMovies)
                .ThenInclude(om => om.Movie)
                .FirstOrDefault(o => o.OrderNumber == orderNumber);
        }


        public string UpdateOrder(Order order)
        {
            try
            {
                order.Status = Entity.Enum.Status.Updated;
                return _orderRepository.Update(order);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


       



    }
}
