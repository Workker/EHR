using EHR.Domain.Entities;
using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;

namespace EHR.Domain.Repository
{
    public class TusRepository
        : BaseRepository
    {
        public TusRepository()
        {
        }

        public TusRepository(ISession session)
            : base(session)
        {

        }

        public virtual Tus GetByCode(string code)
        {
            var criterio = Session.CreateCriteria<Tus>();
            criterio.Add(Expression.Eq("Code", code));

            return criterio.UniqueResult<Tus>();
        }

        public virtual void Save(List<Tus> roots)
        {
            var transaction = Session.BeginTransaction();

            try
            {
                foreach (var root in roots)
                {
                    Session.SaveOrUpdate(root);
                }
                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }
    }
}
