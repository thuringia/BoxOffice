using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Objects;
using System.Globalization;

namespace BoxOffice.Models
{
    /// <summary>
    /// Creates the database schema and seeds some initial data
    /// </summary>
    public class Bootstrap : DropCreateDatabaseIfModelChanges<BoxOfficeContext>
    {
        protected override void  Seed(BoxOfficeContext context)
        {
            var movie = SeedMovie(context);

            SeedCastMember(context, movie);

            SeedCategories(context, movie);

            SeedStudio(context, movie);

            SeedCountry(context, movie);

            SeedImage(context, movie);

            var users = SeedUsers(context);

            SeedComments(context, movie, users);

            var dvds = SeedDVDs(context, movie);
            
            SeedRentals(context, users, dvds.ToList(), movie);

            SeedMessages(context, users);

            SeedRatings(context, movie, users);

            base.Seed(context);
        }

        /// <summary>
        /// Seeds ratings
        /// </summary>
        /// <param name="context">the database context the be seeded into</param>
        /// <param name="movie">the movie the ratings belong to</param>
        /// <param name="users">the users rating the movie</param>
        private static void SeedRatings(BoxOfficeContext context, Movie movie, List<User> users)
        {
            var rating = new Rating
            {
                MovieID = 187,
                UserID = 1,
                DateRented = DateTime.Now,
                Movie = movie,
                User = users[0]
            };
            context.Ratings.Add(rating);

            users[0].Ratings.Add(rating);
            movie.Ratings.Add(rating);
            context.SaveChanges();
        }

        /// <summary>
        /// seeds messages
        /// </summary>
        /// <param name="context">the database context the be seeded into</param>
        /// <param name="users">the user's participating</param>
        private static void SeedMessages(BoxOfficeContext context, List<User> users)
        {
            var msg = new Message
            {
                MessageID = 1,
                FromUserID = 1,
                ToUserID = 2,
                DateSent = DateTime.Now,
                Text = "You suck!",
                toAll = false,
                Users = new List<User>()
            };
            context.Messages.Add(msg);

            users.ForEach(s => s.Messages.Add(msg));
            users.ForEach(s => msg.Users.Add(s));
            context.SaveChanges();
        }

        /// <summary>
        /// seeds rentals
        /// </summary>
        /// <param name="context">the database context the be seeded into</param>
        /// <param name="users">the users renting</param>
        /// <param name="dvds">the dvds to be rented</param>
        private static void SeedRentals(BoxOfficeContext context, List<User> users, List<DVD> dvds, Movie movie)
        {
            var rental = new Rental
            {
                DvdID = dvds.First().DvdID,
                UserID = users.First().UserID,
                DateOfRental = DateTime.Now,
                DateDue = DateTime.Today.AddDays(7),
                DateReturned = null,
                DateSent = null,
                Dvd = dvds.First(),
                User = users.First(),
                Movie = movie,
                MovieID = movie.MovieID
            };
            context.Rentals.Add(rental);

            users[0].Queue.Add(rental);
            dvds[0].Rentals.Add(rental);
            dvds[0].State = "rented";
            movie.Rentals.Add(rental);

            context.SaveChanges();
        }

        /// <summary>
        /// seeds dvds
        /// </summary>
        /// <param name="context">the database context the be seeded into</param>
        /// <param name="movie">the movie the dvds belong to</param>
        /// <returns></returns>
        private static List<DVD> SeedDVDs(BoxOfficeContext context, Movie movie)
        {
            var dvds = new List<DVD> { };
            for (int i = 1; i < 6; i++)
            {
                var dvd = new DVD
                {
                    MovieID = 187,
                    State = "new",
                    Movie = context.Movies.Find(187),
                    Rentals = new List<Rental>()
                };
                dvds.Add(dvd);
            }
            dvds.ForEach(s => context.DVDs.Add(s));

            dvds.ForEach(s => movie.DVDs.Add(s));
            context.SaveChanges();
            return dvds;
        }

        /// <summary>
        /// seeds comments
        /// </summary>
        /// <param name="context">the database context the be seeded into</param>
        /// <param name="movie">the movie the comments belong to</param>
        /// <param name="users">the users commenting on the movie</param>
        private static void SeedComments(BoxOfficeContext context, Movie movie, List<User> users)
        {
            var comment = new Comment
            {
                MovieID = 187,
                UserID = 1,
                Message = "Awesome movie!!!",
                Date = DateTime.Now,
                Movie = context.Movies.Find(187),
                User = context.Users.Find(1)
            };
            context.Comments.Add(comment);

            users[0].Comments.Add(comment);
            movie.Comments.Add(comment);
            context.SaveChanges();
        }

        /// <summary>
        /// seeds users
        /// </summary>
        /// <param name="context">the database context the be seeded into</param>
        /// <returns></returns>
        private static List<User> SeedUsers(BoxOfficeContext context)
        {
            var users = new List<User>
            {
                new User 
                {
                    Username = "foo",
                    Email = "foo@bar.com",
                    DateOfBirth = DateTime.ParseExact("1988-02-13", @"yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Street = "Foo",
                    Number = "1",
                    Zip = "12345",
                    City = "Foo",
                    Comments = new List<Comment>(),
                    Messages = new List<Message>(),
                    Ratings = new List<Rating>()                    
                },
                new User
                {
                    Username = "bar",
                    Email = "bar@foo.com",
                    DateOfBirth = DateTime.ParseExact("1988-02-13", @"yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Street = "Foo",
                    Number = "1",
                    Zip = "12345",
                    City = "Foo",
                    Comments = new List<Comment>(),
                    Messages = new List<Message>(),
                    Ratings = new List<Rating>() 
                }
            };
            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();
            return users;
        }

        /// <summary>
        /// seeds images
        /// </summary>
        /// <param name="context">the database context the be seeded into</param>
        /// <param name="movie">the movie the images belong to</param>
        private static void SeedImage(BoxOfficeContext context, Movie movie)
        {
            var image = new Image
            {
                Type = "poster",
                Url = "http://cf2.imgobject.com/t/p/w185/eCJkepVJslq1nEtqURLaC1zLPAL.jpg",
                Size = "original",
                ImageID = "4bc904e9017a3c57fe00168c"
            };
            context.Images.Add(image);

            movie.Images.Add(image);
            context.SaveChanges();
        }

        /// <summary>
        /// seeds countries
        /// </summary>
        /// <param name="context">the database context the be seeded into</param>
        /// <param name="movie">the movie the images belong to</param>
        private static void SeedCountry(BoxOfficeContext context, Movie movie)
        {
            var country = new Country
            {
                Name = "United States of America",
                Code = "US",
                Url = "http://www.themoviedb.org/country/us",
                Movies = new List<Movie>()
            };
            context.Countries.Add(country);

            movie.Countries.Add(country);
            country.Movies.Add(movie);
            context.SaveChanges();
        }

        /// <summary>
        /// seeds studios
        /// </summary>
        /// <param name="context">the database context the be seeded into</param>
        /// <param name="movie">the movie the studios belong to</param>
        private static void SeedStudio(BoxOfficeContext context, Movie movie)
        {
            var studio = new Studio
            {
                Name = "Miramax Films",
                Url = "http://www.themoviedb.org/company/20",
                StudioID = 20,
                Movies = new List<Movie>()
            };
            context.Studios.Add(studio);

            movie.Studios.Add(studio);
            studio.Movies.Add(movie);
            context.SaveChanges();
        }

        /// <summary>
        /// seeds categories
        /// </summary>
        /// <param name="context">the database context the be seeded into</param>
        /// <param name="movie">the movie the categories belong to</param>
        private static void SeedCategories(BoxOfficeContext context, Movie movie)
        {
            var categories = new List<Category>();

            categories.Add(new Category
            {
                CategoryID = 80,
                Name = "Crime",
                Url = "http://themoviedb.org/genre/crime",
                Type = "genre",
                Movies = new List<Movie>()
            });
            categories.Add(new Category
            {
                CategoryID = 18,
                Type = "genre",
                Url = "http://themoviedb.org/genre/drama",
                Name = "Drama",
                Movies = new List<Movie>()
            });
            categories.Add(new Category
            {
                CategoryID = 53,
                Name = "Thriller",
                Url = "http://themoviedb.org/genre/thriller",
                Type = "genre",
                Movies = new List<Movie>()
            });

            categories.ForEach(s => context.Categories.Add(s));
            context.SaveChanges();

            categories.ForEach(s => s.Movies.Add(movie));
            categories.ForEach(s => movie.Categories.Add(s));
            context.SaveChanges();
        }

        /// <summary>
        /// seeds cast members
        /// </summary>
        /// <param name="context">the database context the be seeded into</param>
        /// <param name="movie">the movie the cast members belong to</param>
        private static void SeedCastMember(BoxOfficeContext context, Movie movie)
        {
            var castMember = new CastMember
            {
                Job = "Director",
                CastMemberID = 2293,
                Name = "Frank Miller",
                Department = "Directing",
                Url = "http://www.themoviedb.org/person/2293",
                Order = 0,
                Cast_id = 1,
                Movie = new Movie()
            };

            context.CastMembers.Add(castMember);

            castMember.MovieID = movie.MovieID;
            castMember.Movie = movie;

            movie.Cast.Add(castMember);
            context.SaveChanges();
        }

        /// <summary>
        /// Seeds a movie object
        /// </summary>
        /// <param name="context">the database context the be seeded into</param>
        /// <returns></returns>
        private static Movie SeedMovie(BoxOfficeContext context)
        {
            var movie = new Movie
            {
                Price = 9.99M,
                DateAdded = DateTime.Now,

                Cast = new List<CastMember>(),
                Adult = false,
                Translated = true,
                Language = "en",
                Original_name = "Sin City",
                Name = "Sin City",
                Type = "movie",
                MovieID = 187,
                Imdb_id = "tt0401792",
                Url = "http://www.themoviedb.org/movie/187",
                Overview = "The first of three films based on the beautiful comic world of Frank Miller. Sin City depicts the comic book almost page for page with its heavy contrast black and white extremely violent and graphic imagery. Sin City is set in a dangerous and dark Basin City where corruption, sex and violence is everyday life.",
                Votes_by_moviedb = 23,
                Rating_by_moviedb = 6.8f,
                Tagline = "There is no justice without sin.",
                Certification = "R",
                DateReleased = DateTime.ParseExact("2005-04-04", @"yyyy-MM-dd", CultureInfo.InvariantCulture),
                Homepage = "http://www.sincitythemovie.com/",
                Trailer = "http://www.youtube.com/watch?v=80",
                Categories = new List<Category>(),
                Comments = new List<Comment>(),
                Countries = new List<Country>(),
                DVDs = new List<DVD>(),
                Images = new List<Image>(),
                Ratings = new List<Rating>(),
                Studios = new List<Studio>(),
                Rentals = new List<Rental>()
            };
            context.Movies.Add(movie);
            context.SaveChanges();
            return movie;
        }
    }
}