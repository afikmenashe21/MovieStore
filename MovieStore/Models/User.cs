using MovieStore.Models.UserPreferences;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models
    {
    public enum UserType
        {
        Admin,
        Customer
        }

    public class User
        {
        public int Id { get; set; }
        [RegularExpression( @"^[\w\d]+$" )] // Demand the username wiil be with any English characters/digits/@/_
        [Required]
        public string UserName { get; set; }
        [Required]
        public UserType Type { get; set; }
        [Required]
        public string Password { get; set; }
        [RegularExpression( @"^([\w\-\.]+)@(\w+)\.(\w+)$" )] // Demand the email wiil be ___@___.___ 
        [Required]
        public string Email { get; set; }
        [RegularExpression( @"^[\w]+$" )] // Demand the username wiil be with any English characters
        [Required]
        public string FirstName { get; set; }
        [RegularExpression( @"^[\w]+$" )] // Demand the username wiil be with any English characters
        [Required]
        public string LastName { get; set; }
        [RegularExpression( @"^([\w\s]+),([a-zA-z\s]+),([a-zA-z\s]+)$")] // Demand the Address wiil be ___ , ___ , ___
        [Required]
        public string Address { get; set; }
        public virtual ICollection<Review> Comments { get; set; }
        [DisplayName( "Suggestions" )]
        public virtual ICollection<UserGenre> UserGenre { get; set; }
        }
    }
