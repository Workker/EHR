using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace EHR.Domain.Repository.Integracao
{
    public class CidsIntegracao : BaseRepository
    {
        public CidsIntegracao(ISession session) :
            base(session)
        {

        }
    }
}
