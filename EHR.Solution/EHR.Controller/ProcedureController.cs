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

        [ExceptionLogger]
        public override List<TusDTO> GetTus(string term)
        {
            //todo: do

            var tusList = GetTusLuceneService.GetTus(term);

            Assertion.NotNull(tusList, "Lista de procedimentos nula.").Validate();

            return tusList;
        }

        [ExceptionLogger]
        public override void SaveProcedure(string day, string month, string year, string procedureCode, int idSummary)
        {
            Assertion.GreaterThan(int.Parse(month), 0, "Mês inválido").Validate();
            Assertion.GreaterThan(int.Parse(day), 0, "Dia inválido").Validate();
            Assertion.GreaterThan(int.Parse(year), 0, "Ano inválido").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(procedureCode), "Codigo do procedimento inválido").Validate();

            var summary = Summaries.Get<Summary>(idSummary);

            var tus = TusRepository.GetByCode(procedureCode);

            summary.CreateProcedure(int.Parse(month), int.Parse(day), int.Parse(year), tus);

            Summaries.Save(summary);

            //todo: do
        }

        [ExceptionLogger]
        public override void RemoveProcedure(int idSummary, int procedureId)
        {
            Assertion.GreaterThan(idSummary, 0, "Sumário de alta não especificado.").Validate();

            var summary = Summaries.Get<Summary>(idSummary);

            summary.RemoveProcedure(procedureId);
            Summaries.Save(summary);

            //todo: do
        }
    }
}
