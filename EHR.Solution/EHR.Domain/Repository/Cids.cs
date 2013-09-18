﻿using EHR.CoreShared;
using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;
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
        public virtual CID GetByCode(string code)
        {
            Assertion.IsTrue(!string.IsNullOrEmpty(code), "Código não informado.").Validate();

            var criterio = Session.CreateCriteria<CID>();
            criterio.Add(Restrictions.Eq("Code", code));

            var cid = criterio.UniqueResult<CID>();

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

        [ExceptionLogger]
        public virtual void SalvarLista(IList<CID> roots)
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
