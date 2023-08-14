using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using FilmDukkani.BLL.Abstract;
using FilmDukkani.BLL.AbstractService;
using FilmDukkani.BLL.Service;
using FilmDukkani.BLL.Concrete;
using FilmDukkani.Entity.Entity;
using Microsoft.AspNetCore.Identity;
using FilmDukkani.Entity.Base;
using FilmDukkani.DAL.Context;

namespace FilmDukkani.IOC.Container
{
    public class ServiceContainer
    {

        public static void ServiceConfigure(IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IMembershipService, MembershipService>();
            services.AddScoped<IActorService, ActorService>();
            services.AddScoped<IDirectorService, DirectorService>();
        }
    }
}
