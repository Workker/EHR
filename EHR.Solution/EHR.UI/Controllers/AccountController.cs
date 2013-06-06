using System.Collections.Generic;
using AutoMapper;
using EHR.Controller;
using EHR.Domain.Entities;
using EHR.UI.Models;
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
            FactoryController.GetController(ControllerEnum.Account).Register(account.FirstName, account.LastName,
                                                                             (GenderEnum) account.Gender, account.CRM,
                                                                             account.Email, account.Password,
                                                                             account.Birthday, account.Hospitals);
            return RedirectToAction("Index");
        }

        public ActionResult Login(AccountModel loginData)
        {
            var accountObject = FactoryController.GetController(ControllerEnum.Account).Login(loginData.Email,
                                                                                              loginData.Password);

            var account = MapAccountModelFrom(accountObject);

            Session["hospitals"] = MapHospitalModelFrom(accountObject);
            Session["account"] = account;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session.Abandon();

            return RedirectToAction("Index", "Account");
        }

    #region Private Methods

        private static List<HospitalModel> MapHospitalModelFrom(Account accountObject)
        {
            Mapper.CreateMap<Hospital, HospitalModel>();

            var hospitalModels = new List<HospitalModel>();

            foreach (var hospital in accountObject.Hospitals)
            {
                hospitalModels.Add(Mapper.Map<Hospital, HospitalModel>(hospital));
            }

            return hospitalModels;
        }

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