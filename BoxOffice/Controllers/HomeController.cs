using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoxOffice.Models;

namespace BoxOffice.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new BoxOfficeContext();

            // add a field for the pages controller, to facilitate error checking in the view
            ViewData["page"] = "home";

            /* add movie of the week to ViewData, so we can display it */
            // query for the MOTW
            var result = (from m in db.Movies
                          where m.isMovieOfTheWeek == true
                          select m).ToList();

            // now check if MOTW is set, if not, fail gracefully
            if (result.Count == 0)
            {
                ViewData["movieOfTheWeek"] = null;
                result = null;
            }
            else
            {
                ViewData["movieOfTheWeek"] = result.First();
                result = null;
            }

            /* hot movies */
            // query for the ten most rented movies
            var movies = db.Movies.ToList();
            result = (from m in db.Movies
                      orderby m.Rentals.Count() ascending
                      select m).ToList();

            // check if there are rented movies, otherwise fail gracefully
            if (result.Count == 0)
            {
                ViewData["hotMovies"] = null;
            }
            else
            {
                ViewData["hotMovies"] = result.Take(10).ToList();
            }         

            return View();
        }
    }
}
