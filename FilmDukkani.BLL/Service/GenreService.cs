using FilmDukkani.BLL.Abstract;
using FilmDukkani.BLL.AbstractService;
using FilmDukkani.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmDukkani.BLL.Service
{
    public class GenreService : IGenreService
    {
        private readonly IRepository<Genre> _genreRepository;

        public GenreService(IRepository<Genre> repository)
        {
            _genreRepository = repository;
        }



        public string CreateGenre(Genre genre)
        {
            try
            {
                _genreRepository.Create(genre);
                return "veri oluşturuldu!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string DeleteGenre(Genre genre)
        {
            try
            {
                genre.Status = FilmDukkani.Entity.Enum.Status.Deleted;
                return _genreRepository.Update(genre);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public Genre FindGenre(int id)
        {
            return _genreRepository.GetById(id);
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            return _genreRepository.GetAll().ToList();
        }

        public string UpdateGenre(Genre genre)
        {
            try
            {
                genre.Status = FilmDukkani.Entity.Enum.Status.Updated;
                return _genreRepository.Update(genre);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
