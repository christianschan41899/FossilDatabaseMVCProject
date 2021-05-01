using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using FossilModel.Models;
using UserModel.Models;

namespace DigSiteModel.Models
{
    public class DigSite
    {
        [Key]
        public int DigSiteID {get; set;}

        [Required]
        [Display(Name="Site Name")]
        public string SiteName {get; set;}

        [Required]
        [Display(Name="Latitude (+ for North, - for South)")]
        public float SiteLatitude {get; set;}

        [Required]
        [Display(Name="Longitude (+ for East, - for West)")]
        public float SiteLongitude {get; set;}

        public string ImageSrc {get; set;}

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        /**************************************************
            Navigation properties.
            .Include to access any non-primitive/DateTime 
            data type fields
        ***************************************************/
        //One to many with Fossil
        public List<Fossil> FossilsUncovered {get; set;}

        //One to many with User
        public int UserID {get; set;}
        public User AddedBy {get; set;}
    }
}