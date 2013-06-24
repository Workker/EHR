using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class ExamMap : ClassMap<Exam>
    {
        ExamMap()
        {
            Id(e => e.Id);
            Map(e => e.Type).CustomType<short>();
            Map(e => e.Date);
            Map(e => e.Description);
        }
    }
}
