using AutoMapper;
using EHR.Domain.Entities;
using System.Collections.Generic;

namespace EHR.UI.Models.Mappers
{
    public static class AccountMapper
    {
        public static List<AccountModel> MapAccountModelFrom(IEnumerable<Account> accounts)
        {
            Mapper.CreateMap<Account, AccountModel>().ForMember(dest => dest.Hospitals, source => source.Ignore());

            var accountModels = new List<AccountModel>();

            foreach (var item in accounts)
            {
                var account = Mapper.Map<Account, AccountModel>(item);

                foreach (var hospital in item.Hospitals)
                {
                    account.Hospitals.Add(hospital.Id);
                }
                accountModels.Add(account);
            }

            return accountModels;
        }

        public static AccountModel MapAccountModelFrom(Account accountObject)
        {
            Mapper.CreateMap<Account, AccountModel>().ForMember(dest => dest.Hospitals, source => source.Ignore());
            var account = Mapper.Map<Account, AccountModel>(accountObject);

            foreach (var hospital in accountObject.Hospitals)
            {
                account.Hospitals.Add(hospital.Id);
            }
            return account;
        }
    }
}