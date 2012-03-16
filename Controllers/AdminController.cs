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


    }
}
