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
    }
}
