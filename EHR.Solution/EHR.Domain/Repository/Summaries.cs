using System.Collections.Generic;
using EHR.Domain.Entities;
using NHibernate.Criterion;
using System.Linq;
using EHR.CoreShared;
using System;

namespace EHR.Domain.Repository
{
    public class Summaries : BaseRepository
    {
        public Summary GetLastSummary(string cpf)
        {
            var criterio = Session.CreateCriteria<Summary>();
            criterio.Add(Restrictions.Eq("Cpf", cpf));

            return criterio.List<Summary>().OrderByDescending(s => s.Date).FirstOrDefault();
        }

        public Summary GetSummaryByTreatment(string cpf,string codeMedicalRecord)
        {
            var criterio = Session.CreateCriteria<Summary>();
            criterio.Add(Restrictions.Eq("Cpf", cpf));
            criterio.Add(Restrictions.Eq("CodeMedicalRecord", codeMedicalRecord));

            return criterio.UniqueResult<Summary>();
        }

        public IList<Summary> GetSummaries(Account account)
        {
            var criterio = Session.CreateCriteria<Summary>();
            criterio.Add(Restrictions.Eq("Account", account));
            return criterio.List<Summary>().OrderByDescending(s => s.Date).ToList();
        }
    }
}