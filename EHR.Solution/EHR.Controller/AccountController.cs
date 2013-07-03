﻿using EHR.Domain.Entities;
using EHR.Domain.Repository;
using EHR.Domain.Util;
using System;
using System.Collections.Generic;
using Workker.Framework.Domain;

namespace EHR.Controller
{
    public class AccountController : EHRController 
    {
        public override void Register(string firstName, string lastName, short gender, string crm,
                                      string email, string password, DateTime birthday, IList<short> hospitals)
        {
            Assertion.IsFalse(string.IsNullOrEmpty(firstName), "Primeiro nome não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(lastName), "Ultimo nome não informado.").Validate();
            Assertion.GreaterThan((int)gender, 0, "Género não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(crm), "CRM não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(email), "E-mail não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(password), "Senha não informada.").Validate();
            Assertion.GreaterThan(birthday, DateTime.MinValue, "Data de aniverssario não informada.").Validate();
            Assertion.GreaterThan(hospitals.Count, 0, "Hospital(is) não informado(s).").Validate();

            var accounts = (Accounts)FactoryRepository.GetRepository(RepositoryEnum.Accounts);

            Assertion.Null(accounts.GetBy(email), "E-mail já cadastrado.").Validate();

            var account = SetPropertiesOfAccount(firstName, lastName, (GenderEnum)gender, crm, email, password, birthday, hospitals);

            account.EncryptPassword();
            accounts.Save(account);

            Assertion.GreaterThan(account.Id, 0, "Conta de usuário não criada.");
        }

        public override Account Login(string email, string password)
        {
            Assertion.IsFalse(string.IsNullOrEmpty(email), "E-mail não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(password), "E-mail não informado.").Validate();

            var accounts = (Accounts)FactoryRepository.GetRepository(RepositoryEnum.Accounts);
            var passwordEncrypted = CryptographyUtil.EncryptToSha512(password);
            var account = accounts.GetBy(email, passwordEncrypted);

            Assertion.NotNull(account, "Conta de usuário não encontrada.").Validate();
            return account;
        }

        public override bool VerifyIfExist(string email)
        {
            Assertion.IsFalse(string.IsNullOrEmpty(email), "E-mail não informado.").Validate();

            var registered = ((Accounts)FactoryRepository.GetRepository(RepositoryEnum.Accounts)).GetBy(email);

            Assertion.Null(registered, "E-mail já cadastrado.").Validate();

            return registered == null;
        }

        public override IList<Account> GetAllNotApproved()
        {
            var accounts = (Accounts)FactoryRepository.GetRepository(RepositoryEnum.Accounts);
            var accountList = accounts.GetAllNotApproved();

            Assertion.NotNull(accountList, "A lista retornada está vázia.").Validate();

            return accountList;
        }

        public override IList<Summary> GetSumaries(int accountId)
        {
            Assertion.GreaterThan(accountId, 0, "Usuário inválido.").Validate();

            var account = FactoryRepository.GetRepository(RepositoryEnum.Accounts).Get<Account>(accountId);

            //todo: doubts 

            return ((Summaries)FactoryRepository.GetRepository(RepositoryEnum.Sumaries)).GetSummaries(account);
        }

        public override void ApproveAccount(int accountId)
        {
            Assertion.GreaterThan(accountId, 0, "Usuário inválido.").Validate();
            var account = FactoryRepository.GetRepository(RepositoryEnum.Accounts).Get<Account>(accountId);
            ((Accounts)FactoryRepository.GetRepository(RepositoryEnum.Accounts)).Approve(account);
            //todo: doubts
        }

        public override void RefuseAccount(int accountId)
        {
            Assertion.GreaterThan(accountId, 0, "Usuário inválido.").Validate();
            var account = FactoryRepository.GetRepository(RepositoryEnum.Accounts).Get<Account>(accountId);
            ((Accounts)FactoryRepository.GetRepository(RepositoryEnum.Accounts)).Refuse(account);
            //todo: doubts
        }

        public override void AlterPasswordOfAccount(int accountId, string password)
        {
            Assertion.GreaterThan(accountId, 0, "Usuário inválido.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(password), "Nova senha não informada.").Validate();

            var account = FactoryRepository.GetRepository(RepositoryEnum.Accounts).Get<Account>(accountId);
            account.Password = password;
            account.EncryptPassword();
            ((Accounts)FactoryRepository.GetRepository(RepositoryEnum.Accounts)).Save(account);
        }

        public IList<Hospital> GetHospitalsFromRepository(IList<short> hospitals)
        {
            Assertion.GreaterThan(hospitals.Count, 0, "Hospital(is) não informado(s).").Validate();

            var hospitalRepository = (Hospitals)FactoryRepository.GetRepository(RepositoryEnum.Hospitals);
            var hospitalsList = hospitalRepository.GetBy(hospitals);

            Assertion.NotNull(hospitalsList, "Não foram retornados hospitais.");

            return hospitalsList;
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
                                  Refused = false,
                                  Administrator = false,
                                  Gender = gender
                              };
            var hospitalsList = GetHospitalsFromRepository(hospitals);
            account.Hospitals = hospitalsList;

            return account;
        }

        #endregion
    }
}
