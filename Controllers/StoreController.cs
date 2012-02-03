using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WAP_Assignment.Models;

namespace WAP_Assignment.Controllers
{
    public class StoreController : Controller
    {
        AssignmentEntities db = new AssignmentEntities();

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
