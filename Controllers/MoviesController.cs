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

            var movie = new Movie();

            #region movie

            movie.Adult = tmdbMovie.Adult;
            movie.Alternative_name = tmdbMovie.AlternativeName;
            cast.ForEach(s => movie.Cast.Add(s));
            categories.ForEach(s => movie.Categories.Add(s));
            movie.Certification = tmdbMovie.Certification;
            countries.ForEach(s => movie.Countries.Add(s));
            dvds.ForEach(s => movie.DVDs.Add(s));
            movie.Homepage = tmdbMovie.Homepage;
            images.ForEach(s => movie.Images.Add(s));
            movie.Rating_by_moviedb = float.Parse(tmdbMovie.Rating);
            movie.Released = DateTime.Parse(tmdbMovie.Released);
            studios.ForEach(s => movie.Studios.Add(s));
            movie.Tagline = tmdbMovie.Tagline;
            movie.Trailer = tmdbMovie.Trailer;
            movie.Translated = tmdbMovie.Translated;
            movie.Type = tmdbMovie.Type;
            movie.Url = tmdbMovie.Url;
            movie.Votes_by_moviedb = int.Parse(tmdbMovie.Votes);

            db.Movies.Add(movie);
            db.SaveChanges();

            #endregion

            # region Cast
            // Add Cast
            foreach (var item in tmdbMovie.Cast)
            {
                cast.Add(new CastMember
                {
                    Cast_id = item.Id,
                    Character = item.Character,
                    Department = item.Department,
                    Job = item.Job,
                    MovieID = movie.MovieID,
                    Name = item.Name,
                    Order = item.Order,
                    Url = item.Url
                });
            }
            cast.ForEach(s => db.CastMembers.Add(s));
            db.SaveChanges();

            #endregion

            #region category

            // Check if category exists already
            foreach (var item in tmdbMovie.Genres)
            {
                var id = db.Categories.Find(item.Id);
                if (id == null)
                {
                    categories.Add(new Category
                    {
                        CategoryID = item.Id,
                        Name = item.Name,
                        Type = item.Type,
                        Url = item.Url
                    });
                }
            }

            categories.ForEach(s => db.Categories.Add(s));
            db.SaveChanges();

            // Add all categories
            categories.Clear();
            tmdbMovie.Genres.ForEach(s => categories.Add(new Category
            {
                CategoryID = s.Id,
                Name = s.Name,
                Type = s.Type,
                Url = s.Url
            }));

            #endregion

            # region studio

            // check if studio exists
            foreach (var item in tmdbMovie.Studios)
            {
                var s = db.Studios.Find(item.Id);

                if (s == null)
                {
                    studios.Add(new Studio
                    {
                        Name = item.Name,
                        StudioID = item.Id,
                        Url = item.Url
                    });
                }
            }
            studios.ForEach(s => db.Studios.Add(s));
            db.SaveChanges();

            // store all studios in list
            studios.Clear();
            tmdbMovie.Studios.ForEach(s => studios.Add(new Studio
            {
                Name = s.Name,
                StudioID = s.Id,
                Url = s.Url
            }));

            #endregion

            #region country

            // check if country exists, if not add it
            foreach (var item in tmdbMovie.Countries)
            {
                var c = db.Countries.Find(item.Code);

                if (c == null)
                {
                    countries.Add(new Country
                    {
                        Code = item.Code,
                        Name = item.Name,
                        Url = item.Url
                    });
                }
            }
            countries.ForEach(s => db.Countries.Add(s));
            db.SaveChanges();

            // store all studios
            countries.Clear();
            tmdbMovie.Countries.ForEach(s => countries.Add(new Country
            {
                Code = s.Code,
                Name = s.Name,
                Url = s.Url
            }));

            #endregion

            #region image

            // check if images exist, if not add them
            foreach (var item in tmdbMovie.Posters)
            {
                var p = db.Images.Find(item.ImageInfo.Id);

                if (p == null)
                {
                    images.Add(new Image
                    {
                        ImageID = item.ImageInfo.Id,
                        Size = item.ImageInfo.Size,
                        Type = item.ImageInfo.Type,
                        Url = item.ImageInfo.Url
                    });
                }
            }

            images.ForEach(s => db.Images.Add(s));
            db.SaveChanges();

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

            #region foreign key updates

            dvds.ForEach(s => s.MovieID = movie.MovieID);
            dvds.ForEach(s => s.Movie = movie);
            db.SaveChanges();

            countries.ForEach(s => s.Movies.Add(movie));
            db.SaveChanges();

            studios.ForEach(s => s.Movies.Add(movie));
            db.SaveChanges();

            #endregion

            return movie;
        }

    }
}