using EHR.Domain.Entities.Interfaces;


namespace EHR.Migration.Entities.Migracao
{
    public class DefMigracao : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }

        public virtual string Produto { get; set; }
    }
}
