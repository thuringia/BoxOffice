using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoxOffice.ActionFilters
{
    public class JsonRequestBehaviorAttribute : ActionFilterAttribute
    {
        private JsonRequestBehavior Behavior { get; set; }

        public JsonRequestBehaviorAttribute()
        {
            Behavior = JsonRequestBehavior.AllowGet;
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var result = filterContext.Result as JsonResult;

            if (result != null)
            {
                result.JsonRequestBehavior = Behavior;
            }
        }
    }
}