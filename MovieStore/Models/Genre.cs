using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public enum GenreTypes
    {
    Action, Adventure, Animation, Biography,
    Comedy, Crime, Documentary, Drama,
    Family, Fantasy, History, Horror, Music,
    Mystery, Romance, Sport, War
    }

namespace MovieStore.Models
    {

    public class Genre
        {
        public int Id { get; set; }

        public string Type { get; set; }

        public int MovieId { get; set; }

        public ICollection<MovieGenre> MovieGenre { get; set; }

        }
    }
