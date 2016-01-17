using EHR.CoreShared.Entities;
using EHR.Domain.Service.Lucene;
using System.Collections.Generic;
using Workker.Framework.Domain;

namespace EHR.Controller
{
    public class DefController : EhrController
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

        [ExceptionLogger]
        public override List<DEF> GetDef(string term)
        {
            var defList = GetDefLuceneService.GetDef(term);
            
            Assertion.NotNull(defList,"Lista de medicamentos nula.").Validate();
            
            return defList;
        }


    }
}
