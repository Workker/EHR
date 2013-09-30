using System;

namespace EHR.Domain.Entities
{
    public class HistoryRecord
    {
        public virtual int Id { get; set; }
        public virtual Account Account { get; set; }
        public virtual HistoricalActionType Action { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string Description { get; set; }
    }
}
