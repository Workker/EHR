using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EHR.CoreShared;
using EHRIntegracao.Domain.Services.GetEntities;

namespace EHR.Domain.Service.Lucene
{
    public class GetTusLuceneService
    {
        private GetTusFromLuceneService getTusFromLuceneService;
        public virtual GetTusFromLuceneService GetTusFromLuceneService
        {
            get { return getTusFromLuceneService ?? (getTusFromLuceneService = new GetTusFromLuceneService()); }
            set
            {
                getTusFromLuceneService = value;
            }
        }

        public List<TusDTO> GetTus(string code)
        {
            return GetTusFromLuceneService.GetTus(code);
        }
    }
}
