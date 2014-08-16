using System.Collections.Generic;
using EHR.CoreShared.Entities;
using EHR.CoreShared.Interfaces;
using EHR.Infrastructure.Util;
using System;
using Workker.Framework.Domain;

namespace EHR.Domain.Entities
{
    public class Account : IAggregateRoot<int>
    {
        #region Properties

        public virtual int Id { get; set; }
        public virtual string FirstName { get; protected set; }
        public virtual string LastName { get; protected set; }
        public virtual GenderEnum Gender { get; protected set; }
        public virtual string Email { get; protected set; }
        public virtual string Password { get; protected set; }
        public virtual DateTime? Birthday { get; protected set; }
        public virtual Hospital Hospital { get; set; }
        public virtual bool Approved { get; protected set; }
        public virtual bool Refused { get; protected set; }
        public virtual bool Administrator { get; protected set; }
        private IList<ProfessionalRegistration> _professionalRegistrations;
        public virtual IList<ProfessionalRegistration> ProfessionalRegistrations
        {
            get { return _professionalRegistrations ?? (_professionalRegistrations = new List<ProfessionalRegistration>()); }
        }

        #endregion

        protected Account() { }

        public Account(bool administrator)
        {
            Administrator = administrator;
        }

        public virtual void ToEnterFirstName(string firstName)
        {
            #region Precondition

            Assertion.IsFalse(string.IsNullOrEmpty(firstName), "Invalid first name!").Validate();

            #endregion

            FirstName = firstName;
        }

        public virtual void AddProfessionalRegistration(ProfessionalRegistration professionalRegistration)
        {
            #region Precondition

            Assertion.NotNull(professionalRegistration, "Registro profissional inválido!").Validate();

            #endregion

            ProfessionalRegistrations.Add(professionalRegistration); ;
        }


        public virtual void RemoveProfessionalRegistration(ProfessionalRegistration professionalRegistration)
        {
            #region Precondition

            Assertion.NotNull(professionalRegistration, "Registro profissional inválido!").Validate();

            #endregion

            ProfessionalRegistrations.Remove(professionalRegistration); ;
        }

        public virtual void ToEnterLastName(string lastName)
        {
            #region Precondition

            Assertion.IsFalse(string.IsNullOrEmpty(lastName), "Invalid last name!").Validate();

            #endregion

            LastName = lastName;
        }

        public virtual void ToEnterGender(GenderEnum gender)
        {
            Gender = gender;
        }

        public virtual void ToEnterEmail(string email)
        {
            #region Precondition

            Assertion.IsFalse(string.IsNullOrEmpty(email), "Invalid email!").Validate();

            #endregion

            Email = email;
        }

        public virtual void ToEnterPassword(string password)
        {
            #region Precondition

            Assertion.IsTrue(string.IsNullOrEmpty(password), "A nova senha não foi informada.").Validate();

            #endregion

            Password = CryptographyUtil.EncryptToSha512(password);
        }

        public virtual void ChangePassword(string password, string newPasswordConfirm)
        {
            #region Precondition

            Assertion.IsTrue(string.IsNullOrEmpty(password), "A nova senha não foi informada.").Validate();
            Assertion.IsTrue(string.IsNullOrEmpty(newPasswordConfirm), "A confirmação de senha não foi informada.").Validate();
            Assertion.IsFalse(string.Equals(password, newPasswordConfirm), "Senhas diferentes.").Validate();

            #endregion

            Password = CryptographyUtil.EncryptToSha512(password);
        }

        public virtual void ToEnterBirthday(DateTime? birthday)
        {
            #region Precondition

            Assertion.GreaterThan(birthday, DateTime.MinValue, "Invalid birthday!").Validate();
            Assertion.EqualsOrLessThan(birthday, DateTime.Today, "Birthday greater than the current date!").Validate();

            #endregion

            Birthday = birthday;
        }

        public virtual void AddHospital(Hospital hospital)
        {
            #region Precondition

            Assertion.NotNull(hospital, "Invalid hospital!").Validate();

            #endregion

            Hospital = hospital;
        }

        public virtual void ToApprove(bool approved)
        {
            Approved = approved;
        }

        public virtual void ToRefuse(bool refused)
        {
            Refused = refused;
        }
    }
}