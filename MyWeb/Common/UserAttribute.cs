using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWeb.Common
{
    public class UserAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var t = ConfigurationManager.AppSettings["test"];
            if (t=="false")
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["Gaokao"];
                if (cookie == null)
                {
                    filterContext.HttpContext.Response.Redirect(ConfigurationManager.AppSettings["out"]);

                }
            }
            
        }
    }
}