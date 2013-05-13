using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHR.Domain.Entities;
using NHibernate;

namespace EHR.Domain.Repository
{
    public  class TusRepository
        : BaseRepository
    {
        public TusRepository(ISession session)
            : base(session) 
        {

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
