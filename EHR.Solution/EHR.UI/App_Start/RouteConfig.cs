using System.Web.Mvc;
using System.Web.Routing;

namespace EHR.UI.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("DischargeSummaryDefault", "{DischargeSummary}/{cpf}",
                            new { controller = "DischargeSummary", action = "Index" }, new { cpf = @"\d+" });

            routes.MapRoute("DischargeSummaryTreatment", "{DischargeSummary}/{cpf}/{treatment}",
                            new { controller = "DischargeSummary", action = "Index" }, new { cpf = @"\d+", treatment = @"\d+" });

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                            new { controller = "Account", action = "Index", id = UrlParameter.Optional });
        }
    }
}