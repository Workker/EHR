using EHR.CoreShared;
using NHibernate;
using NHibernate.Criterion;
using Workker.Framework.Domain;

namespace EHR.Domain.Repository
{
    public class DEFRepository : BaseRepository
    {
        public DEFRepository()
        {
        }

        public DEFRepository(ISession session): base(session)
        {

        }

        [ExceptionLogger]
        public virtual DEF GetById(short id)
        {
            Assertion.GreaterThan((int)id, 0, "Def inválido.").Validate();

            var criterio = Session.CreateCriteria<DEF>();
            criterio.Add(Restrictions.Eq("Id", id));

            var def = criterio.UniqueResult<DEF>();

            Assertion.NotNull(def, "Def inválido.").Validate();

            return def;
        }
    }
}
