using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvpRestApiLib.NogginBox.MvcExtras.Providers;
using System.Data.Entity;
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

            routes.MapRoute(
                "SingleUser",
                "Users/{UserID}",
                new { controller = "Users", action = "SingleUser" });

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            Database.SetInitializer<BoxOfficeContext>(new Bootstrap());

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            // Source: http://haacked.com/archive/2010/04/15/
            // sending-json-to-an-asp-net-mvc-action-method-argument.aspx
            // This must be added to accept JSON as request
            ValueProviderFactories.Factories.Add(new JsonValueProviderFactory());
            ValueProviderFactories.Factories.Add(new XmlValueProviderFactory());
        }
    }
}