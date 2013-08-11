using EHR.Domain.Entities;
using NUnit.Framework;
using System;
using System.Diagnostics.Contracts;

namespace EHR.Test.Domain.Entities
{
    [TestFixture]
    public class AccountTest
    {
        private Account account;

        [TestFixtureSetUp]
        public void SetUp()
        {
            account = new Account();
        }

        [Test]
        public void to_enter_a_crm_valid_successfully()
        {
            account.ToEnterCRM("CRM Test");

            Contract.Assert(account.CRM.Equals("CRM Test"));
        }

        [Test]
        [ExpectedException(UserMessage = "Invalid CRM!")]
        public void to_disallow_enter_an_invalid_crm()
        {
            account.ToEnterCRM(string.Empty);
        }

        [Test]
        public void to_enter_a_first_name_sucessfully()
        {
            account.ToEnterFirstName("Peter");

            Contract.Assert(account.FirstName.Equals("Peter"));
        }

        [Test]
        [ExpectedException(UserMessage = "Invalid first name!")]
        public void to_disallow_enter_an_invalid_first_name()
        {
            account.ToEnterFirstName(string.Empty);
        }

        [Test]
        public void to_enter_a_last_name_sucessfully()
        {
            account.ToEnterLastName("Cech");

            Contract.Assert(account.LastName.Equals("Cech"));
        }

        [Test]
        [ExpectedException(UserMessage = "Invalid last name!")]
        public void to_disallow_enter_an_invalid_last_name()
        {
            account.ToEnterLastName(string.Empty);
        }

        [Test]
        public void to_enter_a_gender_sucessfully()
        {
            account.ToEnterGender(GenderEnum.Male);

            Contract.Assert(account.Gender.Equals(GenderEnum.Male));
        }

        [Test]
        public void to_enter_an_email_sucessfully()
        {
            account.ToEnterEmail("test@test.com");

            Contract.Assert(account.Email.Equals("test@test.com"));
        }

        [Test]
        [ExpectedException(UserMessage = "Invalid email!")]
        public void to_disallow_enter_an_invalid_email()
        {
            account.ToEnterEmail(string.Empty);
        }

        [Test]
        public void to_enter_a_password_sucessfulyy()
        {
            account.ToEnterPassword("swordfish");

            Contract.Assert(!string.IsNullOrEmpty(account.Password));
        }

        [Test]
        [ExpectedException(UserMessage = "Invalid password!")]
        public void to_disallow_enter_an_invalid_password()
        {
            account.ToEnterPassword(string.Empty);
        }

        [Test]
        public void to_enter_a_birthday_sucessfully()
        {
            account.ToEnterBirthday(DateTime.Today);
        }

        [Test]
        [ExpectedException(UserMessage = "Invalid birthday!")]
        public void to_disallow_enter_an_invalid_birthday()
        {
            account.ToEnterBirthday(DateTime.MinValue);
        }

        [Test]
        [ExpectedException(UserMessage = "Birthday greater than the current date!")]
        public void to_disallow_enter_a_birthday_greater_than_the_current_date()
        {
            account.ToEnterBirthday(DateTime.Today.AddDays(1));
        }

        [Test]
        public void to_add_a_hospital_sucessfully()
        {
            var hospital = new Hospital();

            account.AddHospital(hospital);

            Contract.Assert(account.Hospitals.Contains(hospital));
        }

        [Test]
        [ExpectedException(UserMessage = "Invalid hospital!")]
        public void to_disallow_add_an_invalid_hospital()
        {
            account.AddHospital(null);
        }
    }
}