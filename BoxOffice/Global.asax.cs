﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BoxOffice.Models;

namespace BoxOffice
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "MovieSearch",
                "Movies/Search/{searchTerm}",
                new
                {
                    controller = "Movies",
                    action = "Search",
                    searchTerm = ""
                }
            );
            routes.MapRoute(
                "MovieAjaxSearch",
                "Movies/ajaxSearch/",
                new { controller = "Movies", action = "ajaxSearch" }
            );

            routes.MapRoute(
                "AdminSearch",
                "Admin/Search/{searchTerm}",
                new
                {
                    controller = "Admin",
                    action = "Search",
                    searchTerm = ""
                }
            );
            routes.MapRoute(
                "AdminAjaxSearch",
                "Admin/ajaxSearch/",
                new { controller = "Admin", action = "ajaxSearch" }
            );
        }

        protected void Application_Start()
        {
            Database.SetInitializer<BoxOfficeContext>(new Bootstrap());

            AreaRegistration.RegisterAllAreas();

            // Use LocalDB for Entity Framework by default
            Database.DefaultConnectionFactory = new SqlConnectionFactory("Data Source=(localdb)\v11.0; Integrated Security=True; MultipleActiveResultSets=True");

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            BundleTable.Bundles.RegisterTemplateBundles();
        }
    }
}