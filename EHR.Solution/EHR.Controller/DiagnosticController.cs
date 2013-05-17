using EHR.Domain.Entities;
using EHR.Domain.Repository;
using System.Collections.Generic;
using System.Linq;
using EHR.Domain.Service.Lucene;
using EHR.CoreShared;
using Workker.Framework.Domain;

namespace EHR.Controller
{
    public class DiagnosticController : EHRController
    {
        #region Properties

        private Cids _cidsRepository;
        public Cids CidsRepository
        {
            get { return _cidsRepository ?? (_cidsRepository = new Cids()); }
            set
            {
                _cidsRepository = value;
            }
        }

        private Types<DiagnosticType> diagnosticTypes;
        public Types<DiagnosticType> DiagnosticTypes
        {
            get { return diagnosticTypes ?? (diagnosticTypes = new Types<DiagnosticType>()); }
            set
            {
                diagnosticTypes = value;
            }
        }

        private GetCidLucene getCidLucene;
        public GetCidLucene GetCidLucene
        {
            get { return getCidLucene ?? (getCidLucene = new GetCidLucene()); }
            set
            {
                getCidLucene = value;
            }
        }


        #endregion

        public override List<CidDTO> GetCids(string term)
        {
            return GetCidLucene.GetCid(term);
        }

        public override void SaveDiagnostic(string diagnosticType, string cid, Summary summary)
        {
            Assertion.IsFalse(string.IsNullOrEmpty(diagnosticType), "Tipo do diagnostico não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(cid), "Cid não informado.").Validate();
            Assertion.NotNull(summary, "Sumário foi encontrado, por favor selecione um sumário de alta.").Validate();

            var cidObj = CidsRepository.GetByCode(cid);
            var typeDiagnostic = DiagnosticTypes.Get(short.Parse(diagnosticType));
            summary.CreateDiagnostic(typeDiagnostic, cidObj);
            Summaries.Save(summary);


        }

        public override void RemoveDiagnostic(Summary summary, int id)
        {
            summary.RemoveDiagnostic(id);
            Summaries.Save(summary);
        }
    }
}
