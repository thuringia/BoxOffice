using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoxOffice.Models;

namespace BoxOffice.Controllers
{
    public class HomeController : Controller
    {
        private BoxOfficeContext db = new BoxOfficeContext();

        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View(db.Movies.ToList());
        }

    }
}
