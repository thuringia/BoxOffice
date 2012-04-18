using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoxOffice.Models;
using TheMovieDb;

namespace BoxOffice.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private BoxOfficeContext db = new BoxOfficeContext();

        //
        // GET: /Admin/

        public ActionResult Index()
        {                    
            // add movie of the week to ViewData, so we can display it
            ViewData["movieOfTheWeek"] = from m in db.Movies
                                         where m.MovieOfTheWeek == true
                                         select m;

            // create a dictionary for our rental charts
            var rentalCharts = new Dictionary<Movie, int>();

            // loop over all movies to get their rental count
            foreach (var item in db.Movies.ToList())
            {
                int rentalCount = 0;
                item.DVDs.ToList().ForEach(s => rentalCount += s.Rentals.Count());
                rentalCharts.Add(item, rentalCount);
            }

            // sort the charts and get the 10 most rented
            ViewData["hotMovies"] = (from m in rentalCharts
                                    orderby m.Value
                                    select m).Take(10);

            // get all comments that need administrative action

            ViewData["flaggedComments"] = from c in db.Comments
                                          where (c.Flag == 5) || (c.Flag > 5)
                                          orderby c.Flag
                                          select c;
            return View();
        }

        //
        // GET: /Admin/Movies

        public ActionResult Movies()
        {
            return View(db.Movies.ToList());
        }

        //
        // GET: /Admin/AddMovie
        /// <summary>
        /// GETs the AddMovie view
        /// </summary>
        /// <returns>AddMovie view</returns>
        public ActionResult AddMovie()
        {
            return View();
        }

        //
        // POST: /Admin/AddMovie
        /// <summary>
        /// Adds a new movie to BoxOffice's database
        /// </summary>
        /// <param name="add">An AddMovieModel containing all necessery information</param>
        /// <returns>A JSON response, {"success":true} on success, an</returns>
        [HttpPost]
        public JsonResult AddMovie(AddMovieModel add, String returnUrl)
        {
            var mc = new MoviesController();

            if (ModelState.IsValid)
            {
                var tmdb = new TmdbApi("b0f4c9d847ceda92061d4090b470dc10");
                var response = tmdb.MovieSearch(add.Name);

                if (response != null)
                {
                    var m = tmdb.GetMovieInfo(response.First().Id);
                    var movie = mc.persistMovie(m, add.DVDs, add.Price, add.MovieOfTheWeek);
                    return Json(new { success = true, redirect = returnUrl });
                }
                else
                {
                    ModelState.AddModelError("", "The movie could not be found.");
                }
            }
            // If we got this far, something failed
            return Json(new { errors = mc.GetErrorsFromModelState() });
        }
    }
}
