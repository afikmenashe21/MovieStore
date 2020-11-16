using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models
{
    public class Actor
    {
        public int Id { get; set; }
        [RegularExpression(@"^[\w\d\s]+$")] // Demand the username wiil be with any English characters
        [Required]
        public string Name { get; set; }
        public int MovieId { get; set; }
        [DisplayName("Filmography")]
        public ICollection<MovieActor> MovieActor { get; set; }
    }
}
