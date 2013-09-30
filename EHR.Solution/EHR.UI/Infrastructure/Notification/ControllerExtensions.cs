using EHR.Controller;
using EHR.Domain.Entities;
using EHR.UI.Models;
using System;

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

        public static void RegisterActionOfUser(this System.Web.Mvc.Controller controller, HistoricalActionTypeEnum actionType, string description = "")
        {
            var summaryId = ((SummaryModel)controller.Session["Summary"]).Id;
            var accountId = ((AccountModel)controller.Session["Account"]).Id;

            FactoryController.GetController(ControllerEnum.Summary).AddToHistorical(summaryId, accountId, actionType, DateTime.Now, description);
        }
    }
}
