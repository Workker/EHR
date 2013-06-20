using EHR.Domain.Repository;
using NHibernate;

namespace EHR.Migration.Repository.Integracao
{
    public class CidsIntegracao : BaseRepository
    {
        public CidsIntegracao(ISession session) :
            base(session)
        {

        }
    }
}
