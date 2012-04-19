using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoxOffice.Models;

namespace BoxOffice.Controllers
{
    public class GenresController : Controller
    {
        private BoxOfficeContext db = new BoxOfficeContext();

        //
        // GET: /Genres/

        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        //
        // GET: /Genres/Details/5

        public ActionResult Details(int id = 0)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}