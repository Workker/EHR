using AutoMapper;
using EHR.Domain.Entities;
using System.Collections.Generic;

namespace EHR.UI.Models.Mappers
{
    public static class AccountMapper
    {
        public static List<AccountModel> MapAccountModelFrom(IEnumerable<Account> accounts)
        {
            Mapper.CreateMap<Account, AccountModel>().ForMember(dest => dest.Hospital, source => source.Ignore());

            var accountModels = new List<AccountModel>();

            foreach (var item in accounts)
            {
                var account = Mapper.Map<Account, AccountModel>(item);

                account.Hospital = item.Hospital.Id;

                accountModels.Add(account);
            }

            return accountModels;
        }

        public static AccountModel MapAccountModelFrom(Account accountObject)
        {
            Mapper.CreateMap<Account, AccountModel>().ForMember(dest => dest.Hospital, source => source.Ignore());

            var account = Mapper.Map<Account, AccountModel>(accountObject);

            account.Hospital = accountObject.Hospital.Id;

            return account;
        }
    }
}