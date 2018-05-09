using Common;
using MyWeb.Common;
using System.Web;
using System.Web.Mvc;

namespace MyWeb
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new UserAttribute());
            filters.Add(new MyExceptionFileAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
