using FilmDukkani.BLL.Abstract;
using FilmDukkani.BLL.AbstractService;
using FilmDukkani.Common;
using FilmDukkani.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FilmDukkani.BLL.Service
{
    public class DirectorService : IDirectorService
    {
        private readonly IRepository<Director> _directorRepository;

        public DirectorService(IRepository<Director> directorRepository)
        {
            _directorRepository = directorRepository;
        }


        public string CreateDirector(Director director)
        {
            try
            {
                _directorRepository.Create(director);
                director.CreatedComputerName = System.Environment.MachineName;
                director.CreatedAdUsername = System.Environment.UserName;
                director.CreatedIpAddress = IpAddressFinder.GetHostName();
                return "Veri Eklendi!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string DeleteDirector(Director director)
        {
            try
            {
                director.Status = Entity.Enum.Status.Deleted;
                director.UpdatedComputerName = System.Environment.MachineName;
                director.UpdatedAdUsername = System.Environment.UserName;
                director.UpdatedIpAddress = IpAddressFinder.GetHostName();
                return _directorRepository.Update(director);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public Director FindDirector(int id)
        {
            return _directorRepository.GetById(id);
        }

        public IEnumerable<Director> GetAllDirectors()
        {
            return _directorRepository.GetAll().ToList();
        }

        public string UpdateDirector(Director director)
        {
            try
            {
                director.Status = Entity.Enum.Status.Updated;
                director.UpdatedComputerName = System.Environment.MachineName;
                director.UpdatedAdUsername = System.Environment.UserName;
                director.UpdatedIpAddress = IpAddressFinder.GetHostName();

                return _directorRepository.Update(director);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
