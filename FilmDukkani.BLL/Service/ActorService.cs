using FilmDukkani.BLL.Abstract;
using FilmDukkani.BLL.AbstractService;
using FilmDukkani.Common;
using FilmDukkani.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmDukkani.BLL.Service
{
    public class ActorService : IActorService
    {
        private readonly IRepository<Actor> _actorRepository;

        public ActorService(IRepository<Actor> actorRepository)
        {
            _actorRepository = actorRepository;
        }

        public string CreateActor(Actor actor)
        {
            try
            {
                _actorRepository.Create(actor);
                actor.CreatedComputerName = System.Environment.MachineName;
                actor.CreatedAdUsername = System.Environment.UserName;
                actor.CreatedIpAddress = IpAddressFinder.GetHostName();
                return "Veri Eklendi!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string DeleteActor(Actor actor)
        {
            try
            {
                actor.Status = Entity.Enum.Status.Deleted;
                actor.UpdatedComputerName = System.Environment.MachineName;
                actor.UpdatedAdUsername = System.Environment.UserName;
                actor.UpdatedIpAddress = IpAddressFinder.GetHostName();
                return _actorRepository.Update(actor);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public Actor FindActor(int id)
        {
            return _actorRepository.GetById(id);
        }

        public IEnumerable<Actor> GetAllActors()
        {
            return _actorRepository.GetAll().ToList();
        }

        public string UpdateActor(Actor actor)
        {
            try
            {
                actor.Status = Entity.Enum.Status.Updated;
                actor.UpdatedComputerName = System.Environment.MachineName;
                actor.UpdatedAdUsername = System.Environment.UserName;
                actor.UpdatedIpAddress = IpAddressFinder.GetHostName();

                return _actorRepository.Update(actor);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
