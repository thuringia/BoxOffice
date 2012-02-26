using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoxOffice.Models;

namespace BoxOffice.Controllers
{
    public class StoreController : Controller
    {
        BoxOfficeContext db = new BoxOfficeContext();

        //      
        // GET: /Store/        
        public string Index()
        {
            return "Hello from Store.Index()";
        }

        //        
        // GET: /Store/Browse        
        public string Browse()
        {
            return "Hello from Store.Browse()";
        }

        //        
        // GET: /Store/Details        
        public ActionResult Details(int id)
        {
            return View();
        }

    }
}
