using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [RegularExpression(@"^[\w\s]+$")] // Demand the name wiil be with a-z or A-Z or digits characters
        [Required]
        public string Type { get; set; }
        public int MovieId { get; set; }

        public ICollection<MovieGenre> MovieGenre { get; set; }
        }
    }
