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
        [Required]
        public string Content { get; set; }
        public double Rating { get; set; }
        public DateTime Published { get; set; }
        public User Author { get; set; }
        public Movie Movie { get; set; }

    }
}
