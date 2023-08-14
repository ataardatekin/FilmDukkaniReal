using FilmDukkani.BLL.AbstractService;
using FilmDukkani.BLL.Service;
using FilmDukkani.Entity.Entity;
using FilmDukkani.MVC.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FilmDukkani.MVC.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Employee")]
    public class DirectorController : Controller
    {
        private readonly IDirectorService _directorService;

        public DirectorController(IDirectorService directorService)
        {
            _directorService = directorService;
        }
        public IActionResult Index()
        {
            var directors = _directorService.GetAllDirectors();
            return View(directors);
        }


        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Create(DirectorDTO directorDTO)
        {
            Director director = new Director()
            {
                DirectorName = directorDTO.DirectorName
            };


            _directorService.CreateDirector(director);

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {

            var deleted = _directorService.FindDirector(id);
            _directorService.DeleteDirector(deleted);
            return View("Index");
        }

        public IActionResult Update(int id)
        {
            var updated = _directorService.GetAllDirectors().Where(x => x.Id == id).FirstOrDefault();
            return View(updated);
        }



        [HttpPost]
        public IActionResult Update(Director director)
        {

            _directorService.UpdateDirector(director);


            return RedirectToAction("Index");
        }
    }
}
