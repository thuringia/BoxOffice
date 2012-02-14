using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WAP_Assignment.Models
{
    public class AssignmentEntities : DbContext
    {
        public DbSet<Adress> Adresses { get; set; }
        
        public DbSet<Category> Categories { get; set; }

        public DbSet<Comment> Comments { get; set; }
        
        public DbSet<Country> Countries { get; set; }

        public DbSet<DVD> DVDs { get; set; }
        
        public DbSet<Image> Images { get; set; }

        public DbSet<Message> Messages { get; set; }
        
        public DbSet<Movie> Movies { get; set; }
        
        public DbSet<Person> Persons { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Rental> Rentals { get; set; }
        
        public DbSet<Studio> Studios { get; set; }

        public DbSet<User> Users { get; set; }
        
    }
}