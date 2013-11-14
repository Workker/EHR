using EHR.CoreShared.Entities;
using NHibernate;
using NHibernate.Criterion;
using Workker.Framework.Domain;

namespace EHR.Domain.Repository
{
    public class CIDRepository : BaseRepository
    {
        public CIDRepository()
        {
        }

        public CIDRepository(ISession session): base(session)
        {

        }

        [ExceptionLogger]
        public virtual CID GetByCode(string code)
        {
            Assertion.IsTrue(!string.IsNullOrEmpty(code), "Código não informado.").Validate();

            var criterio = Session.CreateCriteria<CID>();
            criterio.Add(Restrictions.Eq("Code", code));

            var cid = criterio.UniqueResult<CID>();

            Assertion.NotNull(cid, "Cid inválido.").Validate();

            return cid;
        }
    }
}
