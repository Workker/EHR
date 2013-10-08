using EHR.CoreShared.Interfaces;
using EHR.Domain.Entities;
using EHR.Domain.Service.Lucene;
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

            if (summary != null)
            {
                summary.Patient = GetPatient(cpf);
            }

            //Assertion.NotNull(summary, "Sumário inválido.").Validate();
            return summary;
        }

        [ExceptionLogger]
        public Summary GetSummaryByTreatment(string cpf, string codeMedicalRecord)
        {
            Assertion.IsTrue(!string.IsNullOrEmpty(cpf), "CPF não informado.").Validate();
            Assertion.IsTrue(!string.IsNullOrEmpty(codeMedicalRecord), "Código do prontuario não informado.").Validate();

            var criterio = Session.CreateCriteria<Summary>();
            criterio.Add(Restrictions.Eq("Cpf", cpf));
            criterio.Add(Restrictions.Eq("CodeMedicalRecord", codeMedicalRecord));

            var summary = criterio.UniqueResult<Summary>();

            if (summary != null)
            {
                summary.Patient = GetPatient(cpf);
            }

            //Assertion.NotNull(summary, "Sumário inválido.").Validate();

            return summary;
        }

        [ExceptionLogger]
        public IList<Summary> GetLastSumariesrealizedby(Account account)
        {
            Assertion.NotNull(account, "Conta de usuário inválido.").Validate();

            var criterio = Session.CreateCriteria<Summary>();

            criterio.Add(Restrictions.Eq("Account", account));
            criterio.AddOrder(Order.Desc("Date"));

            var summaries = criterio.List<Summary>();

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

        [ExceptionLogger]
        public void FinalizeSummary(int id)
        {
            Assertion.GreaterThan(id, 0, "Sumario de alta invalido.").Validate();

            var summary = base.Get<Summary>(id);

            summary.Finalized = true;

            base.Save(summary);
        }

        [ExceptionLogger]
        public void ReOpenSummary(int id)
        {
            Assertion.GreaterThan(id, 0, "Sumario de alta invalido.").Validate();

            var summary = base.Get<Summary>(id);

            summary.Finalized = false;

            base.Save(summary);
        }

        #region Private Methods

        private IPatient GetPatient(string cpf)
        {
            var service = new GetPatientByHospitalService();
            return service.GetPatientBy(cpf);
        }

        #endregion
    }
}