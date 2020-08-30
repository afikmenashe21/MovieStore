using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models
    {
    public class MovieGenre
        {
        
        public int MovieId { get; set; }

        public Movie Movie { get; set; }
        
        public int GenreId { get; set; }

        public Genre Genre { get; set; }
        }
    }

//protected override void OnModelCreating ( ModelBuilder modelBuilder )
//    {
//    modelBuilder.Entity<MovieActor>()
//        .HasKey( ma => new { ma.MovieId , ma.ActorId } );

//    modelBuilder.Entity<MovieActor>()
//        .HasOne( mo => mo.Movie )
//        .WithMany( m => m.Cast )
//        .HasForeignKey( ma => ma.MovieId );

//    modelBuilder.Entity<MovieActor>()
//        .HasOne( ac => ac.Actor )
//        .WithMany( a => a.Filmography )
//        .HasForeignKey( ac => ac.ActorId );

//    modelBuilder.Entity<MovieGenre>()
//        .HasKey( mg => new { mg.MovieId , mg.GenreId } );

//    modelBuilder.Entity<MovieGenre>()
//        .HasOne( mo => mo.Movie )
//        .WithMany( mg => mg.MovieGenre )
//        .HasForeignKey( ma => ma.MovieId );

//    modelBuilder.Entity<MovieGenre>()
//        .HasOne( ge => ge.Genre )
//        .WithMany( mg => mg.MovieGenre )
//        .HasForeignKey( ge => ge.GenreId );
//    }