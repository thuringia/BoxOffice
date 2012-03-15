using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoxOffice.Models;
using MvpRestApiLib;
using TheMovieDb;

namespace BoxOffice.Controllers
{ 
    public class MoviesController : Controller
    {
        private BoxOfficeContext db = new BoxOfficeContext();
        private TheMovieDb.TmdbApi tmdb = new TmdbApi("b0f4c9d847ceda92061d4090b470dc10");

        //
        // GET: /Movies/

        public ViewResult Index()
        {
            return View(db.Movies.ToList());
        }

        //
        // GET: /Movies/Details/5

        public ViewResult Details(int id)
        {
            Movie movie = db.Movies.Find(id);
            return View(movie);
        }

        //
        // DELETE: /Movies/{id}
        
        [HttpDelete]
        [EnableJson, EnableXml]
        //[Authorize(Roles="Admin")]
        public ActionResult Index(int id)
        {
            db.Movies.Remove(db.Movies.Find(id));
            db.SaveChanges();

            return View();
        }

        private bool editMovie(Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();

                return true;
            }
            return false;
        }

        //
        // PUT: /Movies/{id}

        [HttpPut]
        [EnableJson, EnableXml]
        //[Authorize(Roles="Admin")]
        public ActionResult Index(Movie movie)
        {
            if (editMovie(movie))
            {
                return View();
            }
            return View(movie);
        }


        //
        // GET: /Movies/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Movies/Create

        [HttpPost]
        public ActionResult Create(AddMovieModel add)
        {
            if (ModelState.IsValid)
            {
                var response = tmdb.MovieSearch(add.Name);

                if (response != null)
                {
                    var m = tmdb.GetMovieInfo(response.First().Id);
                    var movie = persistMovie(m, add.DVDs, add.Price);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(add);
                }
            }
            return View(add);
        }

        public Movie AddMovieTest(AddMovieModel add)
        {
            if (ModelState.IsValid)
            {
                var response = tmdb.MovieSearch(add.Name);

                if (response != null)
                {
                    var m = tmdb.GetMovieInfo(response.First().Id);
                    var movie = persistMovie(m, add.DVDs, add.Price);
                    return movie;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }
        
        //
        // GET: /Movies/Edit/5
 
        public ActionResult Edit(int id)
        {
            Movie movie = db.Movies.Find(id);
            return View(movie);
        }

        //
        // POST: /Movies/Edit/5

        [HttpPost]
        public ActionResult Edit(Movie movie)
        {
            if (editMovie(movie))
            {
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        //
        // GET: /Movies/Delete/5
 
        public ActionResult Delete(int id)
        {
            Movie movie = db.Movies.Find(id);
            return View(movie);
        }

        //
        // POST: /Movies/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// Saves a movie from TheMovieDB to BoxOffices db
        /// </summary>
        /// <param name="tmdbMovie"></param>
        /// <returns></returns>
        private Movie persistMovie(TmdbMovie tmdbMovie, int dvdi, decimal price)
        {
            // Lists to fill
            List<Category> categories = new List<Category>();
            List<Image> images = new List<Image>();
            List<Person> persons = new List<Person>();
            List<CastMember> cast = new List<CastMember>();
            List<Studio> studios = new List<Studio>();
            List<Country> countries = new List<Country>();
            List<DVD> dvds = new List<DVD>();

            #region movie
            
            // create new movie object
            var movie = new Movie();        

            // set properties
            movie.Adult = tmdbMovie.Adult;
            movie.Alternative_name = tmdbMovie.AlternativeName;
            movie.Cast = cast;
            movie.Categories = categories;
            movie.Certification = tmdbMovie.Certification;
            movie.Comments = new List<Comment>;
            movie.Countries = countries;
            movie.DateReleased = DateTime.Parse(tmdbMovie.Released);
            movie.DVDs = dvds;
            movie.Homepage = tmdbMovie.Homepage;
            movie.Images = images;
            movie.Imdb_id = tmdbMovie.ImdbId;
            movie.Language = tmdbMovie.Language;
            movie.MovieID = tmdbMovie.Id;
            movie.Name = tmdbMovie.Name;
            movie.Original_name = tmdbMovie.OriginalName;
            movie.Overview = tmdbMovie.Overview;
            movie.Price = price;
            movie.Rating_by_moviedb = float.Parse(tmdbMovie.Rating);
            movie.Ratings = new List<Rating>();
            movie.Studios = studios;
            movie.Tagline = tmdbMovie.Tagline;
            movie.Trailer = tmdbMovie.Trailer;
            movie.Translated = tmdbMovie.Translated;
            movie.Type = tmdbMovie.Type;
            movie.Url = tmdbMovie.Url;
            movie.Votes_by_moviedb = int.Parse(tmdbMovie.Votes);

            // add movie to db
            db.Movies.Add(movie);

            // save changes
            db.SaveChanges();

            #endregion

            # region Cast
            // Add Cast
            foreach (var item in tmdbMovie.Cast)
            {
                cast.Add(new CastMember
                {
                    Cast_id = item.CastId,
                    CastMemberID = item.Id,
                    Character = item.Character,
                    Department = item.Department,
                    Job = item.Job,
                    Movie = movie,
                    MovieID = movie.MovieID,
                    Name = item.Name,
                    Order = item.Order,
                    Url = item.Url
                });
            }
            cast.ForEach(s => db.CastMembers.Add(s));
            cast.ForEach(s => movie.Cast.Add(s));
            db.SaveChanges();

            #endregion

            #region category

            // Check if category exists already
            foreach (var item in tmdbMovie.Genres)
            {
                var id = db.Categories.Find(item.Id);

                // Add new category
                if (id == null)
                {
                    var c = new Category
                    {
                        CategoryID = item.Id,
                        Movies = new List<Movie>(),
                        Name = item.Name,
                        Type = item.Type,
                        Url = item.Url
                    };
                    db.Categories.Add(c);
                    c.Movies.Add(movie);
                    movie.Categories.Add(c);
                    db.SaveChanges();
                }
                else
	            {
                    var c = db.Categories.Find(id);
                    c.Movies.Add(movie);
                    movie.Categories.Add(c);
                    db.SaveChanges();
	            }
            }

            #endregion

            # region studio

            // check if studio exists
            foreach (var item in tmdbMovie.Studios)
            {
                // try to find the studio
                var id = db.Studios.Find(item.Id);

                // if null, add new studio
                if (id == null)
                {
                    var s = new Studio
                    {
                        Movies = new List<Movie>(),
                        Name = item.Name,
                        StudioID = item.Id,
                        Url = item.Url
                    };

                    db.Studios.Add(s);
                    s.Movies.Add(movie);
                    movie.Studios.Add(s);

                    db.SaveChanges();

                }
                // if the studio exists already, set the relationships
                else
	            {
                    id.Movies.Add(movie);
                    movie.Studios.Add(id);

                    db.SaveChanges();
	            }
            }

            #endregion

            #region country

            // check if country exists, if not add it
            foreach (var item in tmdbMovie.Countries)
            {
                // try to find the country in the db
                var c = db.Countries.Find(item.Code);

                // if not found add it
                if (c == null)
                {
                    // create new Country object
                    c = new Country
                    {
                        Code = item.Code,
                        Movies = new List<Movie>(),
                        Name = item.Name,
                        Url = item.Url
                    };

                    // add new object to db
                    db.Countries.Add(c);

                    // set relationships
                    c.Movies.Add(movie);
                    movie.Countries.Add(c);

                    // save changes
                    db.SaveChanges();
                }
                // if found, set relationships
                else
	            {
                    // set relationships
                    c.Movies.Add(movie);
                    movie.Countries.Add(c);

                    // save changes
                    db.SaveChanges();
	            }
            }

            #endregion

            #region image

            // check if images exist, if not add them
            foreach (var item in tmdbMovie.Posters)
            {
                // try to find the image
                var i = db.Images.Find(item.ImageInfo.Id);

                // if image not found, create it
                if (i == null)
                {
                    i = new Image
                    {
                        ImageID = i.ImageID,
                        Size = i.Size,
                        Type = item.ImageInfo.Size,
                        Url = item.ImageInfo.Url
                    };

                    // add image to db
                    db.Images.Add(i);

                    // set relationships
                    movie.Images.Add(i);

                    // save changes
                    db.SaveChanges();
                }
                // if image is found (shouldn't happen), set relationships
                else
                {
                    // add image to movie
                    movie.Images.Add(i);

                    // save changes
                    db.SaveChanges();
                }
            }

            #endregion

            #region dvd

            for (int i = 0; i < dvdi; i++)
            {
                dvds.Add(new DVD
                {
                    State = "new"
                });
            }
            dvds.ForEach(s => db.DVDs.Add(s));
            db.SaveChanges();

            #endregion
            
            return movie;
        }

    }
}