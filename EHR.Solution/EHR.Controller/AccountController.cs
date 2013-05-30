using EHR.Domain.Entities;
using EHR.Domain.Repository;
using System;
using System.Collections.Generic;
using EHR.Domain.Util;

namespace EHR.Controller
{
    public class AccountController : EHRController
    {
        public override void Register(string firstName, string lastName, GenderEnum gender, string crm,
                                      string email, string password, DateTime birthday, IList<short> hospitals)
        {
            var account = SetPropertiesOfAccount(firstName, lastName, gender, crm, email, password, birthday, hospitals);
            var accounts = new Accounts();
            accounts.Save(account);
        }

        public override Account Login(string email, string password)
        {
            var accounts = new Accounts();
            var a = CryptographyUtil.ConvertStringToMd5(password);
            var account = accounts.GetBy(email, a);
            return account;
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
                                  Gender = gender
                              };
            var hospitalsList = GetHospitalsFromRepository(hospitals);
            account.Hospitals = hospitalsList;

            return account;
        }

        private IList<Hospital> GetHospitalsFromRepository(IList<short> hospitals)
        {
            var hospitalRepository = new Hospitals();
            var hospitalsList = hospitalRepository.GetBy(hospitals);
            return hospitalsList;
        }

        #endregion
    }
}
