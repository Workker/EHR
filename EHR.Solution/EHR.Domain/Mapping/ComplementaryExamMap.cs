using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class ComplementaryExamMap : ClassMap<ComplementaryExam>
    {
        ComplementaryExamMap()
        {
            Id(c => c.Id);
            Map(c => c.Description);
            Map(c => c.Period);
        }
    }
}
