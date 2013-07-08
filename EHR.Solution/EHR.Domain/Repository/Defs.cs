using EHR.Domain.Entities;
using NHibernate;
using NHibernate.Criterion;
using Workker.Framework.Domain;

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

        [ExceptionLogger]
        public virtual Def GetById(short id)
        {
            Assertion.GreaterThan((int)id, 0, "Def inválido.").Validate();

            var criterio = Session.CreateCriteria<Def>();
            criterio.Add(Expression.Eq("Id", id));

            var def = criterio.UniqueResult<Def>();

            Assertion.NotNull(def, "Def inválido.").Validate();

            return def;
        }

        //[ExceptionLogger]
        //public virtual void Save(List<Def> roots)
        //{
        //    var transaction = Session.BeginTransaction();

        //    try
        //    {
        //        foreach (var root in roots)
        //        {
        //            Session.SaveOrUpdate(root);
        //        }
        //        transaction.Commit();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        transaction.Rollback();
        //        throw ex;
        //    }
        //}

        //[ExceptionLogger]
        //public virtual void SalvarLista(List<Def> roots)
        //{
        //    var transaction = Session.BeginTransaction();

        //    try
        //    {
        //        foreach (var root in roots)
        //        {
        //            Session.SaveOrUpdate(root);
        //        }
        //        transaction.Commit();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        transaction.Rollback();
        //        throw ex;
        //    }
        //}
    }
}
