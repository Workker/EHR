using EHR.Controller;
using EHR.Domain.Entities;
using EHR.UI.Filters;
using EHR.UI.Infrastructure.Notification;
using EHR.UI.Models;
using EHR.UI.Models.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EHR.UI.Controllers
{
    [AuthenticationFilter]
    public class HomeController : System.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            ViewBag.Hospitals = Session["hospitals"];
            return View(Session["account"]);
        }

        #region Partial Views

        public ActionResult AccountApprovedList()
        {
            try
            {
                if (((AccountModel)Session["account"]).Administrator)
                {
                    ViewBag.Accounts = GetAccountsSkip(false);
                    return PartialView("_AccountApprovedList");
                }
                return null;
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);
                return null;
            }
        }

        public ActionResult LastSumariesList()
        {
            try
            {
                ViewBag.Summaries = GetSummariesSkip(false);
                return PartialView("_LastSumariesList");

            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);
                return null;
            }
        }

        #endregion

        #region Ajax Methods

        public ActionResult MoreSumaries()
        {
            try
            {
                ViewBag.Summaries = GetSummariesSkip(true);
                return PartialView("_LastSumariesResult");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);
                return null;
            }
        }

        public ActionResult MoreAccounts()
        {
            try
            {
                ViewBag.Accounts = GetAccountsSkip(true);

                return PartialView("_AccountApprovedListResult");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);
                return null;
            }
        }

        [HttpPost]
        public void ApproveAccount(string id)
        {
            try
            {
                FactoryController.GetController(ControllerEnum.Account).ApproveAccount(int.Parse(id));

                this.ShowMessage(MessageTypeEnum.Success, "Usuário aprovado.");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);
            }
        }

        [HttpPost]
        public void RefuseAccount(string id)
        {
            try
            {
                FactoryController.GetController(ControllerEnum.Account).RefuseAccount(int.Parse(id));

                this.ShowMessage(MessageTypeEnum.Warning, "Usuário reprovado.");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);
            }
        }

        [HttpPost]
        public void AlterPasswordOfAccount(string newPassword)
        {
            try
            {
                var account = (AccountModel)Session["account"];

                FactoryController.GetController(ControllerEnum.Account).AlterPasswordOfAccount(account.Id, newPassword);

                this.ShowMessage(MessageTypeEnum.Success, "Senha alterada.");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);
            }
        }

        [HttpPost]
        public void AddprofessionalResgistration(short professionalResgistrationType, string professionalResgistrationNumber, short stateId)
        {
            try
            {
                var account = GetAccount();

                FactoryController.GetController(ControllerEnum.Account).AddprofessionalResgistration(account.Id, professionalResgistrationType, professionalResgistrationNumber, stateId);

                this.ShowMessage(MessageTypeEnum.Warning, "Registro profissional adicionado.");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);
            }

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

            var hospitalModel = ((IList<HospitalModel>)Session["hospitals"]).FirstOrDefault();

            return skip ? accountController.GetAllNotApproved(hospitalModel.Id).Skip((int)Session["Skip"]).Take(10) : accountController.GetAllNotApproved(hospitalModel.Id).Take(10);
        }

        #endregion

        #region Summary

        private IEnumerable<SummaryModel> GetSummariesSkip(bool skip)
        {
            ManagerSession(skip);
            var account = (AccountModel)Session["account"];
            return GetSummaries(account, skip);
        }

        private IEnumerable<SummaryModel> GetSummaries(AccountModel account, bool skip)
        {
            var accountController = FactoryController.GetController(ControllerEnum.Account);

            return skip ? SummaryMapper.MapSummaryModelFrom(accountController.GetLastSumariesRealizedby(account.Id).Skip((int)Session["Skip"]).Take(10).ToList()) : SummaryMapper.MapSummaryModelFrom(accountController.GetLastSumariesRealizedby(account.Id).Take(10).ToList());
        }

        #endregion

        private void ManagerSession(bool skip)
        {
            if (skip)
                Session["Skip"] = 10 + (Session["Skip"] != null ? (int)Session["Skip"] : 0);
            else
                Session["Skip"] = 0;
        }

        private AccountModel GetAccount()
        {
            return (AccountModel)Session["account"];
        }

        #endregion
    }
}