﻿using EHR.CoreShared;
using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;
using Workker.Framework.Domain;

namespace EHR.Domain.Repository
{
    public class TUSSRepository
        : BaseRepository
    {
        public TUSSRepository()
        {
        }

        public TUSSRepository(ISession session)
            : base(session)
        {

        }

        [ExceptionLogger]
        public virtual TUSS GetByCode(string code)
        {
            Assertion.IsTrue(!string.IsNullOrEmpty(code), "Código do TUS inválido").Validate();

            var criterio = Session.CreateCriteria<TUSS>();
            criterio.Add(Restrictions.Eq("Code", code));

            var tus = criterio.UniqueResult<TUSS>();

            Assertion.NotNull(tus, "TUS inválido.").Validate();

            return tus;
        }

        [ExceptionLogger]
        public virtual void Save(IList<TUSS> roots)
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
