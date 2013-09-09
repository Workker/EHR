using AutoMapper;
using EHR.Domain.Entities;
using System.Collections.Generic;

namespace EHR.UI.Models.Mappers
{
    public static class AccountMapper
    {
        public static List<AccountModel> MapAccountModelFrom(IEnumerable<Account> accounts)
        {
            var accountModels = new List<AccountModel>();

            foreach (var item in accounts)
            {
                accountModels.Add(MapAccountModelFrom(item));
            }

            return accountModels;
        }

        public static AccountModel MapAccountModelFrom(Account accountObject)
        {
            Mapper.CreateMap<Account, AccountModel>().ForMember(dest => dest.Hospital, source => source.Ignore()).ForMember(dest => dest.ProfessionalRegistration, source => source.Ignore());

            var account = Mapper.Map<Account, AccountModel>(accountObject);

            account.Hospital = accountObject.Hospital.Id;

            foreach (var profissionalRegistration in accountObject.ProfessionalRegistration)
            {
                account.ProfessionalRegistration.Add(ProfessionalRegistrationMapper.MapProfessionalRegistrationModelFrom(profissionalRegistration));
            }
            return account;
        }
    }
}