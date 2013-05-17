using EHR.Domain.Entities;
using EHR.Domain.Repository;
using System.Collections.Generic;
using System.Linq;

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

        #endregion

        public override List<Cid> GetCids()
        {
            return CidsRepository.All<Cid>().ToList();
        }

        public override void SaveDiagnostic(string diagnosticType, string cid, Summary summary)
        {
           

            var cidObj = CidsRepository.GetByCode(cid);

            //summary.CreateDiagnostic();

            //Summaries.Save(summary);
        }

        public override void RemoveDiagnostic(Summary summary, int id)
        {
            summary.RemoveDiagnostic(id);
            Summaries.Save(summary);
        }
    }
}
