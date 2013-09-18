using EHR.CoreShared;
using EHR.Domain.Entities;
using EHR.Domain.Repository;
using EHR.Domain.Service.Lucene;
using System.Collections.Generic;
using Workker.Framework.Domain;

namespace EHR.Controller
{
    public class DiagnosticController : EhrController
    {

        private CIDRepository _cidsRepository;
        public CIDRepository CidsRepository
        {
            get { return _cidsRepository ?? (_cidsRepository = new CIDRepository()); }
            set
            {
                _cidsRepository = value;
            }
        }

        private Types<DiagnosticType> _diagnosticTypes;
        public Types<DiagnosticType> DiagnosticTypes
        {
            get { return _diagnosticTypes ?? (_diagnosticTypes = new Types<DiagnosticType>()); }
            set
            {
                _diagnosticTypes = value;
            }
        }

        private GetCidLucene _getCidLucene;
        public GetCidLucene GetCidLucene
        {
            get { return _getCidLucene ?? (_getCidLucene = new GetCidLucene()); }
            set
            {
                _getCidLucene = value;
            }
        }

        [ExceptionLogger]
        public override List<CID> GetCids(string term)
        {
            return GetCidLucene.GetCid(term);
        }

        [ExceptionLogger]
        public override void SaveDiagnostic(short diagnosticType, string cid, int summaryId)
        {
            Assertion.GreaterThan((int)diagnosticType, 0, "Tipo do diagnostico não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(cid), "Cid não informado.").Validate();
            Assertion.GreaterThan(summaryId, 0, "Summario de alta inválido.");

            var summary = Summaries.Get<Summary>(summaryId);
            var cidObj = CidsRepository.GetByCode(cid);
            var typeDiagnostic = DiagnosticTypes.Get(diagnosticType);

            summary.CreateDiagnostic(typeDiagnostic, cidObj);
            Summaries.Save(summary);

            //todo: do
        }

        [ExceptionLogger]
        public override void RemoveDiagnostic(int summaryId, int diagnosticId)
        {
            Assertion.GreaterThan(summaryId, 0, "Summario de alta inválido.");
            Assertion.GreaterThan(diagnosticId, 0, "Diagnóstico inválido.");

            var summary = Summaries.Get<Summary>(summaryId);
            summary.RemoveDiagnostic(diagnosticId);
            Summaries.Save(summary);

            //todo: do
        }
    }
}
