using EHR.Domain.Entities;
using EHR.Domain.Repository;
using System;
using System.Collections.Generic;
using EHR.Domain.Util;
using Workker.Framework.Domain;

namespace EHR.Controller
{
    public class AccountController : EHRController
    {
        public override void Register(string firstName, string lastName, GenderEnum gender, string crm,
                                      string email, string password, DateTime birthday, IList<short> hospitals)
        {
            Assertion.IsFalse(string.IsNullOrEmpty(firstName), "Primeiro nome não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(lastName), "Ultimo nome não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(crm), "CRM não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(email), "E-mail não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(password), "Senha não informada.").Validate();
            Assertion.GreaterThan(birthday, DateTime.MinValue, "Data de aniverssario não informada.").Validate();
            //TODO: Gender validation

            var accounts = (Accounts)FactoryRepository.GetRepository(RepositoryEnum.Accounts);

            Assertion.Null(accounts.GetBy(email), "E-mail já cadastrado.");

            var account = SetPropertiesOfAccount(firstName, lastName, gender, crm, email, password, birthday, hospitals);

            account.EncryptPassword();
            accounts.Save(account);
        }

        public override Account Login(string email, string password)
        {
            var accounts = (Accounts)FactoryRepository.GetRepository(RepositoryEnum.Accounts);
            var passwordEncrypted = CryptographyUtil.EncryptToSha512(password);
            var account = accounts.GetBy(email, passwordEncrypted);
            return account;
        }

        public override bool VerifyIfExist(string email)
        {
            var registered = ((Accounts)FactoryRepository.GetRepository(RepositoryEnum.Accounts)).GetBy(email);
            Assertion.Null(registered, "E-mail já cadastrado.");
            return registered == null;
        }

        public override IList<Account> GetAllNotApproved()
        {
            var accounts = (Accounts)FactoryRepository.GetRepository(RepositoryEnum.Accounts);
            return accounts.GetAllNotApproved();
        }

        public override IList<Summary> GetLastTenSumaries(int id)
        {
            var account = FactoryRepository.GetRepository(RepositoryEnum.Accounts).Get<Account>(id);
            var sumaries = ((Summaries)FactoryRepository.GetRepository(RepositoryEnum.Sumaries)).GetLastTenSummaries(account);
            return sumaries;
        }

        #region Private Methods

        private Account SetPropertiesOfAccount(string firstName, string lastName, GenderEnum gender, string crm,
                                                   string email, string password, DateTime birthday, IList<short> hospitals)
        {
            var account = new Account
                              {
                                  Birthday = birthday,
                                  CRM = crm,
                                  Email = email,
                                  FirstName = firstName,
                                  LastName = lastName,
                                  Password = password,
                                  Approved = false,
                                  Administrator = false,
                                  Gender = gender
                              };
            var hospitalsList = GetHospitalsFromRepository(hospitals);
            account.Hospitals = hospitalsList;

            return account;
        }

        private IList<Hospital> GetHospitalsFromRepository(IList<short> hospitals)
        {
            var hospitalRepository = (Hospitals)FactoryRepository.GetRepository(RepositoryEnum.Hospitals);
            var hospitalsList = hospitalRepository.GetBy(hospitals);
            return hospitalsList;
        }

        #endregion
    }
}
