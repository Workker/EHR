namespace EHR.UI.Infrastructure.Notification
{
    public static class ControllerExtensions
    {
        public static void ShowMessage(this System.Web.Mvc.Controller controller, MessageTypeEnum messageType, string message, bool showAfterRedirect = false)
        {
            var messageTypeKey = messageType.ToString();
            if (showAfterRedirect)
            {
                controller.TempData[messageTypeKey] = message;
            }
            else
            {
                controller.ViewData[messageTypeKey] = message;
            }
        }
    }
}
