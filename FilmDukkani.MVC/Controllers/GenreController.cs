using FilmDukkani.BLL.AbstractService;
using FilmDukkani.DAL.Context;
using FilmDukkani.Entity.Entity;
using FilmDukkani.MVC.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmDukkani.MVC.Controllers
{
    [Authorize]
    public class GenreController : Controller
    {
        private readonly FilmDukkaniContext _context;
        private readonly IMovieService _movieService;

        public GenreController(FilmDukkaniContext context, IMovieService movieService)
        {
            _context = context;
            _movieService = movieService;
        }

        public async Task<IActionResult> Index()
        {
            var genres = _context.Genres.ToList();
            var genreDTOs = genres.Select(g => new GenreDTO
            {
                Id = g.Id,
                GenreName = g.GenreName,
                Description = g.Description
                
            }).ToList(); 

            return View(genreDTOs);
        }

        public async Task<IActionResult> List(int? id)
        {
            var genre = _context.Genres.Include(g => g.Movies).FirstOrDefault(g => g.Id == id);

            if (genre == null)
            {
                return NotFound();
            }

            var movieDTOs = genre.Movies.Select(m => new MovieDTO
            {
                Id = m.Id,
                MovieRealName = m.MovieRealName,
                Description = m.Description,
                PicturePath = m.PicturePath,
                IsActive = m.IsActive,
            }).ToList();

            ViewBag.GenreName = genre.GenreName;
            ViewBag.MovieDTOs = movieDTOs;

            return View(movieDTOs);
        }





        public IActionResult RewardedMovies()
        {
            var rewardedMovies = new List<MovieDTO>();
            var movies = _context.Movies.ToList();
            var movieDTOs = movies.Select(m => new MovieDTO
            {
                Id = m.Id,
                //MovieName = m.MovieName,
                MovieRealName = m.MovieRealName,
                Description = m.Description,
                //YearOfMovie = m.YearOfMovie,
                //IsTurkishDubbing = m.IsTurkishDubbing,
                //IsTurkishSubtitles = m.IsTurkishSubtitles,
                //IsSurround = m.IsSurround,
                //MovieTrailer = m.MovieTrailer,
                //MovieTrophies = m.MovieTrophies,
                //BarcodeNumber = m.BarcodeNumber,
                PicturePath = m.PicturePath,
                IsActive = m.IsActive,
                MovieTrophies = m.MovieTrophies,
            }).ToList();

            foreach ( var movie in movieDTOs)
            {
                if(movie.MovieTrophies != null)
                {
                    rewardedMovies.Add(movie);
                }

            }


            return View(rewardedMovies);
        }


        public IActionResult NewlyAddedMovies()
        {
            var newlyAddedMovies = new List<MovieDTO>();
            var movies = _context.Movies.ToList();
            var movieDTOs = movies.Select(m => new MovieDTO
            {
                Id = m.Id,
                MovieRealName = m.MovieRealName,
                Description = m.Description,
                PicturePath = m.PicturePath,
                IsActive = m.IsActive,
                MovieTrophies = m.MovieTrophies,
                CreatedDate = m.CreatedDate 
            }).ToList();

            var lastTwoWeeks = DateTime.Now.AddDays(-14); 

            foreach (var movie in movieDTOs)
            {
                if (movie.CreatedDate >= lastTwoWeeks && movie.IsActive)
                {
                    newlyAddedMovies.Add(movie);
                }
            }

            return View(newlyAddedMovies);
        }




    }
        
    }
