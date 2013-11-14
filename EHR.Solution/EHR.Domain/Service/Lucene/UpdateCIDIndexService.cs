using EHR.CoreShared.Entities;
using EHR.Domain.Repository;
using EHRIntegracao.Domain.Services.SaveLucene;
using System.Linq;

namespace EHR.Domain.Service.Lucene
{
    public class UpdateCIDIndexService
    {
        public void UpdateIndex()
        {
            var repository = new CIDRepository();
            var cids = repository.All<CID>().ToList();
            var service = new SaveCidInLuceneService();
            
            service.Save(cids);
        }
    }
}
