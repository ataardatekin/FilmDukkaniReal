using FilmDukkani.BLL.AbstractService;
using FilmDukkani.DAL.Context;
using FilmDukkani.DAL.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using FilmDukkani.Common;

namespace FilmDukkani.MVC.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Employee")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMovieService _movieService;
        private readonly FilmDukkaniContext _context;

        public OrderController(IOrderService orderService, IMovieService movieService, FilmDukkaniContext context)
        {
            _orderService = orderService;
            _movieService = movieService;
            _context = context;
        }



        public IActionResult Index()
        {
            var orders = _context.Orders
        .Include(o => o.User)
        .Include(o => o.OrderMovies)
        .ToList();




            return View(orders);
        }



        public IActionResult ConfirmOrder(int id)
        {
            

            var order = _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderMovies)
                    .ThenInclude(om => om.Movie)
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }


            order.IsActive = false;
            order.IsShipped = true;


            var random = new Random();
            var selectedMovies = order.OrderMovies
                .Select(om => om.Movie)
                .OrderBy(m => random.Next())
                .Take(order.User.MovieChange)
                .ToList();

            var selectedMovieNames = selectedMovies.Select(movie => movie.MovieName).ToList();


            // Seçilen filmlerin kullanıcıya mail olarak gönderilmesi işlemi

            //MailSender.SendEmail(order.User.Email,"Kargolarınız",$"Sayın {order.User.Firstname} {order.User.Lastname} size gönderilecek olan filmler şunlardır: {selectedMovieNames}");


            try
            {
                var asdas = order.User.MovieNumber -= order.User.MovieChange;
                if (asdas < 0)
                {
                    ViewBag.ChangeError = "Kullanıcının değişim hakkı kalmadı!";
                    return View("Index");
                }
                foreach (var movie in selectedMovies)
                {
                    movie.UnitsInStock--;
                }

            }
            catch 
            {
                ViewBag.ChangeError = "Kullanıcının değişim hakkı kalmadı!";
                return View("Index");
            }


            // Değişiklikleri veritabanına kaydet
            _context.SaveChanges();

            return RedirectToAction("Index");
        }





    }
}
