using EHR.CoreShared;
using EHR.Domain.Entities;
using EHR.Domain.Repository;
using EHR.Domain.Service.Lucene;
using EHR.Infrastructure.Util;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using Workker.Framework.Domain;

namespace EHR.Controller
{
    public class AccountController : EhrController
    {
        [ExceptionLogger]
        public override void Register(string firstName, string lastName, short gender, short professionalResgistrationType, string professionalResgistrationNumber, string email,
                                      string password, DateTime birthday, short hospitalId)
        {
            ToRegisterAccount(firstName, lastName, gender, professionalResgistrationType, professionalResgistrationNumber, email, password, birthday, hospitalId);
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
        public override IList<Account> GetAllNotApproved(short hospitalId)
        {
            var accounts = (Accounts)FactoryRepository.GetRepository(RepositoryEnum.Accounts);

            var hospitals = new Types<Hospital>();
            var hospital = hospitals.Get(hospitalId);

            var accountList = accounts.GetAllNotApproved(hospital);

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

            ToSendEmail(account.Email, "Rede D'or São Luiz - Aprovação de Cadastro", "Seu cadastro no sistema ERH foi aprovado.");
        }

        [ExceptionLogger]
        public override void RefuseAccount(int accountId)
        {
            Assertion.GreaterThan(accountId, 0, "Usuário inválido.").Validate();

            var account = FactoryRepository.GetRepository(RepositoryEnum.Accounts).Get<Account>(accountId);
            ((Accounts)FactoryRepository.GetRepository(RepositoryEnum.Accounts)).Refuse(account);

            Assertion.IsTrue(account.Refused, "A conta não pode ser recusada.").Validate();

            ToSendEmail(account.Email, "Rede D'or São Luiz - Aprovação de Cadastro", "Seu cadastro no sistema ERH foi reprovado. \n Entre em contato com o responsavel pela aprovação na unidade " + account.Hospital.Name + ".");
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
        public Hospital GetHospitalFromRepository(short hospitalId)
        {
            #region Precondition

            Assertion.GreaterThan((int)hospitalId, 0, "Hospital não informado.").Validate();

            #endregion

            var hospitalRepository = (Hospitals)FactoryRepository.GetRepository(RepositoryEnum.Hospitals);

            var hospitalsList = hospitalRepository.GetBy(hospitalId);

            #region Poscondition

            Assertion.NotNull(hospitalsList, "Não foram retornados hospitais.");

            #endregion

            return hospitalsList;
        }


        #region Private Methods

        private void ToRegisterAccount(string firstName, string lastName, short gender, short professionalResgistrationType, string professionalResgistrationNumber, string email,
                                       string password, DateTime birthday, short hospitalId)
        {
            #region Precondition

            Assertion.IsFalse(string.IsNullOrEmpty(firstName), "Primeiro nome não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(lastName), "Ultimo nome não informado.").Validate();
            Assertion.GreaterThan((int)gender, 0, "Género não informado.").Validate();
            Assertion.GreaterThan((int)professionalResgistrationType, 0, "Tipo do registro profissional não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(professionalResgistrationNumber), "Numero do registro profissional não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(email), "E-mail não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(password), "Senha não informada.").Validate();
            Assertion.GreaterThan(birthday, DateTime.MinValue, "Data de aniverssario não informada.").Validate();
            Assertion.GreaterThan((int)hospitalId, 0, "Hospital não informado(s).").Validate();

            #endregion

            var accounts = (Accounts)FactoryRepository.GetRepository(RepositoryEnum.Accounts);

            Assertion.Null(accounts.GetBy(email), "E-mail já cadastrado.").Validate();

            var account = CreateAccount(firstName, lastName, (GenderEnum)gender, (ProfessionalRegistrationTypeEnum)professionalResgistrationType,
                professionalResgistrationNumber, email, password, birthday, hospitalId);

            accounts.Save(account);

            #region Poscondition

            Assertion.GreaterThan(account.Id, 0, "Conta de usuário não criada.").Validate();

            #endregion
        }

        private Account CreateAccount(string firstName, string lastName, GenderEnum gender, ProfessionalRegistrationTypeEnum professionalResgistrationType,
            string professionalResgistrationNumber, string email, string password, DateTime birthday, short hospitalId)
        {
            var hospital = GetHospitalFromRepository(hospitalId);

            var professionalResgistration = new ProfessionalRegistration
                                                {
                                                    Number = professionalResgistrationNumber,
                                                    State = hospital.State,
                                                    Type = professionalResgistrationType
                                                };

            var account = new Account(false);
            account.ToApprove(false);
            account.ToRefuse(false);
            account.AddProfessionalRegistration(professionalResgistration);
            account.ToEnterFirstName(firstName);
            account.ToEnterLastName(lastName);
            account.ToEnterGender(gender);
            account.ToEnterEmail(email);
            account.ToEnterPassword(password);
            account.ToEnterBirthday(birthday);



            account.AddHospital(hospital);

            return account;
        }

        private void ToSendEmail(string email, string subject, string mensage)
        {
            var emails = new List<MailAddress>
                             {
                                 new MailAddress(email),
                             };

            EmailUtil.EnviarEmail(subject, mensage, emails);
        }

        #endregion
    }
}