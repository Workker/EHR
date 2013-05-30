using System;
using AutoMapper;
using EHR.Controller;
using EHR.Domain.Entities;
using EHR.UI.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using Workker.Framework.Domain;

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
            Assertion.IsFalse(string.IsNullOrEmpty(account.FirstName), "Primeiro nome não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(account.LastName), "Ultimo nome não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(account.CRM), "CRM não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(account.Email), "E-mail não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(account.Password), "Senha não informada.").Validate();
            Assertion.GreaterThan(account.Birthday, DateTime.MinValue, "Data de aniverssario não informada.").Validate();
            Assertion.GreaterThan(account.Gender, new short(), "Genero não informado.").Validate();

            FactoryController.GetController(ControllerEnum.Account).Register(account.FirstName, account.LastName, (GenderEnum)account.Gender, account.CRM, account.Email, account.Password, account.Birthday, new List<short>() { 1, 2, 3 });

            return RedirectToAction("Index");
        }

        public ActionResult Login(AccountModel loginData)
        {
            Assertion.IsFalse(string.IsNullOrEmpty(loginData.Email), "E-mail não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(loginData.Password), "Senha não informada.").Validate();

            var accountObject = FactoryController.GetController(ControllerEnum.Account).Login(loginData.Email, loginData.Password);
            var account = MapAccountModelFrom(accountObject);

            Session["account"] = account;

            Assertion.NotNull(Session["account"], "Falha ao tentar criar a sessão de usuário.").Validate();

            return RedirectToAction("Index", "Home");

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
    }
}
