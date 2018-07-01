using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Basic_ASP.NET_Web_Application.Models;
using Microsoft.Data.Sqlite;

namespace Basic_ASP.NET_Web_Application.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult People()
        {
            using (SqliteConnection db = new SqliteConnection("Data Source=test.db"))
            {
                //Open connection to this projects sqlite3 database
                db.Open();

                //Default message
                ViewData["message"] = "Enter in a name and an age.";

                //If the user is submiting a person to add to the database.
                if (Request.Method == "POST")
                {
                    string name = Request.Form["name"]; //For adding a person
                    string deleteName = Request.Form["deleteName"]; //For removing a person

                    if (!Equals(name, null) && name.Length > 0)
                    {
                        //Get the name of the person from the first box
                        name = Request.Form["name"];

                        //Age of the person
                        int age = 0;

                        try
                        {
                            age = Convert.ToInt32(Request.Form["age"]);
                            SqliteCommand insertCommand = new SqliteCommand("INSERT INTO people VALUES('" + name + "'," + age + ");", db);
                            insertCommand.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            //If the user puts in something that is not a integer value then this message will be displayed.
                            ViewData["message"] = "Please enter a whole number value for the age.";
                        }
                    }
                    else if(!Equals(deleteName, null) && deleteName.Length > 0)
                    {
                        SqliteCommand deleteCommand = new SqliteCommand("DELETE FROM people WHERE name = '" + deleteName + "';" ,db);
                        deleteCommand.ExecuteNonQuery();
                        ViewData["message"] = deleteName + " was deleted";
                    }
                    else
                    {
                        ViewData["message"] = "No one was added or removed";
                    }
                }

                //Get all the people currently in the database
                SqliteCommand selectQuery = new SqliteCommand("SELECT * FROM people;",db);
                SqliteDataReader results = selectQuery.ExecuteReader();

                //Generate html list with all the people in the database. This will be sent to the page and diplayed as html.
                string  html = "<ul id='myList'>";
                while (results.Read())
                {
                    html += "<li>";
                    string name = results.GetString(0);
                    int age = results.GetInt32(1);
                    html += name + ", ";
                    html += age.ToString();
                    html += "</li>";
                    
                }
                html += "</ul>";

                //Close database
                db.Close(); 

                //Send html to the page as raw html
                ViewBag.HtmlStr = html;
            }
            
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
