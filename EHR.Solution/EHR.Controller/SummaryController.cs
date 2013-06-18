using EHR.Domain.Entities;

namespace EHR.Controller
{
    public class SummaryController : EHRController
    {
        public override void SaveMdr(int summaryId, string mdr)
        {
            var summary = Summaries.Get<Summary>(summaryId);
            summary.Mdr = mdr;
            Summaries.Save(summary);
        }

        public override Summary GetBy(int id)
        {
            return Summaries.Get<Summary>(id);
        }
    }
}
