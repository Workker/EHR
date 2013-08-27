using System.Net.Mail;
using EHR.CoreShared;
using EHR.Domain.Entities;
using EHR.Domain.Repository;
using EHR.Domain.Service.Lucene;
using EHR.Infrastructure.Util;
using System;
using System.Collections.Generic;
using Workker.Framework.Domain;

namespace EHR.Controller
{
    public class AccountController : EhrController
    {
        [ExceptionLogger]
        public override void Register(string firstName, string lastName, short gender, string crm, string email,
                                      string password, DateTime birthday, IList<short> hospitals)
        {
            ToRegisterAccount(firstName, lastName, gender, crm, email, password, birthday, hospitals);

            ToSendEmail();
        }

        private void ToSendEmail()
        {
            //TODO: Passar aqui os e-mails dos administradores
            var emails = new List<MailAddress>()
                             {
                                 new MailAddress("thiago@workker.com.br"),
                                 new MailAddress("ramonfelipe@workker.com.br")
                             };

            EmailUtil.EnviarEmail("Aprovação de Cadastro", "Aprova ae!", emails);
        }

        private void ToRegisterAccount(string firstName, string lastName, short gender, string crm, string email,
                                       string password, DateTime birthday, IList<short> hospitals)
        {
            #region Precondition

            Assertion.IsFalse(string.IsNullOrEmpty(firstName), "Primeiro nome não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(lastName), "Ultimo nome não informado.").Validate();
            Assertion.GreaterThan((int)gender, 0, "Género não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(crm), "CRM não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(email), "E-mail não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(password), "Senha não informada.").Validate();
            Assertion.GreaterThan(birthday, DateTime.MinValue, "Data de aniverssario não informada.").Validate();
            Assertion.GreaterThan(hospitals.Count, 0, "Hospital(is) não informado(s).").Validate();

            #endregion

            var accounts = (Accounts)FactoryRepository.GetRepository(RepositoryEnum.Accounts);

            Assertion.Null(accounts.GetBy(email), "E-mail já cadastrado.").Validate();

            var account = CreateAccount(firstName, lastName, (GenderEnum)gender, crm, email, password, birthday, hospitals);

            accounts.Save(account);

            #region Poscondition

            Assertion.GreaterThan(account.Id, 0, "Conta de usuário não criada.").Validate();

            #endregion
        }

        [ExceptionLogger]
        public override Account Login(string email, string password)
        {
            #region Precondition

            Assertion.IsFalse(string.IsNullOrEmpty(email), "E-mail não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(password), "E-mail não informado.").Validate();

            #endregion

            var accounts = (Accounts)FactoryRepository.GetRepository(RepositoryEnum.Accounts);

            var passwordEncrypted = CryptographyUtil.EncryptToSha512(password);

            var account = accounts.GetBy(email, passwordEncrypted);

            #region Poscondition

            Assertion.NotNull(account, "Conta de usuário não encontrada.").Validate();

            #endregion

            return account;
        }

        [ExceptionLogger]
        public override IList<Account> GetAllNotApproved()
        {
            var accounts = (Accounts)FactoryRepository.GetRepository(RepositoryEnum.Accounts);

            var accountList = accounts.GetAllNotApproved();

            #region Poscondition

            Assertion.NotNull(accountList, "A lista retornada está nula.").Validate();

            #endregion

            return accountList;
        }

        [ExceptionLogger]
        public override IList<Summary> GetLastSumariesRealizedby(int accountId)
        {
            #region Precondition

            Assertion.GreaterThan(accountId, 0, "Usuário inválido.").Validate();

            #endregion

            var account = FactoryRepository.GetRepository(RepositoryEnum.Accounts).Get<Account>(accountId);

            var summaryList =
                ((Summaries)FactoryRepository.GetRepository(RepositoryEnum.Sumaries)).GetLastSumariesrealizedby(account);

            var service = new GetPatientByHospitalService();

            foreach (var summary in summaryList)
            {
                summary.Patient = service.GetPatientBy(summary.Cpf);
            }

            #region Poscondition

            Assertion.NotNull(summaryList, "A lista retornada está nula.").Validate();

            #endregion

            return summaryList;
        }

        [ExceptionLogger]
        public override void ApproveAccount(int accountId)
        {
            Assertion.GreaterThan(accountId, 0, "Usuário inválido.").Validate();

            var account = FactoryRepository.GetRepository(RepositoryEnum.Accounts).Get<Account>(accountId);
            ((Accounts)FactoryRepository.GetRepository(RepositoryEnum.Accounts)).Approve(account);

            Assertion.IsTrue(account.Approved, "Conta não aprovada.").Validate();
        }

        [ExceptionLogger]
        public override void RefuseAccount(int accountId)
        {
            Assertion.GreaterThan(accountId, 0, "Usuário inválido.").Validate();

            var account = FactoryRepository.GetRepository(RepositoryEnum.Accounts).Get<Account>(accountId);
            ((Accounts)FactoryRepository.GetRepository(RepositoryEnum.Accounts)).Refuse(account);

            Assertion.IsTrue(account.Refused, "A conta não pode ser recusada.").Validate();
        }

        [ExceptionLogger]
        public override void AlterPasswordOfAccount(int accountId, string password)
        {
            #region Precondition

            Assertion.GreaterThan(accountId, 0, "Usuário inválido.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(password), "Nova senha não informada.").Validate();

            #endregion

            var account = FactoryRepository.GetRepository(RepositoryEnum.Accounts).Get<Account>(accountId);

            account.ToEnterPassword(password);

            ((Accounts)FactoryRepository.GetRepository(RepositoryEnum.Accounts)).Save(account);

            #region Poscondition

            Assertion.Equals(account.Password, CryptographyUtil.EncryptToSha512(password), "Senha não alterada.").
                Validate();

            #endregion
        }

        [ExceptionLogger]
        public IList<Hospital> GetHospitalsFromRepository(IList<short> hospitals)
        {
            #region Precondition

            Assertion.GreaterThan(hospitals.Count, 0, "Hospital(is) não informado(s).").Validate();

            #endregion

            var hospitalRepository = (Hospitals)FactoryRepository.GetRepository(RepositoryEnum.Hospitals);

            var hospitalsList = hospitalRepository.GetBy(hospitals);

            #region Poscondition

            Assertion.NotNull(hospitalsList, "Não foram retornados hospitais.");

            #endregion

            return hospitalsList;
        }

        private Account CreateAccount(string firstName, string lastName, GenderEnum gender, string crm, string email,
                                      string password, DateTime birthday, IList<short> hospitals)
        {
            var account = new Account(false);
            account.ToApprove(false);
            account.ToRefuse(false);
            account.ToEnterCRM(crm);
            account.ToEnterFirstName(firstName);
            account.ToEnterLastName(lastName);
            account.ToEnterGender(gender);
            account.ToEnterEmail(email);
            account.ToEnterPassword(password);
            account.ToEnterBirthday(birthday);

            var hospitalsList = GetHospitalsFromRepository(hospitals);

            foreach (var hospital in hospitalsList)
            {
                account.AddHospital(hospital);
            }

            return account;
        }
    }
}