using AutoMapper;
using EHR.Controller;
using EHR.Domain.Entities;
using EHR.UI.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EHR.UI.Controllers
{
    public class AccountController : System.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register(AccountModel account)
        {
            FactoryController.GetController(ControllerEnum.Account).Register(account.FirstName, account.LastName, (GenderEnum)account.Gender, account.CRM, account.Email, account.Password, account.Birthday, new List<short>() { 1, 2, 3 });
            return RedirectToAction("Index");
        }

        public ActionResult Login(AccountModel loginData)
        {
            var accountObject = FactoryController.GetController(ControllerEnum.Account).Login(loginData.Email, loginData.Password);
            var account = MapAccountModelFrom(accountObject);

            Session["account"] = account;

            return RedirectToAction("Index", "Home");
        }

        #region Private Methods

        private static AccountModel MapAccountModelFrom(Account accountObject)
        {
            Mapper.CreateMap<Account, AccountModel>().ForMember(dest => dest.Hospitals, source => source.Ignore());
            var account = Mapper.Map<Account, AccountModel>(accountObject);

            foreach (var hospital in accountObject.Hospitals)
            {
                account.Hospitals.Add(hospital.Id);
            }
            return account;
        }

        #endregion
    }
}
