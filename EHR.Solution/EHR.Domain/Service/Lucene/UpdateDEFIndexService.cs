using System.Configuration;
using EHR.CoreShared.Entities;
using EHR.Domain.Repository;
using EHRIntegracao.Domain.Services.SaveLucene;
using System.Linq;

namespace EHR.Domain.Service.Lucene
{
    public class UpdateDEFIndexService
    {
        public void UpdateIndex()
        {
            var repository = new DEFRepository();
            var def = repository.All<DEF>().ToList();
            var service = new SaveDefInLuceneService(ConfigurationManager.AppSettings["DEFIndexPath"]);

            service.Save(def, ConfigurationManager.AppSettings["DEFIndexPath"]);
        }
    }
}
