using EHR.Domain.Entities;
using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;

namespace EHR.Domain.Repository
{
    public class Cids : BaseRepository
    {
        public Cids()
        {
        }

        public Cids(ISession session)
            : base(session)
        {

        }

        public virtual Cid GetByCode(string code)
        {
            var criterio = Session.CreateCriteria<Cid>();
            criterio.Add(Expression.Eq("Code", code));

            return criterio.UniqueResult<Cid>();
        }

        public virtual void Save(List<Cid> roots)
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
