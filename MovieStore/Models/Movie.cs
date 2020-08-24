using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace MovieStore.Models
{

    public class Movie
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Duration { get; set; }
        virtual public ICollection<Genre> Genres { get; set; }
        public ICollection<Actor> Cast { get; set; }
        public double Rate { get; set; } //Fetch from IMBD
        public string Director { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}
