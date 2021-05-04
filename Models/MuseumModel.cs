using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using FossilModel.Models;
using UserModel.Models;
using ImageUpload.Models;

namespace MuseumModel.Models
{
    public class Museum
    {
        [Key]
        public int MuseumID {get; set;}

        [Required]
        [Display(Name="Name")]
        public string MuseumName {get; set;}

        [Required]
        [Display(Name="Latitude (+ for North, - for South)")]
        public float MuseumLatitude {get; set;}

        [Required]
        [Display(Name="Longitude (+ for East, - for West)")]
        public float MuseumLongitude {get; set;}

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        /**************************************************
            Navigation properties.
            .Include to access any non-primitive/DateTime 
            data type fields
        ***************************************************/
         //One to many with Fossil
        public List<Fossil> FossilsOwned {get; set;}

        //One to many with User
        public int UserID {get; set;}
        public User AddedBy {get; set;}

    }
}