using EHR.Domain.Repository;
using NHibernate;

namespace EHR.Migration.Repository.Integracao
{
    public class ProcedimentosIntegracao : BaseRepository
    {
        public ProcedimentosIntegracao(ISession session) :
            base(session)
        {

        }
    }
}
