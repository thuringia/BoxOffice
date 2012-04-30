using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using BoxOffice.Models;
using BoxOffice.ActionFilters;
using BoxOffice.Exceptions;
using System.Net;
using System.Xml.Linq;
using System.Globalization;

namespace BoxOffice.Controllers
{
    [JsonRequestBehavior]
    public class MoviesController : Controller
    {
        private BoxOfficeContext db = new BoxOfficeContext();

        //
        // GET: /Movies/
        /// <summary>
        /// controller action (CA) for the controller's start page
        /// </summary>
        /// <returns>a list of all movies</returns>
        public ViewResult Index()
        {
            return View(db.Movies.ToList());
        }

        //
        // GET: /Movies/Details/5
        /// <summary>
        /// CA for a movie's details page
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the movie which is to be displayed</returns>
        public ViewResult Details(int id)
        {
            Movie movie = db.Movies.Find(id);
            return View(movie);
        }

        //
        // GET: /Movies/Rent/{id}

        /// <summary>
        /// Handles a user's request to add a movie to his queue
        /// </summary>
        /// <param name="id">the ID of the desired movie</param>
        /// <returns>
        /// {"success":true} if movie was successfully added to queue,
        /// {"authenticated":false} if the user wasn't logged in during the request,
        /// {"fail":true} if the movie couldn't be added to the queue
        /// </returns>
        [HttpGet]
        public JsonResult Rent(int id)
        {
            if (Request.IsAuthenticated)
            {
                try
                {
                    rentMovie(id);
                }
                catch (AddToQueueFailedException)
                {

                    return Json(new { fail = true });
                }

                return Json(new { success = true });
            }
            else
            {
                return Json(new { authenticated = false });
            }
        }

        /// <summary>
        /// the code which created a new queue item
        /// </summary>
        /// <param name="id"></param>
        private void rentMovie(int id)
        {
            try
            {
                var movie = db.Movies.First(m => m.MovieID == id);

                var newQueueItem = new Rental
                {
                    DateDue = null,
                    DateOfRental = DateTime.Now,
                    DateReturned = null,
                    DateSent = null,
                    Dvd = new DVD(),
                    DvdID = null
                };
                db.Rentals.Add(newQueueItem);
                db.SaveChanges();

                var user = db.Users.First(u => u.Username == User.Identity.Name);

                user.Queue.Add(newQueueItem);
                newQueueItem.User = user;
                newQueueItem.UserID = user.UserID;
                movie.Rentals.Add(newQueueItem);
                newQueueItem.Movie = movie;
                newQueueItem.MovieID = movie.MovieID;
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw new AddToQueueFailedException();
            }
        }

        //
        // GET: /Movies/Flag/{id}
        /// <summary>
        /// Handles a user's request to flag a comment
        /// </summary>
        /// <param name="id">the ID of the comment to flag</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Flag(int id)
        {
            if (Request.IsAuthenticated)
            {
                try
                {
                    flagComment(id);
                }
                catch (FlagCommentFailedException)
                {

                    return Json(new { fail = true });
                }

                return Json(new { success = true });
            }
            else
            {
                return Json(new { authenticated = false });
            }
        }

        private void flagComment(int id)
        {
            try
            {
                var toFlag = (from c in db.Comments
                              where c.CommentID == id
                              select c).First();
                if (toFlag.Flag == null)
                {
                    toFlag.Flag = new int();
                    toFlag.Flag = 1;
                }
                else
                {
                    toFlag.Flag += 1;
                }

                db.SaveChanges();
            }
            catch (Exception)
            {

                throw new FlagCommentFailedException();
            }
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
            var movies = (from m in db.Movies
                          where m.Name.Contains(q)
                          orderby m.Name
                          select m).Take(10);
            List<String> response = new List<string>();
            movies.ToList().ForEach(m => response.Add(m.Name));
            return Json(response.ToArray());
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
                return View("Index");
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
                    return View("Index");
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
        //
        // GET: /Movies/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Movies/Create
        /// <summary>
        /// Adds a new movie to BoxOffice's database
        /// </summary>
        /// <param name="add">An AddMovieModel containing all necessery information</param>
        /// <returns>A JSON response, {"success":true} on success, an</returns>
        [HttpPost]
        public JsonResult Create(AddMovieModel add, String returnUrl)
        {
            if (ModelState.IsValid)
            {
                /*
                var response = tmdb.MovieSearch(add.Name);

                if (response != null)
                {
                    var m = tmdb.GetMovieInfo(response.First().Id);
                    var movie = persistMovie(m, add.DVDs, add.Price, add.isMovieOfTheWeek);
                    return Json(new { success = true, redirect = returnUrl });
                }
                else
                {
                    ModelState.AddModelError("", "The movie could not be found.");
                }
                */
            }
            // If we got this far, something failed
            return Json(new { errors = GetErrorsFromModelState() });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// Sets the new movie of the week
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MovieOfTheWeek(Movie movie)
        {
            var old = db.Movies.Find(movie.MovieID);
            old.isMovieOfTheWeek = false;

            movie.isMovieOfTheWeek = true;

            db.SaveChanges();

            return View();
        }

        
        public IEnumerable<string> GetErrorsFromModelState()
        {
            return ModelState.SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage));
        }

    }
}