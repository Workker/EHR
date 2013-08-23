using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class ProcedureMap : ClassMap<Procedure>
    {
        public ProcedureMap()
        {
            Table("MedicalProcedure");
            Id(x => x.Id);
            References(x => x.Tus);
            Map(x => x.Date).Column("ExecuteDate");
        }
    }
}
