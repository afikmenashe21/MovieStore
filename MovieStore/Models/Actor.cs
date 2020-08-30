using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models
    {
    public class Actor
        {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MovieId { get; set; }
        [DisplayName( "Filmography" )]
        public ICollection<MovieActor> MovieActor { get; set; }
        }
    }
