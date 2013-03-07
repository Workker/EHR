using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHR.Domain.Entities.Sumario;
using NHibernate;

namespace EHR.Domain.Repository
{
    public  class Procedimentos
        : BaseRepository
    {
        public Procedimentos(ISession session)
            : base(session) 
        {

        }

        public virtual void SalvarLista(List<Procedimento> roots)
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
