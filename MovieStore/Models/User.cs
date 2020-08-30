using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models
    {
    public class User
        {
        public int Id { get; set; }
        //[RegularExpression( @"^[\w\d\@\_]+$" )] // Demand the username wiil be with any English characters/digits/@/_
        //[Required]
        public string UserName { get; set; }
        //[Required]
        public string Password { get; set; }
        //[RegularExpression( @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$" )] // Demand the email wiil be ___@___.___ 
        //[Required]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public ICollection<Review> Comments { get; set; }
        }
    }
