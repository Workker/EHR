using System.Configuration;
using EHR.CoreShared.Entities;
using EHRIntegracao.Domain.Services.GetEntities;
using System.Collections.Generic;

namespace EHR.Domain.Service.Lucene
{
    public class GetCidLucene
    {
        private GetCidFromLuceneService _getCidFromLuceneService;
        public virtual GetCidFromLuceneService GetCidFromLuceneService
        {
            get { return _getCidFromLuceneService ?? (_getCidFromLuceneService = new GetCidFromLuceneService(ConfigurationManager.AppSettings["CIDIndexPath"])); }
            set
            {
                _getCidFromLuceneService = value;
            }
        }

        public List<CID> GetCid(string code)
        {
            return GetCidFromLuceneService.GetCid(code);
        }
    }
}
