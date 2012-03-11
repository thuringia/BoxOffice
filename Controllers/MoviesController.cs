using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoxOffice.Models;
using MvpRestApiLib;

namespace BoxOffice.Controllers
{ 
    public class MoviesController : Controller
    {
        private BoxOfficeContext db = new BoxOfficeContext();

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
        public ActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(movie);
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
    }
}