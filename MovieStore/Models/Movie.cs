using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace MovieStore.Models
    {

    public class Movie
        {

        public int Id { get; set; }
        [RegularExpression( @"^[\w\s]+$" )] // Demand the name wiil be with a-z or A-Z or digits characters
        [Required]
        public string Name { get; set; }
        [DataType( DataType.Date )]
        public DateTime ReleaseDate { get; set; }
        public int Duration { get; set; } // time in minutes.
        [RegularExpression( @"^[\w\s]+$" )] // Demand the name wiil be with a-z or A-Z or digits characters
        public string Director { get; set; }
        public string Poster { get; set; }
        public string Trailer { get; set; }
        [RegularExpression( @"^[\w\s]+$" )] // Demand the name wiil be with a-z or A-Z or digits characters
        public string Storyline { get; set; }
        [Range(0,5)]
        public double AverageRating { get; set; }
        public ICollection<MovieGenre> MovieGenre { get; set; }
        [DisplayName( "Cast" )]
        public ICollection<MovieActor> MovieActor { get; set; }
        public ICollection<Review> Comments { get; set; }
        public string imdbID { get; set; }
        }
    }
