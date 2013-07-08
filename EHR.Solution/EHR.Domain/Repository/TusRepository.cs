using EHR.Domain.Entities;
using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;
using Workker.Framework.Domain;

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

        [ExceptionLogger]
        public virtual Tus GetByCode(string code)
        {
            Assertion.IsTrue(!string.IsNullOrEmpty(code), "Código do TUS inválido").Validate();

            var criterio = Session.CreateCriteria<Tus>();
            criterio.Add(Expression.Eq("Code", code));

            var tus = criterio.UniqueResult<Tus>();

            Assertion.NotNull(tus, "TUS inválido.").Validate();

            return tus;
        }

        //[ExceptionLogger]
        //public virtual void Save(List<Tus> roots)
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
