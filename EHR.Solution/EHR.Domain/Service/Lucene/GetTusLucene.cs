using System.Configuration;
using EHR.CoreShared.Entities;
using EHRIntegracao.Domain.Services.GetEntities;
using System.Collections.Generic;

namespace EHR.Domain.Service.Lucene
{
    public class GetTusLuceneService
    {
        private GetTusFromLuceneService _getTusFromLuceneService;
        public virtual GetTusFromLuceneService GetTusFromLuceneService
        {
            get { return _getTusFromLuceneService ?? (_getTusFromLuceneService = new GetTusFromLuceneService(ConfigurationManager.AppSettings["TUSSIndexPath"])); }
            set
            {
                _getTusFromLuceneService = value;
            }
        }

        public List<TUSS> GetTus(string code)
        {
            return GetTusFromLuceneService.GetTus(code);
        }
    }
}
