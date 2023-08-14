using FilmDukkani.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmDukkani.BLL.AbstractService
{
    public interface IGenreService
    {
        IEnumerable<Genre> GetAllGenres();

        string CreateGenre(Genre genre);

        string DeleteGenre(Genre genre);

        string UpdateGenre(Genre genre);

        Genre FindGenre(int id);

    }
}
