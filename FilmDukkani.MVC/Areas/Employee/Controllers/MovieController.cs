using FilmDukkani.BLL.AbstractService;
using FilmDukkani.Common;
using FilmDukkani.DAL.Context;
using FilmDukkani.Entity.Entity;
using FilmDukkani.MVC.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Linq;

namespace FilmDukkani.MVC.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Employee")]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly FilmDukkaniContext _context;

        public MovieController(IMovieService movieService, IGenreService genreService, IWebHostEnvironment webHostEnvironment, FilmDukkaniContext context)
        {
            _movieService = movieService;
            _genreService = genreService;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }


        public IActionResult Index()
        {
            var movies = _movieService.GetAllMovies();

            return View(movies);
        }



        public IActionResult Create()
        {
            ViewBag.SelectActors = _context.Actors.ToList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            ViewBag.SelectSupplier = _context.Suppliers.ToList().Select(x => new SelectListItem
            {
                Text = x.CompanyName,
                Value = x.Id.ToString()
            });



            ViewBag.SelectDirectors = _context.Directors.ToList().Select(x => new SelectListItem
            {
                Text = x.DirectorName,
                Value = x.Id.ToString()
            });


            ViewBag.SelectGenres = _genreService.GetAllGenres().Select(x => new SelectListItem
            {
                Text = x.GenreName,
                Value = x.Id.ToString()
            });
            return View();
        }

        [HttpPost]
        public IActionResult Create(MovieDTO movieDTO, IFormFile movieImage)
        {

            if (ModelState.IsValid)
            {
                string webRoothPath = _webHostEnvironment.WebRootPath;
                string path = "";
                path = Path.Combine(webRoothPath, "images\\Movie");
                string result = ImageUploader.UploadImage(path, movieImage);

                movieDTO.PicturePath = result;



                Movie movie = new Movie()
                {
                    MovieName = movieDTO.MovieName,
                    MovieRealName = movieDTO.MovieRealName,
                    Description = movieDTO.Description,
                    YearOfMovie = movieDTO.YearOfMovie,
                    IsTurkishSubtitles = movieDTO.IsTurkishSubtitles,
                    IsTurkishDubbing = movieDTO.IsTurkishDubbing,
                    IsSurround = movieDTO.IsSurround,
                    MovieTrophies = movieDTO.MovieTrophies,
                    BarcodeNumber = movieDTO.BarcodeNumber,
                    PicturePath = movieDTO.PicturePath,
                    UnitsInStock = movieDTO.UnitsInStock,
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    //SupplierId = movieDTO.SupplierId,
                    //Genres = movieDTO.Genres.Select(id => new Genre { Id = id }).ToList(),
                    //Actors = movieDTO.Actors.Select(id => new Actor { Id = id }).ToList(),
                    //Directors = movieDTO.Directors.Select(id => new Director { Id = id }).ToList(),
                    Status = Entity.Enum.Status.Created
                };

                var supplier = _context.Suppliers.FirstOrDefault(s => s.Id == movieDTO.SupplierId);
                if (supplier != null)
                {
                    movie.Supplier = supplier;
                }

                var selectedGenreIds = movieDTO.Genres; 
                movie.Genres = _context.Genres.Where(g => selectedGenreIds.Contains(g.Id)).ToList();

                var selectedActorIds = movieDTO.Actors;
                movie.Actors = _context.Actors.Where(g => selectedActorIds.Contains(g.Id)).ToList();

                var selectedDirectorIds = movieDTO.Directors;
                movie.Directors = _context.Directors.Where(g => selectedDirectorIds.Contains(g.Id)).ToList();





                _movieService.CreateMovie(movie);

            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id) 
        {

            var deleted = _movieService.FindMovie(id);
            _movieService.DeleteMovie(deleted);

            return View("Index");
        
        }





        
        public IActionResult Update(int id)
        {
            ViewBag.SelectActors = _context.Actors.ToList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            ViewBag.SelectSupplier = _context.Suppliers.ToList().Select(x => new SelectListItem
            {
                Text = x.CompanyName,
                Value = x.Id.ToString()
            });



            ViewBag.SelectDirectors = _context.Directors.ToList().Select(x => new SelectListItem
            {
                Text = x.DirectorName,
                Value = x.Id.ToString()
            });


            ViewBag.SelectGenres = _genreService.GetAllGenres().Select(x => new SelectListItem
            {
                Text = x.GenreName,
                Value = x.Id.ToString()
            });



            var updated = _movieService.GetAllMovies().Where(x => x.Id == id).FirstOrDefault();
            return View(updated);
        }




        [HttpPost]
        public IActionResult Update(Movie movie,IFormFile movieImage)
        {

            string webRoothPath = _webHostEnvironment.WebRootPath;
            string path = "";
            path = Path.Combine(webRoothPath, "images\\Movie");
            string result = ImageUploader.UploadImage(path, movieImage);

            movie.PicturePath = result;





            _movieService.UpdateMovie(movie);


            return RedirectToAction("Index");
        }









        private void PopulateDropdownLists()
        {
            ViewBag.SelectActors = _context.Actors.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            ViewBag.SelectSupplier = _context.Suppliers.Select(x => new SelectListItem
            {
                Text = x.CompanyName,
                Value = x.Id.ToString()
            });

            ViewBag.SelectDirectors = _context.Directors.Select(x => new SelectListItem
            {
                Text = x.DirectorName,
                Value = x.Id.ToString()
            });

            ViewBag.SelectGenres = _genreService.GetAllGenres().Select(x => new SelectListItem
            {
                Text = x.GenreName,
                Value = x.Id.ToString()
            });
        }
    }
}

