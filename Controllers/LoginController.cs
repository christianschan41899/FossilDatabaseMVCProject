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

namespace Login.Controllers
{
    public class LoginController : Controller
    {
        private MyContext dbContext;
        public LoginController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("signin")]
        public IActionResult SignInPage()
        {
            return View("SignIn");
        }

        [HttpPost("register")]
        public IActionResult CreateUser(User newUser)
        {
            if(ModelState.IsValid)
            {
                //Check if Email is already in use
                if(dbContext.Users.Any(u => u.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View("SignIn");
                }

                //Hash password
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                
                //Store in db and assign to Session
                dbContext.Add(newUser);
                dbContext.SaveChanges();
                HttpContext.Session.SetInt32("LoggedUser", newUser.UserID);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("SignIn");
            }
        }

        [HttpPost("login")]

        public IActionResult LoginHandler(LoginUser submitUser)
        {
            if(ModelState.IsValid)
            {
                User userInDb = dbContext.Users.FirstOrDefault(u => u.Email == submitUser.LoginEmail);
                // If no user exists with provided email
                if(userInDb == null)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid Email/Password");
                    return View("SignIn");
                }
                
                var hasher = new PasswordHasher<LoginUser>();
                // verify provided password against hash stored in db
                var result = hasher.VerifyHashedPassword(submitUser, userInDb.Password, submitUser.LoginPassword);
                
                // result can be compared to 0 for failure
                if(result == 0)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid Email/Password");
                    return View("SignIn");
                }
                //Save user in session
                HttpContext.Session.SetInt32("LoggedUser", userInDb.UserID);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("SignIn");
            }
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }

}