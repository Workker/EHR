﻿using EHR.CoreShared;
using EHR.Domain.Entities;
using NHibernate;
using NHibernate.Criterion;
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
        public virtual TUS GetByCode(string code)
        {
            Assertion.IsTrue(!string.IsNullOrEmpty(code), "Código do TUS inválido").Validate();

            var criterio = Session.CreateCriteria<TUS>();
            criterio.Add(Restrictions.Eq("Code", code));

            var tus = criterio.UniqueResult<TUS>();

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
