using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BoxOffice.Models;

namespace BoxOffice.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private BoxOfficeContext db = new BoxOfficeContext();

        //
        // GET: /Admin/
        /// <summary>
        /// Display the admin's dashboard with movie of the week, most rented movies and comment's needing action
        /// </summary>
        /// <returns>the view</returns>
        public ActionResult Index()
        {
            // add a field for the pages controller, to facilitate error checking in the view
            ViewData["page"] = "admin";

            /* add movie of the week to ViewData, so we can display it */
            // query for the MOTW
            var result = (from m in db.Movies
                          where m.MovieOfTheWeek == true
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
            result = (from m in db.Movies
                     orderby m.RentalCount
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

            /* flagged comments */
            // query for comments that need administrative action
            var flagged = (from c in db.Comments
                           where ((c.Flag == 5) || (c.Flag > 5))
                           orderby c.Flag
                           select c).ToList();

            // check if there are flagged comments or fail gracefully
            if (flagged.Count == 0)
            {
                ViewData["flaggedComments"] = null;
            }
            else
            {
                ViewData["flaggedComments"] = flagged;
            }

            // return the Index view with all the data necessary for templating
            return View();
        }

        //
        // POST: /Admin/MessageUser
        [HttpPost]
        public JsonResult MessageUser()
        {
            return Json(new { implemented = false });
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
                try
                {
                    mc.persistMovie(add);

                    return Json(new { success = true });
                }
                catch (Exception e)
                {

                    return Json(new { exception = true, message = e.Message });
                }
            }
            // If we got this far, something failed
            return Json(new { errors = mc.GetErrorsFromModelState() });
        }

        //
        // GET: /Movies/ajaxSearch

        /// <summary>
        /// Controller Action for the search field
        /// </summary>
        /// <param name="q">The name to be searched for</param>
        /// <returns>A list of potential matches</returns>
        [HttpGet]
        public JsonResult ajaxSearch(String q)
        {
            var requestUrl = "http://api.themoviedb.org/2.1/Movie.search/en/json/b0f4c9d847ceda92061d4090b470dc10/" + q;
            try
            {
                HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                var jsonResponse = new JsonResult();
                jsonResponse.Data = response.GetResponseStream();
                return (jsonResponse);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                // if we get here, something failed
                return Json(new { fail = true });
            }
        }

        /// <summary>
        /// Processes a user's search request
        /// </summary>
        /// <param name="searchTerm">The search term</param>
        /// <returns>a response</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Search(string searchTerm)
        {
            if (searchTerm == string.Empty)
            {
                return View();
            }
            else
            {
                // if the search contains only one result return details
                // otherwise a list
                var movies = from a in db.Movies
                             where a.Name.Contains(searchTerm)
                             orderby a.Name
                             select a;

                if (movies.Count() == 0)
                {
                    return View("notfound");
                }

                if (movies.Count() > 1)
                {
                    return View("List", movies);
                }
                else
                {
                    return RedirectToAction("Details",
                        new { id = movies.First().MovieID });
                }
            }
        }
    }
}
