using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace EHR.Domain.Repository
{
    public class DefsIntegracao : BaseRepository
    {
        public DefsIntegracao(ISession session) :
            base(session)
        {

        }
    }
}
