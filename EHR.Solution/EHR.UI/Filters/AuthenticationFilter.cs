using System.Web.Mvc;
using System.Web.Routing;

namespace EHR.UI.Filters
{
    public class AuthenticationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;
            if (session == null || session["account"] != null) return;
            var redirectTarget = new RouteValueDictionary { { "action", "Index" }, { "controller", "Account" } };
            filterContext.Result = new RedirectToRouteResult(redirectTarget);
        }
    }
}