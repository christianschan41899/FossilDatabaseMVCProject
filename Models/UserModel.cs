using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using FossilModel.Models;
using DigSiteModel.Models;
using MuseumModel.Models;

namespace UserModel.Models
{
    public class User
    {
        [Key]
        public int UserID {get; set;}

        [Required]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long!")]
        public string Username {get; set;}

        [EmailAddress]
        [Required]
        public string Email {get; set;}

        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long!")]
        [Required]
        public string Password {get; set;}

        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string Confirm {get; set;}

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        /**************************************************
            Navigation properties.
            .Include to access any non-primitive/DateTime 
            data type fields
        ***************************************************/

        //One to many with Fossil
        public List<Fossil> FossilsCreated {get; set;}

        //One to many with Dig Site
        public List<DigSite> DigSitesCreated {get; set;}

        //One to many with Museum
        public List<Museum> MuseumsCreated {get; set;}
        
    }


    //For logins
    public class LoginUser
    {
        [Required]
        [Display(Name = "Email")]
        public string LoginEmail {get; set;}

        [Required]
        [DataType(DataType.Password)]
        [Display(Name="Password")]
        public string LoginPassword { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }

}