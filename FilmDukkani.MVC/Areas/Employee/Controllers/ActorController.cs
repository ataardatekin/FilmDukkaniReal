using FilmDukkani.BLL.AbstractService;
using FilmDukkani.MVC.DTO;
using FilmDukkani.Entity.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using FilmDukkani.BLL.Service;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using FilmDukkani.Common;
using Microsoft.AspNetCore.Hosting;

namespace FilmDukkani.MVC.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Employee")]
    public class ActorController : Controller
    {
        private readonly IActorService _actorService;

        public ActorController(IActorService actorService)
        {
            _actorService = actorService;
        }

        public IActionResult Index()
        {
            var actors = _actorService.GetAllActors();
            return View(actors);
        }


        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Create(ActorDTO actorDTO)
        {
            Actor actor = new Actor()
            {
                Name = actorDTO.ActorName
            };


            _actorService.CreateActor(actor);

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {

            var deleted = _actorService.FindActor(id);
            _actorService.DeleteActor(deleted);

            return View("Index");
        }

        public IActionResult Update(int id)
        {
            var updated = _actorService.GetAllActors().Where(x => x.Id == id).FirstOrDefault();
            return View(updated);
        }



        [HttpPost]
        public IActionResult Update(Actor actor)
        {

            _actorService.UpdateActor(actor);


            return RedirectToAction("Index");
        }



    }
}
