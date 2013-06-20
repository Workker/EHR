using EHR.Domain.Repository;
using NHibernate;

namespace EHR.Migration.Repository.Integracao
{
    public class DefsIntegracao : BaseRepository
    {
        public DefsIntegracao(ISession session) :
            base(session)
        {

        }
    }
}
