using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using UserModel.Models;
using FossilDigContext.Models;
using DigSiteModel.Models;
using FossilModel.Models;
using MuseumModel.Models;
using ImageUpload.Models;
using ViewModels.Models;


namespace Project.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(MyContext context, IWebHostEnvironment hostEnvironment)
        {
            dbContext = context;
            this._hostEnvironment = hostEnvironment;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.LoginID = HttpContext.Session.GetInt32("LoggedUser");
            ViewBag.MuseumList = dbContext.Museums
                .Include(mus => mus.FossilsOwned)
                .OrderBy(mus => mus.MuseumID);
            ViewBag.DigSiteList = dbContext.DigSites
                .Include(dig => dig.FossilsUncovered)
                .OrderBy(dig => dig.DigSiteID);
            return View();
        }

        /*************************
             Create Dig Site
        **************************/
        [HttpGet("digs/new")]
        public IActionResult DigForm()
        {
            if(HttpContext.Session.GetInt32("LoggedUser") == null)
            {
                return RedirectToAction("SignInPage", "Login");
            }
            return View("DigSiteForm");
        }

        [HttpPost("digs/new")]
        public IActionResult CreateDig(DigSite newDig)
        {
            if(ModelState.IsValid)
            {
                newDig.UserID = HttpContext.Session.GetInt32("LoggedUser").Value;
                dbContext.Add(newDig);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("DigSiteForm");
            }
        }

        /*************************
             Create Museum
        **************************/
        [HttpGet("museums/new")]
        public IActionResult MuseumForm()
        {
            if(HttpContext.Session.GetInt32("LoggedUser") == null)
            {
                return RedirectToAction("SignInPage", "Login");
            }
            return View("MuseumForm");
        }

        [HttpPost("museums/new")]
        public IActionResult CreateMuseums(Museum newMuseum)
        {
            if(ModelState.IsValid)
            {
                newMuseum.UserID = HttpContext.Session.GetInt32("LoggedUser").Value;
                dbContext.Add(newMuseum);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("MuseumForm");
            }
        }

        /*************************
             Create Fossil
        **************************/
        [HttpGet("fossils/new")]
        public IActionResult FossilForm()
        {
            if(HttpContext.Session.GetInt32("LoggedUser") == null)
            {
                return RedirectToAction("SignInPage", "Login");
            }
            ViewBag.DigList = dbContext.DigSites
                .OrderBy(dig => dig.DigSiteID);
            ViewBag.MuseumList = dbContext.Museums
                .OrderBy(mus => mus.MuseumID);
            return View("FossilForm");
        }

        [HttpPost("fossils/new")]
        public IActionResult CreateFossil(Fossil newFossil)
        {
            if(ModelState.IsValid)
            {
                newFossil.UserID = HttpContext.Session.GetInt32("LoggedUser").Value;
                dbContext.Add(newFossil);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("FossilForm");
            }
        }

        /*************************
             Display Dig Site
        **************************/
        [HttpGet("digs/{digid}")]
        public IActionResult GetDig(int digid)
        {
            DigSite displayDig = dbContext.DigSites
                .Include(dig => dig.AddedBy)
                .Include(dig => dig.FossilsUncovered)
                .FirstOrDefault(dig => dig.DigSiteID == digid);
            
            return View("DigSiteDisplay", displayDig);
        }

        /*************************
             Display Museum
        **************************/
        [HttpGet("museums/{museumid}")]
        public IActionResult GetMuseum(int museumid)
        {
            Museum displayMuseum = dbContext.Museums
                .Include(mus => mus.AddedBy)
                .Include(mus => mus.FossilsOwned)
                .FirstOrDefault(mus => mus.MuseumID == museumid);
            
            return View("MuseumDisplay", displayMuseum);
        }

        /*************************
             Display Fossil
        **************************/

        [HttpGet("fossils/{fossilid}")]
        public IActionResult GetFossil(int fossilid)
        {
            Fossil displayFossil = dbContext.Fossils
                .Include(fossil => fossil.AddedBy)
                .Include(fossil => fossil.FossilImages)
                .Include(fossil => fossil.UnearthedAt)
                .Include(fossil => fossil.LocatedAt)
                .FirstOrDefault(fossil => fossil.FossilID == fossilid);

            FossilImage displayData = new FossilImage{
                fossil = displayFossil
            };
            
            return View("FossilDisplay", displayData);
        }

        /*************************
             Delete Fossil
        **************************/
        [HttpGet("fossils/{fossilid}/delete")]
        public IActionResult DeleteFossil(int fossilid)
        {
            if(HttpContext.Session.GetInt32("LoggedUser") == null)
            {
                return RedirectToAction("SignInPage", "Login");
            }
            Fossil deleteFossil = dbContext.Fossils
                .Include(fossil => fossil.FossilImages)
                .FirstOrDefault(fossil => fossil.FossilID == fossilid);

            //Deleting a fossil will also need to delete all images associated with the fossil.
            foreach(var image in deleteFossil.FossilImages)
            {
                //retrieve image path
                var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Image", image.ImageName);
                //Check if image exists (make sure something isn't accidentally deleted or if an error is thrown)
                if(System.IO.File.Exists(imagePath))
                {
                    //Delete Image
                    System.IO.File.Delete(imagePath);
                }
                //remove image's database entry
                dbContext.Images.Remove(image);
            }
            
            dbContext.Fossils.Remove(deleteFossil);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        /*************************
             Edit Fossil
        **************************/

        [HttpGet("fossils/{fossilid}/edit")]
        public IActionResult EditFossilForm(int fossilid)
        {
            if(HttpContext.Session.GetInt32("LoggedUser") == null)
            {
                return RedirectToAction("SignInPage", "Login");
            }
            Fossil displayFossil = dbContext.Fossils
                .Include(fossil => fossil.UnearthedAt)
                .Include(fossil => fossil.LocatedAt)
                .FirstOrDefault(fossil => fossil.FossilID == fossilid);

            ViewBag.DigList = dbContext.DigSites
                .OrderBy(dig => dig.DigSiteID);
            ViewBag.MuseumList = dbContext.Museums
                .OrderBy(mus => mus.MuseumID);
            
            return View("EditFossil", displayFossil);
        }

        [HttpPost("fossils/{fossilid}/edit")]
        public IActionResult EditFossil(int fossilid, Fossil updateData)
        {
            Fossil editFossil = dbContext.Fossils
                    .FirstOrDefault(fossil => fossil.FossilID == fossilid);
            if(ModelState.IsValid)
            {
                editFossil.FossilName = updateData.FossilName;
                editFossil.FossilSpecies = updateData.FossilSpecies;
                editFossil.DigSiteID = updateData.DigSiteID;
                editFossil.MuseumID = updateData.MuseumID;
                dbContext.SaveChanges();
                return Redirect($"../{fossilid}");
            }
            else
            {
                ViewBag.DigList = dbContext.DigSites
                    .OrderBy(dig => dig.DigSiteID);
                ViewBag.MuseumList = dbContext.Museums
                    .OrderBy(mus => mus.MuseumID);
                
                return View("EditFossil", editFossil);
            }
        }

        /*****************************
            Handle image upload
        ******************************/
        [HttpPost("fossils/{fossilid}/addImage")]
        [RequestSizeLimit(4194304)] //4MB Upload limit
        public async Task<IActionResult> CreateFossilImage(FossilImage newImage,int fossilid)
        {
            //Check if our submit actually has an image, otherwise redirect back to site.
            if(newImage.image != null)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(newImage.image.ImageFile.FileName); //Grabs string of file name
                string extension = Path.GetExtension(newImage.image.ImageFile.FileName);//Grabs string of file extension
                //Create new file name based on DateTime to avoid duplicate file names.
                fileName = fileName + DateTime.Now.ToString("yyyyMMddmmss") + extension;
                //Get path image will be saved to
                string path = Path.Combine(wwwRootPath+"/Image" , fileName);
                //Put image in wwwroot folder
                using(var fileStream = new FileStream(path, FileMode.Create))
                {
                    await newImage.image.ImageFile.CopyToAsync(fileStream);
                }
                ImageModel addImage = new ImageModel{
                    FossilID = fossilid,
                    ImageName = fileName,
                    ImageFile = newImage.image.ImageFile
                };
                dbContext.Add(addImage);
                await dbContext.SaveChangesAsync();
                return Redirect($"../{fossilid}");
            }
            else
            {
                return Redirect($"../{fossilid}");
            }
            
        }
    }

}
