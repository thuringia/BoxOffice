﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BoxOffice.Models;

namespace BoxOffice.Bootstrap
{
    public class Bootstrap : DropCreateDatabaseIfModelChanges<BoxOfficeContext>
    {
        protected override void Seed(BoxOfficeContext context)
        {
            var movie = new Movie
            {
                Price = 9.99M,
                
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
                Released = DateTime.Parse("2005-04-04"),
                Homepage = "http://www.sincitythemovie.com/",
                Trailer = "http://www.youtube.com/watch?v=80"
            };
            context.Movies.Add(movie);
            context.SaveChanges();

            var rating = new Rating
            {
                RatingID = 0,
                MovieID = 187,
                UserID = 0,
                Date = DateTime.Now
            };
            context.Ratings.Add(rating);
            context.SaveChanges();

            var categories = new List<Category> {
                new Category { CategoryID = 80, Name = "Crime", Url = "http://themoviedb.org/genre/crime", Type = "genre", MovieID = 187 },
                new Category { CategoryID = 18, MovieID = 187, Type = "genre", Url = "http://themoviedb.org/genre/drama", Name = "Drama" },
                new Category { CategoryID = 53, Name = "Thriller", Url = "http://themoviedb.org/genre/thriller", Type = "genre", MovieID=187 }  
            };
            categories.ForEach(s => context.Categories.Add(s));
            context.SaveChanges();

            var studio = new Studio
            {
                Name = "Miramax Films",
                Url = "http://www.themoviedb.org/company/20",
                StudioID = 20
            };
            context.Studios.Add(studio);
            context.SaveChanges();

            var country = new Country
            {
                Name = "United States of America",
                Code = "US",
                Url = "http://www.themoviedb.org/country/us"
            };
            context.Countries.Add(country);
            context.SaveChanges();

            var image = new Image
            {
                Type = "poster",
                Url = "http://hwcdn.themoviedb.org/posters/68c/4bc904e9017a3c57fe00168c/sin-city-original.jpg",
                Size = "original",
                ImageID = "4bc904e9017a3c57fe00168c",
                MovieID = 178
            };
            context.Images.Add(image);
            context.SaveChanges();

            var person = new Person
            {
                Name = "Frank Miller",
                Job = "Director",
                PersonID = 2293,
                Department = "Directing",
                Url = "http://www.themoviedb.org/person/2293",
                Order = 0,
                Cast_id = 1
            };
            context.Persons.Add(person);
            context.SaveChanges();

            var users = new List<User>
            {
                new User 
                {
                    UserID = 0,
                    Username = "foo",
                    Email = "foo@bar.com",
                    DateOfBirth = DateTime.Parse("1988-02-13"),
                    Password = "foobar",
                    AdressID = 0,
                    isAdmin = false
                },
                new User
                {
                    UserID = 1,
                    Username = "bar",
                    Email = "bar@foo.com",
                    DateOfBirth = DateTime.Parse("1988-02-13"),
                    Password = "foobar",
                    AdressID = 1,
                    isAdmin = false
                }
            };
            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();

            var adresses = new List<Adress>
            {
                new Adress
                {
                    UserID = 0,
                    AdressID = 0,
                    Street = "Foo Street",
                    Number = "1",
                    Zip = "12345",
                    City = "Foocity"
                },
                new Adress
                {
                    UserID = 1,
                    AdressID = 1,
                    Street = "Bar Road",
                    Number = "1",
                    Zip = "54321",
                    City = "Bar Town"
                }
            };
            adresses.ForEach(s => context.Adresses.Add(s));
            context.SaveChanges();

            var comment = new Comment
            {
                CommentID = 0,
                MovieID = 187,
                UserID = 0,
                Message = "Awesome movie!!!",
                Date = DateTime.Now
            };
            context.Comments.Add(comment);
            context.SaveChanges();

            var dvds = new List<DVD>{};
            for (int i = 0; i < 5; i++)
            {
                var dvd = new DVD
                {
                    DvdID = i,
                    MovieID = 187,
                    State = "new"
                };
                dvds.Add(dvd);
            }
            dvds.ForEach(s => context.DVDs.Add(s));
            context.SaveChanges();

            var rental = new Rental
            {
                RentalID = 0,
                DvdID = 0,
                UserID = 0,
                DateOfRental = DateTime.Now,
                DueDate = DateTime.Now
            };
            context.Rentals.Add(rental);
            context.SaveChanges();

            var msg = new Message
            {
                MessageID = 0,
                FromUserID = 0,
                ToUserID = 1,
                Date = DateTime.Now,
                Text = "You suck!",
                toAll = false
            };
            context.Messages.Add(msg);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}