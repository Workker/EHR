using EHR.Controller;
using EHR.Domain.Entities;
using EHR.UI.Filters;
using EHR.UI.Infrastructure.Notification;
using EHR.UI.Mappers;
using EHR.UI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EHR.UI.Controllers
{
    [AuthenticationFilter]
    public class HomeController : System.Web.Mvc.Controller
    {
        #region Views

        public ActionResult Index()
        {
            ViewBag.Hospitals = Session["hospitals"];
            return View(Session["account"]);
        }

        #endregion

        #region Partial Views

        public ActionResult AccountApprovedList()
        {
            if (((AccountModel)Session["account"]).Administrator == true)
            {
                ViewBag.Accounts = GetAccountsSkip(false);
                return PartialView("_AccountApprovedList");
            }
            else
            {
                return null;
            }
        }

        public ActionResult LastSumariesList()
        {
            var account = (AccountModel)Session["account"];
            var sumaries = FactoryController.GetController(ControllerEnum.Account).GetSumaries(account.Id).Take(10);
            ViewBag.Summaries = new List<SummaryModel>();
            return PartialView("_LastSumariesList");
        }

        #endregion

        #region Ajax Methods

        public ActionResult MoreSumaries()
        {
            return PartialView("_LastSumariesResult", GetSummaries(true));
        }

        public ActionResult MoreAccounts()
        {
            ViewBag.Accounts = GetAccountsSkip(true);
            return PartialView("_AccountApprovedListResult");
        }

        [HttpPost]
        public void ApproveAccount(string id)
        {
            FactoryController.GetController(ControllerEnum.Account).ApproveAccount(int.Parse(id));
        }

        [HttpPost]
        public void RefuseAccount(string id)
        {
            FactoryController.GetController(ControllerEnum.Account).RefuseAccount(int.Parse(id));
        }

        [HttpPost]
        public void AlterPasswordOfAccount(string newPassword)
        {
            var account = (AccountModel)Session["account"];
            FactoryController.GetController(ControllerEnum.Account).AlterPasswordOfAccount(account.Id, newPassword);
        }

        public void ChangeCurrentHospital(string q)
        {
            var account = (AccountModel)Session["account"];
            account.CurrentHospital = short.Parse(q);
            Session["account"] = account;
        }

        #endregion

        #region Private Methods

        #region Account

        private IEnumerable<AccountModel> GetAccountsSkip(bool skip)
        {
            ManagerSession(skip);
            var accounts = GetAccounts(skip);
            return AccountMapper.MapAccountModelFrom(accounts);
        }

        private IEnumerable<Account> GetAccounts(bool skip)
        {
            var accountController = FactoryController.GetController(ControllerEnum.Account);
            return skip ? accountController.GetAllNotApproved().Skip((int)Session["Skip"]).Take(10) : accountController.GetAllNotApproved().Take(10);
        }

        #endregion

        #region Summary

        private IEnumerable<SummaryModel> GetSummaries(bool skip)
        {
            ManagerSession(skip);
            var account = (AccountModel)Session["account"];
            return GetSummaries(account, skip);
        }

        private IEnumerable<SummaryModel> GetSummaries(AccountModel account, bool skip)
        {
            var accountController = FactoryController.GetController(ControllerEnum.Account);

            return skip ? SummaryMapper.MapSummaryModelFrom(accountController.GetSumaries(account.Id).Skip((int)Session["Skip"]).Take(10).ToList()) : SummaryMapper.MapSummaryModelFrom(accountController.GetSumaries(account.Id).Take(10).ToList());
        }

        #endregion

        private void ManagerSession(bool skip)
        {
            if (skip)
                Session["Skip"] = 10 + (Session["Skip"] != null ? (int)Session["Skip"] : 0);
            else
                Session["Skip"] = 0;
        }

        #endregion
    }
}