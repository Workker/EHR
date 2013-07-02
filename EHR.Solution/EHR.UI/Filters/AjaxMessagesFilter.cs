using EHR.UI.Infrastructure.Notification;
using System;
using System.Web.Mvc;

namespace EHR.UI.Filters
{
    public class AjaxMessagesFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var viewData = filterContext.Controller.ViewData;
                var response = filterContext.HttpContext.Response;

                foreach (var messageType in Enum.GetNames(typeof(MessageTypeEnum)))
                {
                    var message = viewData.ContainsKey(messageType)
                                    ? viewData[messageType]
                                    : null;
                    if (message != null) // We store only one message in the http header. First message that comes wins.
                    {
                        response.AddHeader("X-Message-Type", messageType);
                        response.AddHeader("X-Message", message.ToString());
                        return;
                    }
                }
            }
        }
    }
}