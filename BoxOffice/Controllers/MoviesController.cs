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
                var movie = (from m in db.Movies
                             where m.MovieID == id
                             select m).First();

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

                var user = (from u in db.Users
                            where u.Username == User.Identity.Name
                            select u).First();

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
                    var movie = persistMovie(m, add.DVDs, add.Price, add.MovieOfTheWeek);
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
            old.MovieOfTheWeek = false;

            movie.MovieOfTheWeek = true;

            db.SaveChanges();

            return View();
        }

        /// <summary>
        /// Saves a movie from TheMovieDB to BoxOffices db
        /// </summary>
        /// <param name="newMovie">An AddMovie model containing all info</param>
        public void persistMovie(AddMovieModel model)
        {
            // create request url
            string UrlRequest = "http://api.themoviedb.org/2.1/Movie.getInfo/en/xml/b0f4c9d847ceda92061d4090b470dc10/" + model.TMDbID;

            /* make request */
            XDocument tmdbMovie = null;
            try
            {
                HttpWebRequest request = WebRequest.Create(UrlRequest) as HttpWebRequest;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                tmdbMovie = XDocument.Load(response.GetResponseStream());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new RequestFailedException();
            }

            /* parse xml */
            // Lists to fill
            var categories = new List<Category>();
            var images = new List<Image>();
            var cast = new List<CastMember>();
            var studios = new List<Studio>();
            var countries = new List<Country>();
            var dvds = new List<DVD>();

            try
            {
                #region movie
                // create a new Movie object and fill it with data parsed from the xml
                var movie = (from m in tmdbMovie.Descendants("movie")
                             select new Movie
                             {
                                 Adult = bool.Parse(m.Element("adult").Value),
                                 Alternative_name = m.Element("alternative_name").Value,
                                 Cast = cast,
                                 Categories = categories,
                                 Certification = m.Element("certification").Value,
                                 Comments = new List<Comment>(),
                                 Countries = countries,
                                 DateAdded = DateTime.Now,
                                 DateReleased = DateTime.ParseExact(m.Element("released").Value, @"yyyy-MM-dd", CultureInfo.InvariantCulture),
                                 DVDs = dvds,
                                 Homepage = m.Element("homepage").Value,
                                 Images = images,
                                 Imdb_id = m.Element("imdb_id").Value,
                                 Language = m.Element("language").Value,
                                 MovieID = int.Parse(m.Element("id").Value),
                                 MovieOfTheWeek = model.MovieOfTheWeek,
                                 Name = m.Element("name").Value,
                                 Original_name = m.Element("original_name").Value,
                                 Overview = m.Element("overview").Value,
                                 Price = model.Price,
                                 Rating = null,
                                 Rating_by_moviedb = float.Parse(m.Element("rating").Value),
                                 Ratings = new List<Rating>(),
                                 Rentals = new List<Rental>(),
                                 Studios = studios,
                                 Tagline = m.Element("tagline").Value,
                                 Trailer = m.Element("trailer").Value,
                                 Translated = bool.Parse(m.Element("translated").Value),
                                 Type = m.Element("type").Value,
                                 Url = m.Element("url").Value,
                                 Votes = null,
                                 Votes_by_moviedb = int.Parse(m.Element("votes").Value)
                             }).First();

                // save the new movie
                db.Movies.Add(movie);
                db.SaveChanges();

                #endregion

                #region cast
                // now parse the cast
                foreach (var person in (from castMembers
                                            in tmdbMovie.Descendants("person")
                                        select castMembers))
                {
                    // create new CastMember
                    var member = new CastMember
                    {
                        Cast_id = int.Parse(person.Attribute("cast_id").Value),
                        // CastMemberID will be generated
                        Character = person.Attribute("character").Value,
                        Department = person.Attribute("department").Value,
                        Job = person.Attribute("job").Value,
                        // Movie + Movie ID added after obj creation
                        Movie = movie,
                        Name = person.Attribute("name").Value,
                        Order = int.Parse(person.Attribute("order").Value),
                        Url = person.Attribute("url").Value
                    };

                    // save CastMember
                    cast.Add(member);
                }

                // add new CastMembers to DB
                cast.ForEach(cm => db.CastMembers.Add(cm));

                // now add FKs
                foreach (var member in cast)
                {
                    member.Movie = movie;
                    member.MovieID = movie.MovieID;
                }
                movie.Cast = cast;

                // save
                db.SaveChanges();

                #endregion

                #region category
                // loop over all category elements in the tmcb xml
                foreach (var category in
                    (from elements
                         in tmdbMovie.Descendants("category")
                     select elements))
                {
                    /* check if categories exist already */
                    var dbCategory = (from c in db.Categories
                                      where (c.CategoryID == (int.Parse(category.Attribute("id").Value)))
                                      select c).First();

                    /* if yes, add existing */
                    if (dbCategory != null)
                    {
                        categories.Add(dbCategory);
                    }
                    /* if no, parse it */
                    else
                    {
                        var c =
                            new Category
                            {
                                CategoryID = int.Parse(category.Attribute("id").Value),
                                // Movies will be set later on
                                Movies = new List<Movie>(),
                                Name = category.Attribute("name").Value,
                                Type = category.Attribute("type").Value,
                                Url = category.Attribute("url").Value
                            };

                        categories.Add(c);
                        db.Categories.Add(c);
                    }
                }
                // now save categories
                db.SaveChanges();

                // add FKs
                categories.ForEach(c => c.Movies.Add(movie));
                categories.ForEach(c => movie.Categories.Add(c));

                // now save categories
                db.SaveChanges();

                #endregion

                #region studio

                /* check if studio exists already in DB, if not, parse */
                foreach (var studio in (from elements
                                            in tmdbMovie.Descendants("studio")
                                        select elements))
                {
                    var dbStudio = (from s in db.Studios
                                    where (s.StudioID == (int.Parse(studio.Attribute("id").Value)))
                                    select s).First();

                    if (dbStudio != null)
                    {
                        studios.Add(dbStudio);
                    }
                    else
                    {
                        var s =
                            new Studio
                            {
                                Movies = new List<Movie>(),
                                Name = studio.Attribute("name").Value,
                                StudioID = int.Parse(studio.Attribute("id").Value),
                                Url = studio.Attribute("url").Value
                            };

                        studios.Add(s);
                        db.Studios.Add(s);
                    }
                }

                /* add FKs */
                studios.ForEach(s => s.Movies.Add(movie));
                studios.ForEach(s => movie.Studios.Add(s));

                /* save changes */
                db.SaveChanges();

                #endregion

                #region country

                /* check if studio exists already in DB, if not, parse */
                foreach (var country in (from elements
                                            in tmdbMovie.Descendants("country")
                                         select elements))
                {
                    var dbCountry = (from c in db.Countries
                                     where c.Code.Equals(country.Attribute("code").Value)
                                     select c).First();

                    if (dbCountry != null)
                    {
                        countries.Add(dbCountry);
                    }
                    else
                    {
                        var c =
                            new Country
                            {
                                Movies = new List<Movie>(),
                                Name = country.Attribute("name").Value,
                                Code = country.Attribute("code").Value,
                                Url = country.Attribute("url").Value
                            };

                        countries.Add(c);
                        db.Countries.Add(c);
                    }
                }

                /* add FKs */
                countries.ForEach(s => s.Movies.Add(movie));
                countries.ForEach(s => movie.Countries.Add(s));

                /* save changes */
                db.SaveChanges();

                #endregion

                #region image

                /* check if studio exists already in DB, if not, parse */
                images = (from image
                         in tmdbMovie.Descendants("image")
                          where image.Attribute("type").Value.Equals("poster")
                          select new Image
                          {
                              ImageID = image.Attribute("id").Value,
                              Size = image.Attribute("size").Value,
                              Type = image.Attribute("type").Value,
                              Url = image.Attribute("url").Value
                          }).ToList();

                /* save changes */
                images.ForEach(i => db.Images.Add(i));

                images.ForEach(i => movie.Images.Add(i));
                db.SaveChanges();

                #endregion

                #region dvd

                /* create new DVDs according to number in AddMovie model */
                for (int i = 0; i < model.DVDs; i++)
                {
                    var dvd = new DVD
                    {
                        Movie = new Movie(),
                        Rentals = new List<Rental>(),
                        State = "new"
                    };

                    dvds.Add(dvd);
                    db.DVDs.Add(dvd);
                }

                foreach (var d in dvds)
                {
                    d.Movie = movie;
                    d.MovieID = movie.MovieID;
                    movie.DVDs.Add(d);
                }

                db.SaveChanges();

                #endregion
            }
            catch (Exception e)
            {
                throw e;
            }

            return;
        }

        public IEnumerable<string> GetErrorsFromModelState()
        {
            return ModelState.SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage));
        }

    }
}