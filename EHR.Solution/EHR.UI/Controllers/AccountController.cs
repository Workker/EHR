using EHR.Controller;
using EHR.UI.Infrastructure.Notification;
using EHR.UI.Models;
using EHR.UI.Models.Mappers;
using System;
using System.Linq;
using System.Web.Mvc;

namespace EHR.UI.Controllers
{
    public class AccountController : System.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            try
            {
                ViewBag.Hospitals = FactoryController.GetController(ControllerEnum.Hospital).GetAllHospitals();

                return View();
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message, true);

                return View("Error");
            }
        }

        public ActionResult Register(AccountModel account)
        {
            try
            {
                var professionalRegistration = account.ProfessionalRegistration.FirstOrDefault();

                FactoryController.GetController(ControllerEnum.Account).Register(account.FirstName, account.LastName,
                                                                             account.Gender, professionalRegistration.Type, professionalRegistration.Number,
                                                                             account.Email, account.Password,
                                                                             account.Birthday, account.Hospital);

                this.ShowMessage(MessageTypeEnum.Success, "Conta de usuário criada. Aguarde aprovação do administrador.", true);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                this.ShowMessage(MessageTypeEnum.Error, ex.Message, true);

                return RedirectToAction("Index");
            }

        }

        public ActionResult Login(AccountModel loginData)
        {
            try
            {
                var accountObject = FactoryController.GetController(ControllerEnum.Account).Login(loginData.Email,
                                                                                                  loginData.Password);

                var account = AccountMapper.MapAccountModelFrom(accountObject);
                account.CurrentHospital = account.Hospital;

                Session["hospital"] = HospitalMapper.MapHospitalModelFrom(accountObject);
                Session["account"] = account;

                Session["States"] = FactoryController.GetController(ControllerEnum.State).GetAll();

                return RedirectToAction("Index", "Home");

            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message, true);

                return RedirectToAction("Index");
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();

            return RedirectToAction("Index", "Account");
        }
    }
}