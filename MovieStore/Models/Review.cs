using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models
    {
    public class Review
        {
        public int Id { get; set; }
        [Required]
        public string Headline { get; set; }
        [StringLength( 250 , MinimumLength = 2 )]
        [RegularExpression( @"^[\w\s]+$" )] // Demand the name wiil be with a-z or A-Z or digits characters
        [Required]
        public string Content { get; set; }
        public int Rating { get; set; }
        public DateTime Published { get; set; }
        public User Author { get; set; }
        public Movie Movie { get; set; }

        }
    }
