using EHR.Controller;
using EHR.UI.Infrastructure.Notification;
using EHR.UI.Mappers;
using EHR.UI.Models;
using System;
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
            try
            {
                FactoryController.GetController(ControllerEnum.Account).Register(account.FirstName, account.LastName,
                                                                             account.Gender, account.CRM,
                                                                             account.Email, account.Password,
                                                                             account.Birthday, account.Hospitals);

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
                account.CurrentHospital = account.Hospitals[0];

                Session["hospitals"] = HospitalMapper.MapHospitalModelFrom(accountObject);
                Session["account"] = account;

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