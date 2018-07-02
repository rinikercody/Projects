using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sessions.Models;

namespace Sessions.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult LogInOut()
        {
            if(Request.Method == "POST")
            {
                //The name that user entered.
                string username = Request.Form["username"];

                //This is the name of the authorized user that is compared to when logging in. 
                //If this were a real application then the username would probbally be stored in some type of database
                //and not hardcoded in.
                string name = "Cody";

                if (!Equals(username, null)) //Log in button pressed
                {
                    if (username == name)
                    {
                        //Create a cookie called session with the value of authorized.
                        Response.Cookies.Append("session", "authorized");

                        ViewData["message"] = "You logged in!";
                    }
                    else
                    {
                        ViewData["message"] = "Access Denied";
                    }
                }
                else //Log out button pressed
                {
                    ViewData["message"] = "You logged out.";

                    //Delete cookie and end session
                    Response.Cookies.Delete("session");
                }

            }
            return View();
        }

        public IActionResult SessionForm()
        {
            //Get session cookie
            string authorization = Request.Cookies["session"];

            if(!Equals(authorization,null) && authorization == "authorized") //User has a current session
            {
                ViewData["message"] = "Active Session. You can see this message because you are logged in!";
            }
            else //User did not log in and has no session data
            {
                ViewData["message"] = "You are not allowed to be here.";
            }
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
