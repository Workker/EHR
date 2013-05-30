using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class DoctorMap : ClassMap<Doctor>
    {
        public DoctorMap()
        {
            Id(d => d.Id);
            Map(d => d.CRM);
            Map(d => d.FirstName);
            Map(d => d.LastName);
            References(d => d.Gender);
            Map(d => d.Email);
            Map(d => d.Password);
            Map(d => d.Birthday);
            HasManyToMany(d => d.Hospitals);
        }
    }
}