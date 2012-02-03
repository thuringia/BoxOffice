using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAP_Assignment.Models
{
    public class Movie
    {
        public bool adult { get; set; }
        public bool translated { get; set; }
        public string language { get; set; }
        public string original_name { get; set; }
        public string name { get; set; }
        public string alternative_name { get; set; }
        public string type { get; set; }
        public int id { get; set; }
        public string imdb_id { get; set; }
        public string url { get; set; }
        public string overview { get; set; }
        public int votes { get; set; }
        public float rating { get; set; }
        public bool rating_by_moviedb { get; set; }
        public bool language { get; set; }
        public string tagline { get; set; }
        public string certification { get; set; }
        public DateTime released { get; set; }
        public string homepage { get; set; }
        public string trailer { get; set; }
        public List<Category> categories { get; set; }
        public List<Studio> studios { get; set; }
        public List<Country> countries { get; set; }
        public List<Image> images { get; set; }
        public List<Person> cast { get; set; }
    }
}