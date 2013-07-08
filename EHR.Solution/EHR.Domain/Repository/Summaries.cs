using EHR.Domain.Entities;
using NHibernate.Criterion;
using System.Collections.Generic;
using System.Linq;
using Workker.Framework.Domain;

namespace EHR.Domain.Repository
{
    public class Summaries : BaseRepository
    {
        [ExceptionLogger]
        public Summary GetLastSummary(string cpf)
        {
            Assertion.IsTrue(!string.IsNullOrEmpty(cpf), "CPF não informado.").Validate();

            var criterio = Session.CreateCriteria<Summary>();
            criterio.Add(Restrictions.Eq("Cpf", cpf));

            var summary = criterio.List<Summary>().OrderByDescending(s => s.Date).FirstOrDefault();

            Assertion.NotNull(summary, "Sumário inválido.").Validate();
            return summary;
        }

        [ExceptionLogger]
        public Summary GetSummaryByTreatment(string cpf, string codeMedicalRecord)
        {
            Assertion.IsFalse(!string.IsNullOrEmpty(cpf), "CPF não informado.").Validate();
            Assertion.IsFalse(!string.IsNullOrEmpty(codeMedicalRecord), "Código do prontuario não informado.").Validate();

            var criterio = Session.CreateCriteria<Summary>();
            criterio.Add(Restrictions.Eq("Cpf", cpf));
            criterio.Add(Restrictions.Eq("CodeMedicalRecord", codeMedicalRecord));

            var summary = criterio.UniqueResult<Summary>();

            Assertion.NotNull(summary, "Sumário inválido.").Validate();

            return summary;
        }

        [ExceptionLogger]
        public IList<Summary> GetSummaries(Account account)
        {
            Assertion.NotNull(account, "Conta de usuário inválido.").Validate();

            var criterio = Session.CreateCriteria<Summary>();
            criterio.Add(Restrictions.Eq("Account", account));
            var summaries = criterio.List<Summary>().OrderByDescending(s => s.Date).ToList();

            Assertion.NotNull(summaries, "Lista de sumários nula.").Validate();

            return summaries;
        }

        [ExceptionLogger]
        public IList<Summary> GetAllSummaries(string cpf)
        {
            Assertion.IsTrue(!string.IsNullOrEmpty(cpf), "Paciente inválido.").Validate();

            var criterio = Session.CreateCriteria<Summary>();
            criterio.Add(Restrictions.Eq("Cpf", cpf));
            var summaries = criterio.List<Summary>().ToList();

            Assertion.NotNull(summaries, "Lista de sumários nula.").Validate();

            return summaries;
        }
    }
}