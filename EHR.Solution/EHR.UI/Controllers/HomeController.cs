using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using EHR.Controller;
using EHR.Domain.Entities;
using EHR.UI.Filters;
using EHR.UI.Models;

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

        public ActionResult AccountApprovedList()
        {
            var accounts = FactoryController.GetController(ControllerEnum.Account).GetAllNotApproved();
            var accountModels = MapAccountModelFrom(accounts);
            ViewBag.Accounts = accountModels;
            return PartialView("_AccountApprovedList");
        }

        public ActionResult LastSumariesList()
        {
            //var account = (AccountModel)Session["account"];
            //var sumaries = FactoryController.GetController(ControllerEnum.Account).GetLastTenSumaries(account.Id);
            //var accountModels = MapSummaryModelFrom(sumaries);
            ViewBag.Summaries = new List<SumaryModel>();
            return PartialView("_LastSumariesList");
        }

        #region Private Methods

        private static List<AccountModel> MapAccountModelFrom(IList<Account> accounts)
        {
            Mapper.CreateMap<Account, AccountModel>().ForMember(dest => dest.Hospitals, source => source.Ignore());

            var accountModels = new List<AccountModel>();

            foreach (var item in accounts)
            {
                var account = Mapper.Map<Account, AccountModel>(item);

                foreach (var hospital in item.Hospitals)
                {
                    account.Hospitals.Add(hospital.Id);
                }
                accountModels.Add(account);
            }

            return accountModels;
        }

        //private static List<SumaryModel> MapSummaryModelFrom(IList<Summary> summaries)
        //{
        //    Mapper.CreateMap<Summary, SumaryModel>();

        //    var accountModels = new List<AccountModel>();

        //    foreach (var item in summaries)
        //    {
        //        var summary = Mapper.Map<Summary, SumaryModel>(item);

        //        foreach (var hospital in item.Hospitals)
        //        {
        //            account.Hospitals.Add(hospital.Id);
        //        }
        //        accountModels.Add(account);
        //    }

        //    return accountModels;
        //}

        #endregion
    }
}