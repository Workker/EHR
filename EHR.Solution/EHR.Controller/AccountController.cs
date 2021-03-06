﻿using EHR.CoreShared.Entities;
using EHR.Domain.Entities;
using EHR.Domain.Repository;
using EHR.Domain.Service.Lucene;
using EHR.Infrastructure.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Workker.Framework.Domain;

namespace EHR.Controller
{
    public class AccountController : EhrController
    {
        private readonly Accounts _accounts;

        public AccountController()
        {
            _accounts = _accounts ?? (Accounts)FactoryRepository.GetRepository(RepositoryEnum.Accounts);
        }

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

            var passwordEncrypted = CryptographyUtil.EncryptToSha512(password);

            var account = _accounts.GetBy(email, passwordEncrypted);

            #region Poscondition

            Assertion.NotNull(account, "Conta de usuário não encontrada.").Validate();

            #endregion

            return account;
        }

        [ExceptionLogger]
        public override IList<Account> GetAllNotApproved(short hospitalId)
        {
            var hospitals = new Types<Hospital>();
            var hospital = hospitals.Get(hospitalId);

            var accountList = _accounts.GetAllNotApproved(hospital);

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

            var account = _accounts.Get<Account>(accountId);

            var summaryList = Summaries.GetLastSumariesrealizedby(account);

            var service = new GetPatientByHospitalService();

            foreach (var summary in summaryList)
            {
                summary.Patient = service.GetPatientBy(summary.Cpf);

                summary.Treatment =
                    summary.Patient.Treatments.Find(
                        t => t.Id == summary.TreatmentId && t.Hospital.Key == summary.Hospital.Key);
            }

            #region Poscondition

            Assertion.NotNull(summaryList, "A lista retornada está nula.").Validate();

            #endregion

            return summaryList;
        }

        [ExceptionLogger]
        public override void AddprofessionalResgistration(int accountId, short professionalResgistrationType, string professionalResgistrationNumber, short stateId)
        {
            //todo: Implementando

            var account = _accounts.Get<Account>(accountId);
            Assertion.IsTrue(account.ProfessionalRegistrations == null ||
                             account.ProfessionalRegistrations.Count == 0 ||
                             !account.ProfessionalRegistrations.Any(p => p.State.Id == stateId && p.Type == (ProfessionalRegistrationTypeEnum)professionalResgistrationType),
                             "Este estado j&aacute; foi registrado").Validate();

            //((Accounts)FactoryRepository.GetRepository(RepositoryEnum.Accounts)).Approve(account);
            var repository = new Types<State>();
            var state = repository.Get(stateId);
            var professionalRegistration = new ProfessionalRegistration
                                                                    {
                                                                        Number = professionalResgistrationNumber,
                                                                        Type = (ProfessionalRegistrationTypeEnum)professionalResgistrationType,
                                                                        State = state
                                                                    };

            account.AddProfessionalRegistration(professionalRegistration);
            FactoryRepository.GetRepository(RepositoryEnum.Accounts).Save(account);
        }

        [ExceptionLogger]
        public override void ApproveProfessionalRegistration(int accountId, int professionalRegistrationId)
        {
            #region Precondition

            Assertion.GreaterThan(accountId, 0, "Usuário inválido.").Validate();
            Assertion.GreaterThan(professionalRegistrationId, 0, "Registro Profissional inv&aacute;lido.").Validate();

            #endregion

            _accounts.ApproveProfessionalRegistration(accountId, professionalRegistrationId);

            var account = _accounts.Get<Account>(accountId);

            ToSendEmail(account.Email, "Rede D'or São Luiz - Aprovação de Cadastro", "Seu cadastro no sistema ERH foi aprovado.");
        }

        [ExceptionLogger]
        public override void RefuseAccount(int Id, int ProfessionalId)
        {
            Assertion.GreaterThan(Id, 0, "Usuário inv&aacute;lido.").Validate();

            var account = _accounts.Get<Account>(Id);
            var professional = account.ProfessionalRegistrations.FirstOrDefault(p => p.Id == ProfessionalId);
            account.RemoveProfessionalRegistration(professional);
            _accounts.Save(account);

            ToSendEmail(account.Email, "Rede D'or São Luiz - Aprovação de Cadastro", "Seu cadastro no sistema ERH foi reprovado. \n Entre em contato com o responsavel pela aprovação na unidade " + account.Hospital.Name + ".");
        }

        [ExceptionLogger]
        public override void AlterPasswordOfAccount(int accountId, string password, string newPasswordConfirm)
        {
            #region Precondition

            Assertion.GreaterThan(accountId, 0, "Usuário inválido.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(password), "Nova senha não informada.").Validate();

            #endregion

            var account = _accounts.Get<Account>(accountId);

            account.ChangePassword(password, newPasswordConfirm);

            _accounts.Save(account);

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
            Assertion.IsTrue(GetMonthsBetween(DateTime.Now, birthday) >= 216, "Não é permitido o cadastro de menores de 18 anos.").Validate();
            Assertion.GreaterThan((int)hospitalId, 0, "Hospital não informado(s).").Validate();

            #endregion

            var accounts = _accounts;

            Assertion.Null(accounts.GetBy(email), "E-mail já cadastrado.").Validate();

            var hospital = GetHospitalFromRepository(hospitalId);

            var account = CreateAccount(firstName, lastName, (GenderEnum)gender, (ProfessionalRegistrationTypeEnum)professionalResgistrationType,
                professionalResgistrationNumber, email, password, birthday, hospital);

            accounts.Save(account);

            var administrator = accounts.GetAdministratorAccount(hospital);

            ToSendEmail(administrator.Email, "Rede D'or São Luiz - Solicitação de Aprovação de Cadastro", account.FirstName + " " + account.LastName +
                " - " + EnumUtil.GetDescriptionFromEnumValue((ProfessionalRegistrationTypeEnum)Enum.Parse(typeof(ProfessionalRegistrationTypeEnum),
                professionalResgistrationType.ToString(CultureInfo.InvariantCulture))) + ": " + professionalResgistrationNumber +
                ". Efetuou cadastro no sistema sumário de alta." + " Acesse o sistema para aprovar ou recusar o cadastro.");


            #region Poscondition

            Assertion.GreaterThan(account.Id, 0, "Conta de usuário não criada.").Validate();

            #endregion
        }

        private Account CreateAccount(string firstName, string lastName, GenderEnum gender, ProfessionalRegistrationTypeEnum professionalResgistrationType,
            string professionalResgistrationNumber, string email, string password, DateTime birthday, Hospital hospital)
        {


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
            Task.Factory.StartNew(() =>
            {
                var emails = new List<MailAddress>
                             {
                                 new MailAddress(email),
                             };

                EmailUtil.EnviarEmail(subject, mensage, emails);
            });
        }

        private static int GetMonthsBetween(DateTime from, DateTime to)
        {
            if (from > to) return GetMonthsBetween(to, from);

            var monthDiff = Math.Abs((to.Year * 12 + (to.Month - 1)) - (from.Year * 12 + (from.Month - 1)));

            if (from.AddMonths(monthDiff) > to || to.Day < from.Day)
            {
                return monthDiff - 1;
            }
            else
            {
                return monthDiff;
            }
        }

        #endregion
    }
}