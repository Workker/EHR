using EHR.CoreShared;
using EHR.Domain.Service.Lucene;
using System.Collections.Generic;

namespace EHR.Controller
{
    public class DefController : EHRController
    {
        private GetDefLuceneService _getDefLuceneService;
        public GetDefLuceneService GetDefLuceneService
        {
            get { return _getDefLuceneService ?? (_getDefLuceneService = new GetDefLuceneService()); }
            set
            {
                _getDefLuceneService = value;
            }
        }

        public override List<DefDTO> GetDef(string term)
        {
            return GetDefLuceneService.GetDef(term);
        }
    }
}
