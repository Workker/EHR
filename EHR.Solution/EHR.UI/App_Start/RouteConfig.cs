using System.Web.Mvc;
using System.Web.Routing;

namespace EHR.UI.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Patient", "{Patient}/{cpf}",
                            new { controller = "Patient", action = "Index" }, new { cpf = @"\d+" });

            routes.MapRoute("Treatment", "{Patient}/{cpf}/{treatment}",
                            new { controller = "Patient", action = "Index" }, new { cpf = @"\d+", treatment = @"\d+" });

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                            new { controller = "Account", action = "Index", id = UrlParameter.Optional });
        }
    }
}