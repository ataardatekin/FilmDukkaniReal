using FilmDukkani.Entity;
using FilmDukkani.Entity.Base;
using FilmDukkani.Entity.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmDukkani.DAL.Context
{
    public class FilmDukkaniContext : IdentityDbContext<User, IdentityRole, string>

    {
        public FilmDukkaniContext(DbContextOptions<FilmDukkaniContext> options) : base(options)
        {

        }

        //Tables
        public DbSet<Movie> Movies { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Actor> Actors { get; set; }

        public DbSet<Director> Directors { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Membership> Memberships { get; set; }

        public DbSet<CreditCard> CreditCards { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderMovie> OrderMovies { get; set; }











        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<Membership>()
                .Property(m => m.Price)
                .HasColumnType("decimal(5,2)");

            
            modelBuilder.Entity<CreditCard>()
                .HasOne(c => c.User)
                .WithMany(u => u.CreditCards)
                .HasForeignKey(c => c.UserId);



            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders) 
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);



            modelBuilder.Entity<OrderMovie>()
    .HasKey(om => new { om.OrderId, om.MovieId });

            modelBuilder.Entity<OrderMovie>()
                .HasOne(om => om.Order)
                .WithMany(o => o.OrderMovies)
                .HasForeignKey(om => om.OrderId);

            modelBuilder.Entity<OrderMovie>()
                .HasOne(om => om.Movie)
                .WithMany(m => m.OrderMovies)
                .HasForeignKey(om => om.MovieId);











        }

    }
}
