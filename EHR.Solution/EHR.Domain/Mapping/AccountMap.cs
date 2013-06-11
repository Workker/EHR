using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class AccountMap : ClassMap<Account>
    {
        public AccountMap()
        {
            Id(a => a.Id);
            Map(a => a.CRM);
            Map(a => a.FirstName);
            Map(a => a.LastName);
            Map(a => a.Gender).CustomType<short>();
            Map(a => a.Email);
            Map(a => a.Password);
            Map(a => a.Birthday);
            HasManyToMany(a => a.Hospitals);
            Map(a => a.Approved);
            Map(a => a.Refused);
            Map(a => a.Administrator);
        }
    }
}