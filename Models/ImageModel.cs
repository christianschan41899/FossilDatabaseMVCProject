using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using FossilModel.Models;
using MuseumModel.Models;
using DigSiteModel.Models;
using Microsoft.AspNetCore.Mvc;

namespace ImageUpload.Models
{
    public class ImageModel
    {
        [Key]
        public int ImageID {get; set;}

        [Display(Name = "Image Name")]
        public string ImageName {get; set;}

        [NotMapped]
        [Required]
        [Display(Name = "Upload File")]
        //[RequestSizeLimit(4194304)]
        public IFormFile ImageFile {get; set;}

        /**************************************************
            Navigation properties.
            .Include to access any non-primitive/DateTime 
            data type fields
        ***************************************************/
        //One to many with Fossil
        public int FossilID {get; set;}
        public Fossil DescribesFossil {get; set;}
    }
}