using System.Collections.Generic;
using EHR.Domain.Entities;
using NHibernate.Criterion;
using System.Linq;

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

        public IList<Summary> GetSummaries(Account account)
        {
            var criterio = Session.CreateCriteria<Summary>();
            criterio.Add(Expression.Eq("Account", account));
            return criterio.List<Summary>().OrderByDescending(s => s.Date).ToList();
        }
    }
}