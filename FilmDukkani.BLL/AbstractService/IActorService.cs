using FilmDukkani.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmDukkani.BLL.AbstractService
{
    public interface IActorService
    {
        IEnumerable<Actor> GetAllActors();

        string CreateActor(Actor actor);

        string DeleteActor(Actor actor);

        Actor FindActor(int id);

        string UpdateActor(Actor actor);
    }
}
