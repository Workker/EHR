using EHR.CoreShared;
using EHR.Domain.Entities;
using EHR.Domain.Repository;
using EHR.Domain.Service.Lucene;
using System.Collections.Generic;
using Workker.Framework.Domain;

namespace EHR.Controller
{
    public class ProcedureController : EHRController
    {
        private TusRepository _tusRepository;
        public TusRepository TusRepository
        {
            get { return _tusRepository ?? (_tusRepository = new TusRepository()); }
            set
            {
                _tusRepository = value;
            }
        }

        private GetTusLuceneService _getTusLuceneService;
        public GetTusLuceneService GetTusLuceneService
        {
            get { return _getTusLuceneService ?? (_getTusLuceneService = new GetTusLuceneService()); }
            set
            {
                _getTusLuceneService = value;
            }
        }


        public override List<TusDTO> GetTus(string term)
        {
            return GetTusLuceneService.GetTus(term);
        }

        public override void SaveProcedure(string dob_day, string dob_month, string dob_year, string procedureCode, int idSummary)
        {
            Assertion.GreaterThan(int.Parse(dob_month), 0, "Mês inválido").Validate();
            Assertion.GreaterThan(int.Parse(dob_day), 0, "Dia inválido").Validate();
            Assertion.GreaterThan(int.Parse(dob_year), 0, "Ano inválido").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(procedureCode), "Codigo do procedimento inválido").Validate();

            var summary = Summaries.Get<Summary>(idSummary);

            var tus = TusRepository.GetByCode(procedureCode);

            summary.CreateProcedure(int.Parse(dob_month), int.Parse(dob_day), int.Parse(dob_year), tus);

            Summaries.Save(summary);
        }

        public override void RemoveProcedure(int idSummary, int id)
        {
            var summary = Summaries.Get<Summary>(idSummary);
            summary.RemoveProcedure(id);
            Summaries.Save(summary);
        }
    }
}
