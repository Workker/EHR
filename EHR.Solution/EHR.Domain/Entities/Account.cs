using EHR.CoreShared;
using EHR.CoreShared.Interfaces;
using EHR.Infrastructure.Util;
using System;
using System.Collections.Generic;
using Workker.Framework.Domain;

namespace EHR.Domain.Entities
{
    public class Account : IAggregateRoot<int>
    {
        #region Fields

        private IList<Hospital> _hospitals;

        #endregion

        #region Properties

        public virtual int Id { get; set; }
        public virtual string CRM { get; protected set; }
        public virtual string FirstName { get; protected set; }
        public virtual string LastName { get; protected set; }
        public virtual GenderEnum Gender { get; protected set; }
        public virtual string Email { get; protected set; }
        public virtual string Password { get; protected set; }
        public virtual DateTime? Birthday { get; protected set; }
        public virtual IList<Hospital> Hospitals
        {
            get { return _hospitals ?? (_hospitals = new List<Hospital>()); }
        }
        public virtual bool Approved { get; protected set; }
        public virtual bool Refused { get; protected set; }
        public virtual bool Administrator { get; protected set; }

        #endregion

        protected Account() { }

        public Account(bool administrator)
        {
            Administrator = administrator;
        }

        public virtual void ToEnterCRM(string crm)
        {
            #region Precondition

            Assertion.IsFalse(string.IsNullOrEmpty(crm), "Invalid CRM!").Validate();

            #endregion

            CRM = crm;
        }

        public virtual void ToEnterFirstName(string firstName)
        {
            #region Precondition

            Assertion.IsFalse(string.IsNullOrEmpty(firstName), "Invalid first name!").Validate();

            #endregion

            FirstName = firstName;
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

            Assertion.IsFalse(string.IsNullOrEmpty(password), "Invalid password!").Validate();

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

            Hospitals.Add(hospital);
        }

        public void ToApprove(bool approved)
        {
            Approved = approved;
        }

        public void ToRefuse(bool refused)
        {
            Refused = refused;
        }
    }
}