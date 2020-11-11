using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RandomUser.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public Name Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string ThumbnailImage { get; set; }
        [Required]
        public string MainImage { get; set; }
    }
}