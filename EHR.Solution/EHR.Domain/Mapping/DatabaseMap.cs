using EHR.CoreShared.Entities;

namespace EHR.Domain.Mapping
{
    public class DatabaseMap : ValueObjectMap<Database>
    {
        public DatabaseMap()
        {
            Table("HospitalDatabase");
            Id(d => d.Id).GeneratedBy.Identity();
            Map(d => d.Host).Column("DbHost");
            Map(d => d.Service).Column("DbService");
            Map(d => d.User).Column("DbUser");
            Map(d => d.Password).Column("DbPassword");
        }
    }
}
