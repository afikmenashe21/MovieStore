using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace MovieStore.Models
{
    public enum Genres
    {
        Action, Adventure, Animation, Biography,
        Comedy, Crime, Documentary, Drama,
        Family, Fantasy, History, Horror, Music,
        Mystery, Romance, Sport, War
    }

    public class Movie
    {


        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ICollection<string> Genre { get; set; }
        public int Duration { get; set; }
        // public Supplier supplier { get; set; }
        public ICollection<string> Cast { get; set; }
        public double Rate { get; set; }//Fetch from IMBD
        public string Director { get; set; }

    }
}
