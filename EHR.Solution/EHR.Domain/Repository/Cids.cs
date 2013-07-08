using EHR.Domain.Entities;
using NHibernate;
using NHibernate.Criterion;
using Workker.Framework.Domain;

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

        [ExceptionLogger]
        public virtual Cid GetByCode(string code)
        {
            Assertion.IsTrue(!string.IsNullOrEmpty(code), "Código não informado.").Validate();

            var criterio = Session.CreateCriteria<Cid>();
            criterio.Add(Expression.Eq("Code", code));

            var cid = criterio.UniqueResult<Cid>();

            Assertion.NotNull(cid, "Cid inválido.").Validate();

            return cid;
        }

        //[ExceptionLogger]
        //public virtual void Save(List<Cid> roots)
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
        //public virtual void SalvarLista(List<Cid> roots)
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
