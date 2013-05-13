using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Criterion;
using EHR.Domain.Entities;

namespace EHR.Domain.Repository
{
    public class Summaries : BaseRepository
    {
        public Summary GetLastSummary(string cpf)
        {
            var criterio = Session.CreateCriteria<Summary>();
            criterio.Add(Expression.Eq("Cpf", cpf));

            return criterio.List<Summary>().OrderByDescending(s => s.Date).FirstOrDefault();
        }
    }
}
