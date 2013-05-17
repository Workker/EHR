using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EHR.CoreShared;
using EHRIntegracao.Domain.Services.GetEntities;

namespace EHR.Domain.Service.Lucene
{
    public class GetCidLucene
    {
        private GetCidFromLuceneService getCidFromLuceneService;
        public virtual GetCidFromLuceneService GetCidFromLuceneService
        {
            get { return getCidFromLuceneService ?? (getCidFromLuceneService = new GetCidFromLuceneService()); }
            set
            {
                getCidFromLuceneService = value;
            }
        }

        public List<CidDTO> GetCid(string code)
        {
            return GetCidFromLuceneService.GetCid(code);
        }
    }
}
