using EHR.Domain.Entities.Interfaces;

namespace EHR.Migration.Entities.Migracao
{
    public class ProcedimentoMigracao : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }

        public virtual string CodigoProcedimento { get; set; }

        public virtual string Procedimento { get; set; }

        public virtual string Grupo { get; set; }

        public virtual string SubGrupo { get; set; }
    }
}
