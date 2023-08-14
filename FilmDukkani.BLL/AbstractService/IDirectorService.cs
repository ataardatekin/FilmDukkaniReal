using FilmDukkani.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmDukkani.BLL.AbstractService
{
    public interface IDirectorService
    {
        IEnumerable<Director> GetAllDirectors();

        string CreateDirector(Director director);

        string DeleteDirector(Director director);

        Director FindDirector(int id);

        string UpdateDirector(Director director);
    }
}
