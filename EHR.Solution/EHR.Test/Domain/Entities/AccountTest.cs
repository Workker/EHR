using EHR.CoreShared.Entities;
using EHR.Domain.Entities;
using NUnit.Framework;
using System;
using System.Diagnostics.Contracts;

namespace EHR.Test.Domain.Entities
{
    [TestFixture]
    public class AccountTest
    {
        private Account _account;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _account = new Account(false);
        }

        //[Test]
        //public void to_enter_a_crm_valid_successfully()
        //{
        //    _account.ToEnterCRM("CRM Test");

        //    Contract.Assert(_account.CRM.Equals("CRM Test"));
        //}

        //[Test]
        //[ExpectedException(UserMessage = "Invalid CRM!")]
        //public void to_disallow_enter_an_invalid_crm()
        //{
        //    _account.ToEnterCRM(string.Empty);
        //}

        [Test]
        public void to_enter_a_first_name_sucessfully()
        {
            _account.ToEnterFirstName("Peter");

            Contract.Assert(_account.FirstName.Equals("Peter"));
        }

        [Test]
        [ExpectedException(UserMessage = "Invalid first name!")]
        public void to_disallow_enter_an_invalid_first_name()
        {
            _account.ToEnterFirstName(string.Empty);
        }

        [Test]
        public void to_enter_a_last_name_sucessfully()
        {
            _account.ToEnterLastName("Cech");

            Contract.Assert(_account.LastName.Equals("Cech"));
        }

        [Test]
        [ExpectedException(UserMessage = "Invalid last name!")]
        public void to_disallow_enter_an_invalid_last_name()
        {
            _account.ToEnterLastName(string.Empty);
        }

        [Test]
        public void to_enter_a_gender_sucessfully()
        {
            _account.ToEnterGender(GenderEnum.Male);

            Contract.Assert(_account.Gender.Equals(GenderEnum.Male));
        }

        [Test]
        public void to_enter_an_email_sucessfully()
        {
            _account.ToEnterEmail("test@test.com");

            Contract.Assert(_account.Email.Equals("test@test.com"));
        }

        [Test]
        [ExpectedException(UserMessage = "Invalid email!")]
        public void to_disallow_enter_an_invalid_email()
        {
            _account.ToEnterEmail(string.Empty);
        }

        [Test]
        public void to_enter_a_password_sucessfulyy()
        {
            _account.ToEnterPassword("swordfish");

            Contract.Assert(!string.IsNullOrEmpty(_account.Password));
        }

        [Test]
        [ExpectedException(UserMessage = "Invalid password!")]
        public void to_disallow_enter_an_invalid_password()
        {
            _account.ToEnterPassword(string.Empty);
        }

        [Test]
        public void to_enter_a_birthday_sucessfully()
        {
            _account.ToEnterBirthday(DateTime.Today);
        }

        [Test]
        [ExpectedException(UserMessage = "Invalid birthday!")]
        public void to_disallow_enter_an_invalid_birthday()
        {
            _account.ToEnterBirthday(DateTime.MinValue);
        }

        [Test]
        [ExpectedException(UserMessage = "Birthday greater than the current date!")]
        public void to_disallow_enter_a_birthday_greater_than_the_current_date()
        {
            _account.ToEnterBirthday(DateTime.Today.AddDays(1));
        }

        [Test]
        public void to_add_a_hospital_sucessfully()
        {
            var hospital = new Hospital();

            _account.AddHospital(hospital);

            Contract.Assert(_account.Hospital == hospital);
        }

        [Test]
        [ExpectedException(UserMessage = "Invalid hospital!")]
        public void to_disallow_add_an_invalid_hospital()
        {
            _account.AddHospital(null);
        }

        [Test]
        public void to_approve_account_sucessfully()
        {
            _account.ToApprove(true);

            Assert.IsTrue(_account.Approved);
        }

        [Test]
        public void to_refuse_account_sucessfully()
        {
            _account.ToRefuse(true);

            Assert.IsTrue(_account.Refused);
        }
    }
}