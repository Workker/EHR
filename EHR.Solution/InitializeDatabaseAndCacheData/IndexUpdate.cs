using EHR.Domain.Service.Lucene;

namespace InitializeDatabaseAndCacheData
{
    public class IndexUpdate
    {
        public void update_cid_index()
        {
            var service = new UpdateCIDIndexService();

            service.UpdateIndex();

        }

        public void update_def_index()
        {
            var service = new UpdateDEFIndexService();

            service.UpdateIndex();

        }

        public void update_tuss_index()
        {
            var service = new UpdateTUSSIndexService();

            service.UpdateIndex();

        }
    }
}
