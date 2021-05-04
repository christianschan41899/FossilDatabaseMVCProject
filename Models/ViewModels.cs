using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using FossilModel.Models;
using ImageUpload.Models;

namespace ViewModels.Models
{
    /*********************************************************************
        Class to hold Fossil AND ImageUpload so images can be uploaded
        on the fossil's description page
    **********************************************************************/
    public class FossilImage{
        public Fossil fossil{get; set;}
        public ImageModel image{get; set;}
    }
}