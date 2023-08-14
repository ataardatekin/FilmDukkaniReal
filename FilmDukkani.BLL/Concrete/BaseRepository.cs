using FilmDukkani.BLL.Abstract;
using FilmDukkani.DAL.Context;
using FilmDukkani.Entity.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmDukkani.BLL.Concrete
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly FilmDukkaniContext _context;
        private readonly DbSet<T> _entities;

        public BaseRepository(FilmDukkaniContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public string Create(T entity)
        {
            try
            {
                _entities.Add(entity);
                _context.SaveChanges();

                return "Veri kaydedildi";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public string Delete(T entity)
        {
            try
            {
                var deleted = GetById(entity.Id);
                deleted.Status = FilmDukkani.Entity.Enum.Status.Deleted;

                Update(deleted);
                return "Veri silindi!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.Where(x => x.Status == FilmDukkani.Entity.Enum.Status.Created || x.Status == FilmDukkani.Entity.Enum.Status.Updated);
        }

        public T GetById(int id)
        {
            var entity = _entities.Find(id);
            return entity;
        }

        public string Update(T entity)
        {
            string result = "";
            try
            {
                switch (entity.Status)
                {
                    case FilmDukkani.Entity.Enum.Status.Updated:
                        entity.Status = FilmDukkani.Entity.Enum.Status.Updated;
                        _context.Entry(entity).State = EntityState.Modified;
                        _context.SaveChanges();
                        result = "veri güncellendi!";
                        break;

                        case FilmDukkani.Entity.Enum.Status.Deleted:
                        entity.Status = FilmDukkani.Entity.Enum.Status.Deleted;
                        _context.SaveChanges();
                        result = "veri güncellendi!";
                        break;
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

    }
}
