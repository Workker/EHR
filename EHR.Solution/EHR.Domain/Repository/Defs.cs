using System.Collections.Generic;
using EHR.Domain.Entities;
using NHibernate;
using NHibernate.Criterion;

namespace EHR.Domain.Repository
{
    public class Defs : BaseRepository
    {
        public Defs()
        {
        }

        public Defs(ISession session)
            : base(session)
        {

        }

        public virtual Def GetById(string id)
        {
            var criterio = Session.CreateCriteria<Def>();
            criterio.Add(Expression.Eq("Id", id));

            return criterio.UniqueResult<Def>();
        }

        public virtual void Save(List<Def> roots)
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
        public virtual void SalvarLista(List<Def> roots)
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
