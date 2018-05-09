using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Common
{
    public class MyExceptionFileAttribute: System.Web.Mvc.HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            new DBHelper().Log(filterContext.Exception.ToString());
            //Common.LogHelp.WriteLog(filterContext.Exception.ToString());
            filterContext.HttpContext.Response.Redirect("/Shared/Error");
        }
    }
}