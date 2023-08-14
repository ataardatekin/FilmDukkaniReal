using FilmDukkani.BLL.AbstractService;
using FilmDukkani.Common;
using FilmDukkani.DAL.Context;
using FilmDukkani.Entity.Base;
using FilmDukkani.Entity.Entity;
using FilmDukkani.MVC.DTO;
using FilmDukkani.MVC.Models;
using FilmDukkani.MVC.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace FilmDukkani.MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly FilmDukkaniContext _context;
        private readonly IMovieService _movieService;
        private readonly UserManager<User> _userManager;
        private readonly IOrderService _orderService;
        

        public HomeController(FilmDukkaniContext context, IMovieService movieService, UserManager<User> userManager, IOrderService orderService)
        {
            _context = context;
            _movieService = movieService;
            _userManager = userManager;
            _orderService = orderService;
        }


        public IActionResult Index()
        {

            var movies = _context.Movies.ToList();
            var movieDTOs = movies.Select(m => new MovieDTO
            {
                Id = m.Id,
                //MovieName = m.MovieName,
                MovieRealName = m.MovieRealName,
                Description = m.Description,
                Status = m.Status,
                //YearOfMovie = m.YearOfMovie,
                //IsTurkishDubbing = m.IsTurkishDubbing,
                //IsTurkishSubtitles = m.IsTurkishSubtitles,
                //IsSurround = m.IsSurround,
                //MovieTrailer = m.MovieTrailer,
                //MovieTrophies = m.MovieTrophies,
                //BarcodeNumber = m.BarcodeNumber,
                PicturePath = m.PicturePath,
                IsActive = m.IsActive,
            }).ToList();




            

            return View(movieDTOs);
        }




        public IActionResult Details(int id)
        {
            var movie = _context.Movies
                .Include(m => m.Genres)
                .Include(m => m.Actors)
                .FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            var movieDTO = new MovieDTO
            {
                Id = movie.Id,
                MovieName = movie.MovieName,
                MovieRealName = movie.MovieRealName,
                Description = movie.Description,
                YearOfMovie = movie.YearOfMovie,
                IsTurkishDubbing = movie.IsTurkishDubbing,
                IsTurkishSubtitles = movie.IsTurkishSubtitles,
                IsSurround = movie.IsSurround,
                MovieTrailer = movie.MovieTrailer,
                MovieTrophies = movie.MovieTrophies,
                BarcodeNumber = movie.BarcodeNumber,
                PicturePath= movie.PicturePath,


                GenreList = movie.Genres.Select(g => new GenreDTO { Id = g.Id, GenreName = g.GenreName }).ToList(),
                ActorList = movie.Actors.Select(a => new ActorDTO { Id = a.Id, ActorName = a.Name }).ToList()
            };



            return View(movieDTO);
        }




        public IActionResult AddToCart(int id)
        {
            Cart cartSession;

            Movie movie = _movieService.FindMovie(id);

            //Serialize => Json
            if (SessionHelper.GetMovieFromJson<Cart>(HttpContext.Session, "sepet") == null)
            {
                cartSession = new Cart();
            }
            else
            {
                cartSession = SessionHelper.GetMovieFromJson<Cart>(HttpContext.Session, "sepet");
            }


            CartItem cartItem = new CartItem();
            cartItem.Id = movie.Id;
            cartItem.MovieName = movie.MovieRealName;
            
            cartSession.AddItem(cartItem);
            SessionHelper.SetJsonMovie(HttpContext.Session, "sepet", cartSession);

            return RedirectToAction("Index");

        }


        public IActionResult MyCart()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login","Account");
            }
            else
            {
                if(SessionHelper.GetMovieFromJson<Cart>(HttpContext.Session, "sepet") != null)
                {
                    var sepet = SessionHelper.GetMovieFromJson<Cart>(HttpContext.Session, "sepet");
                    return View(sepet);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }


        public async Task<IActionResult> CompleteCart()
        {
            Cart cart = SessionHelper.GetMovieFromJson<Cart>(HttpContext.Session, "sepet");
            Random rnd = new Random();
            if (User.Identity.IsAuthenticated)
            {
                if (cart != null && cart._myCart.Count >= 10)
                {
                    if (DateTime.Now.DayOfWeek != DayOfWeek.Sunday)
                    {
                        Order order = new Order();
                        var user = await _userManager.GetUserAsync(User);
                        order.User = user;
                        order.User.UserName = user.UserName;
                        order.UserId = user.Id.ToString();
                        order.OrderNumber = rnd.Next(0, 99999999).ToString();


                        foreach (var cartItemPair in cart._myCart)
                        {
                            int movieId = cartItemPair.Key; 
                            var cartItem = cartItemPair.Value; 

                            var movie = _movieService.FindMovie(movieId);

                            if (movie != null)
                            {
                                var orderMovie = new OrderMovie
                                {
                                    MovieId = movie.Id,
                                    
                                    OrderId = order.Id
                                };
                                order.OrderMovies.Add(orderMovie);
                            }
                        }



                        _orderService.CreateOrder(order);

                        SessionHelper.RemoveSession(HttpContext.Session, "sepet");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Pazar günleri sipariş verilemez.");
                        return View("MyCart", cart);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Sepetinizde en az 10 film bulunması gerekiyor.");
                    return View("MyCart", cart);
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        public IActionResult Search(string q)
        {
            var movies = _movieService.SearchMovies(q); 

            return View("SearchResults", movies);
        }








    }
    }