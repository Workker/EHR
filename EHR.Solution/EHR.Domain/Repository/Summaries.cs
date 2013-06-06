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

        public List<Summary> GetLastTenSummaries(Account account)
        {
            var criterio = Session.CreateCriteria<Summary>();
            criterio.Add(Expression.Eq("Account", account));
            //TODO: Add order by date of summary

            return criterio.List<Summary>().Take(10).ToList();
        }
    }
}
