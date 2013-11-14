using EHR.CoreShared.Entities;
using EHR.Domain.Repository;
using EHRIntegracao.Domain.Services.SaveLucene;
using System.Linq;

namespace EHR.Domain.Service.Lucene
{
    public class UpdateTUSSIndexService
    {
        public void UpdateIndex()
        {
            var repository = new TUSSRepository();
            var tus = repository.All<TUSS>().ToList();
            var service = new SaveTusInLuceneService();

            service.Save(tus);
        }
    }
}
