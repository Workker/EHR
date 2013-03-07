using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace EHR.Domain.Repository
{
    public class ProcedimentosIntegracao : BaseRepository
    {
        public ProcedimentosIntegracao(ISession session) :
            base(session)
        {

        }
    }
}
