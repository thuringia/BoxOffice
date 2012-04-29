using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BoxOffice.Models;
using BoxOffice.ActionFilters;
using System.Xml.Linq;
using BoxOffice.Exceptions;
using System.Globalization;

namespace BoxOffice.Controllers
{
    //[Authorize(Roles = "Admin")]
    [JsonRequestBehavior]
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
        public ActionResult AddMovie(AddMovieModel add, String returnUrl)
        {
            if (ModelState.IsValid)
            {
                //try
                //{
                persistMovie(add);

                return View("Movies");
                //}
                //catch (Exception e)
                //{

                //    return View();
                //}
            }
            // If we got this far, something failed
            return View();
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
        /// Ignores a user's search request
        /// </summary>
        /// <param name="searchTerm">The search term</param>
        /// <returns>View("Movies")</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Search(string searchTerm)
        {
            return View(viewName: "Movies", model: db.Movies.ToList());
        }

        /// <summary>
        /// Processes the Admin's request to "delete" a movie.
        /// => Movie will be marked as unrentable/unqueable
        /// </summary>
        /// <param name="id">The MovieID to be deleted</param>
        /// <returns>
        /// {"success" = true} if successful,
        /// {"fail" = true} if unsuccessful
        /// </returns>
        [HttpGet]
        public JsonResult Delete(int id)
        {
            try
            {
                var movieToDelete = (from m
                                         in db.Movies
                                     where m.MovieID == id
                                     select m).First();
                movieToDelete.isRentable = false;
                db.SaveChanges();

                return Json(new {success = true});
            }
            catch (Exception e)
            {
                return Json(new { fail = true, message = e.Message });
            }

            // if we got here, something failed
            return Json(new { fail = true });
        }

        /// <summary>
        /// Makes a movie the movie of the week.
        /// If there is currently a MOTW, upromote it first
        /// </summary>
        /// <param name="id">The MovieID to become MOTW</param>
        /// <returns>
        /// {"success" = true} if successful,
        /// {"fail" = true} if unsuccessful
        /// </returns>
        [HttpGet]
        public JsonResult Promote(int id)
        {
            try
            {
                // check if there is a MOTW at present
                var currentPromo = from m
                                       in db.Movies
                                   where m.isMovieOfTheWeek == true
                                   select m;
                
                if (currentPromo.Count() != 0)
                {
                    // unpromote movie
                    currentPromo.First().isMovieOfTheWeek = false;
                    db.SaveChanges();
                }

                // promote new movie
                var newPromo = (from m in db.Movies
                                where m.MovieID == id
                                select m).First();

                newPromo.isMovieOfTheWeek = true;
                db.SaveChanges();

                return Json(new {success = true});
            }
            catch (Exception e)
            {

                return Json(new {fail = true, message = e.Message});
            }

            // if we got here, something failed
            return Json(new {fail = true});
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


            //try
            //{
            #region movie
            // check if the new movie is to be promoted
            if (model.MovieOfTheWeek == true)
            {
                // get old movie of the week
                var promoQueryResult = (from m
                                 in db.Movies
                                        where m.isMovieOfTheWeek == true
                                        select m);

                // check if there actually is a movie
                if (!promoQueryResult.Equals(null))
                {
                    var promo = promoQueryResult.First();
                    // unpromote it
                    promo.isMovieOfTheWeek = false;
                    db.SaveChanges();
                }
            }

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
                             isMovieOfTheWeek = model.MovieOfTheWeek,
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
            var persons = from c
                          in tmdbMovie.Descendants("cast").Elements()
                          select c;

            foreach (var person in persons)
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
            var categoriesXML = from elements
                                  in tmdbMovie.Descendants("categories").Elements()
                                select elements;
            foreach (var category in categoriesXML)
            {
                /* check if categories exist already */
                Int32 categoryId;
                int.TryParse(category.Attribute("id").Value, out categoryId);

                var dbCategory = (from c in db.Categories
                                  where (c.CategoryID == categoryId)
                                  select c);

                /* if yes, add existing */
                if (dbCategory.Count() != 0)
                {
                    categories.Add(dbCategory.First());
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
            foreach (var item in categories.ToList())
            {
                movie.Categories.Add(item);
                item.Movies.Add(movie);
                db.SaveChanges();
            }

            #endregion

            #region studio

            /* check if studio exists already in DB, if not, parse */
            var studiosInXML = from elements
                                   in tmdbMovie.Descendants("studios").Elements()
                               select elements;

            foreach (var studio in studiosInXML)
            {
                Int32 studioId;
                int.TryParse(studio.Attribute("id").Value, out studioId);

                var dbStudio = (from s in db.Studios
                                where (s.StudioID == studioId)
                                select s);

                if (dbStudio.Count() != 0)
                {
                    studios.Add(dbStudio.First());
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
            foreach (var item in studios.ToList())
            {
                item.Movies.Add(movie);
                movie.Studios.Add(item);
                db.SaveChanges();
            }

            #endregion

            #region country

            /* check if country exists already in DB, if not, parse */
            var countriesXML = from elements
                                   in tmdbMovie.Descendants("countries").Elements()
                               select elements;

            foreach (var country in countriesXML)
            {
                var code = country.Attribute("code").Value;

                var dbCountry = from c
                                    in db.Countries
                                where c.Code == code
                                select c;

                if (dbCountry.Count() != 0)
                {
                    countries.Add(dbCountry.First());
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
            foreach (var item in countries.ToList())
            {
                movie.Countries.Add(item);
                item.Movies.Add(movie);
                db.SaveChanges();
            }

            #endregion

            #region image

            /* check if studio exists already in DB, if not, parse */
            images = (from image
                          in tmdbMovie.Descendants("images").Elements()
                      where image.Attribute("type").Value.Equals("poster") && image.Attribute("size").Value.Equals("original")
                      select new Image
                      {
                          ImageID = image.Attribute("id").Value,
                          Size = image.Attribute("size").Value,
                          Type = image.Attribute("type").Value,
                          Url = image.Attribute("url").Value
                      }).ToList();

            /* save changes */
            images.ForEach(i => db.Images.Add(i));
            db.SaveChanges();

            images.ForEach(i => movie.Images.Add(i));
            db.SaveChanges();

            #endregion

            #region dvd

            /* create new DVDs according to number in AddMovie model */
            for (int i = 0; i < model.DVDs; i++)
            {
                var newDVD = new DVD
                {
                    Movie = movie,
                    MovieID = movie.MovieID,
                    Rentals = new List<Rental>(),
                    State = "new"
                };

                dvds.Add(newDVD);
            }

            // add DVDs to DB context
            dvds.ForEach(dvd => db.DVDs.Add(dvd));

            // add DVDs to movie
            dvds.ToList().ForEach(dvd => movie.DVDs.Add(dvd));

            db.SaveChanges();

            #endregion
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //    throw e;
            //}


            return;
        }

    }
}
