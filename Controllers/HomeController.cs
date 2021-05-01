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
using UserModel.Models;
using FossilDigContext.Models;
using DigSiteModel.Models;
using FossilModel.Models;
using MuseumModel.Models;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
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
                .Include(fossil => fossil.UnearthedAt)
                .Include(fossil => fossil.LocatedAt)
                .FirstOrDefault(fossil => fossil.FossilID == fossilid);
            
            return View("FossilDisplay", displayFossil);
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
                .FirstOrDefault(fossil => fossil.FossilID == fossilid);
            
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
    }
}
