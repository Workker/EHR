using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class ReactionTypeMap : ClassMap<ReactionType>
    {
        public ReactionTypeMap()
        {
            Id(x => x.Id);
            Map(x => x.Description);
        }
    }
}
