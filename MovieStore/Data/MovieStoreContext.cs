using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieStore.Models;

namespace MovieStore.Data
{
    public class MovieStoreContext : DbContext
    {
        public MovieStoreContext (DbContextOptions<MovieStoreContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating ( ModelBuilder modelBuilder )
            {
            modelBuilder.Entity<MovieActor>()
                .HasKey( ma => new { ma.MovieId , ma.ActorId } );

            modelBuilder.Entity<MovieActor>()
                .HasOne( mo => mo.Movie )
                .WithMany( m => m.MovieActor )
                .HasForeignKey( ma => ma.MovieId );

            modelBuilder.Entity<MovieActor>()
                .HasOne( ac => ac.Actor )
                .WithMany( a => a.MovieActor )
                .HasForeignKey( ac => ac.ActorId );

            modelBuilder.Entity<MovieGenre>()
                .HasKey( mg => new { mg.MovieId , mg.GenreId } );

            modelBuilder.Entity<MovieGenre>()
                .HasOne( mo => mo.Movie )
                .WithMany( mg => mg.MovieGenre )
                .HasForeignKey( ma => ma.MovieId );

            modelBuilder.Entity<MovieGenre>()
                .HasOne( ge => ge.Genre )
                .WithMany( mg => mg.MovieGenre )
                .HasForeignKey( ge => ge.GenreId );
            }

        public DbSet<MovieStore.Models.User> User { get; set; }

        public DbSet<MovieStore.Models.Review> Review { get; set; }

        public DbSet<MovieStore.Models.MovieGenre> MovieGenre { get; set; }

        public DbSet<MovieStore.Models.MovieActor> MovieActor { get; set; }

        public DbSet<MovieStore.Models.Genre> Genre { get; set; }

        public DbSet<MovieStore.Models.Actor> Actor { get; set; }

        public DbSet<MovieStore.Models.Movie> Movie { get; set; }
    }
}
