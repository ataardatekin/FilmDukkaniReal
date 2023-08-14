using FilmDukkani.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmDukkani.BLL.AbstractService
{
    public interface IMovieService
    {
        IEnumerable<Movie> GetAllMovies();

        string CreateMovie(Movie movie);

        string DeleteMovie(Movie movie);

        Movie FindMovie(int id);

        string UpdateMovie(Movie movie);

        int DecreaseStock(int id, int count);

        List<Movie> SearchMovies(string searchTerm);
    }
}
