using FilmDukkani.BLL.Abstract;
using FilmDukkani.BLL.AbstractService;
using FilmDukkani.Common;
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
    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _movieRepository;


        public MovieService(IRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
            
        }



        public string CreateMovie(Movie movie)
        {
            try
            {
                _movieRepository.Create(movie);
                movie.CreatedComputerName = System.Environment.MachineName;
                movie.CreatedAdUsername = System.Environment.UserName;
                movie.CreatedIpAddress = IpAddressFinder.GetHostName();
                return "Veri Eklendi!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public int DecreaseStock(int id, int count)
        {
            var movie = FindMovie(id);
            movie.UnitsInStock -= count;
            UpdateMovie(movie);
            return movie.UnitsInStock;
        }

        public string DeleteMovie(Movie movie)
        {
            try
            {
                movie.Status = Entity.Enum.Status.Deleted;
                movie.UpdatedComputerName = System.Environment.MachineName;
                movie.UpdatedAdUsername = System.Environment.UserName;
                movie.UpdatedIpAddress = IpAddressFinder.GetHostName();
                return _movieRepository.Update(movie);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public Movie FindMovie(int id)
        {
            return _movieRepository.GetById(id);
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return _movieRepository.GetAll().ToList();
        }

        public List<Movie> SearchMovies(string searchTerm)
        {
            try
            {
                return _movieRepository.GetAll()
                .Where(movie =>
                    movie.MovieName.Contains(searchTerm) ||
                    movie.MovieRealName.Contains(searchTerm) ||
                    movie.Description.Contains(searchTerm))
                .ToList();
            }
            catch 
            {
                return new List<Movie>();
            }
        }

        public string UpdateMovie(Movie movie)
        {
            try
            {
                movie.Status = Entity.Enum.Status.Updated;
                movie.UpdatedComputerName = System.Environment.MachineName;
                movie.UpdatedAdUsername = System.Environment.UserName;
                movie.UpdatedIpAddress = IpAddressFinder.GetHostName();

                return _movieRepository.Update(movie);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
