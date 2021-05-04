using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using UserModel.Models;
using DigSiteModel.Models;
using MuseumModel.Models;
using ImageUpload.Models;

namespace FossilModel.Models
{
    public class Fossil
    {
        [Key]
        public int FossilID {get; set;}

        [Required]
        [Display(Name="Name")]
        public string FossilName {get; set;}

        [Required]
        [Display(Name="Species")]
        public string FossilSpecies {get; set;}

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        /**************************************************
            Navigation properties.
            .Include to access any non-primitive/DateTime 
            data type fields
        ***************************************************/

        //One to many with Museum
        [Display(Name="Museum")]
        public int MuseumID {get; set;}
        public Museum LocatedAt {get; set;}

        //One to many with Dig Site
        [Display(Name="Dig Site")]
        public int DigSiteID {get; set;}
        public DigSite UnearthedAt {get; set;}

        //One to many with User
        public int UserID {get; set;}
        public User AddedBy {get; set;}

        //One to many with Image
        public List<ImageModel> FossilImages {get; set;}
    }
}