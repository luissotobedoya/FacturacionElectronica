using Enovel.Canacol.FacturacionElectronica.Controllers;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Enovel.Canacol.FacturacionElectronica
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            HttpException httpException = exception as HttpException;
            RouteData data = new RouteData();
            data.Values.Add("controller", "Error");
            if (httpException == null)
            {
                data.Values.Add("action", "Index");
            }
            else
            {
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        data.Values.Add("action", "http404");
                        break;
                    case 405:
                        data.Values.Add("action", "http405");
                        break;
                    default:
                        data.Values.Add("action", "general");
                        break;
                }
            }
            Server.ClearError();
            Response.TrySkipIisCustomErrors = true;
            IController error = new ErrorController();
            error.Execute(new RequestContext(new HttpContextWrapper(Context), data));
        }
    }
}
