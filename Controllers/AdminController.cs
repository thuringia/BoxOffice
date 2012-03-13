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

        private TheMovieDb.TmdbApi tmdb = new TmdbApi("b0f4c9d847ceda92061d4090b470dc10");

        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Admin/Movies

        public ActionResult Movies()
        {
            return View(db.Movies.ToList());
        }

        //
        // PUT: /Admin/AddMovie

        public ActionResult AddMovie(AddMovieModel add)
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

            # region Cast

            // Check if the persons exist already
            foreach (var item in tmdbMovie.Cast)
            {
                var p = db.Persons.Find(item.Id);

                if (p == null)
                {
                    persons.Add(new Person
                    {
                        Name = item.Name,
                        PersonID = item.Id
                    });
                }
            }
            persons.ForEach(s => db.Persons.Add(s));
            db.SaveChanges();

            // Add Cast
            foreach (var item in tmdbMovie.Cast)
            {
                cast.Add(new CastMember
                {
                    Job = item.Job,
                    PersonID = item.Id,
                    Person = db.Persons.Find(item.Id),
                    CastMemberID = (db.CastMembers.Last().CastMemberID) + 1,
                    Cast_id = item.CastId,
                    Character = item.Character,
                    Department = item.Department,
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
                    categories.Add(new Category { 
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
                    DvdID = db.DVDs.Last().DvdID + 1,
                    State = "new"
                });
            }
            dvds.ForEach(s => db.DVDs.Add(s));
            db.SaveChanges();

            #endregion

            #region movie

            movie.Adult = tmdbMovie.Adult;
            movie.Alternative_name = tmdbMovie.AlternativeName;
            cast.ForEach(s =>  movie.Cast.Add(s));
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
