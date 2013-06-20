using EHR.Domain.Entities.Interfaces;

namespace EHR.Migration.Entities.Migracao
{
    public class CidMigracao : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual string Descricao { get; set; }
        public virtual string CodigoCid { get; set; }


    }
}
