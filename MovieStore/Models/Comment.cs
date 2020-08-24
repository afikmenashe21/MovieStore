using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Published { get; set; }
        public User Author { get; set; }
        public Movie Movie { get; set; }

    }
}
