using System;

namespace EHR.Domain.Entities
{
    public class HistoryRecord
    {
        public int Id { get; set; }
        public Account Account { get; set; }
        public HistoricalActionType Action { get; set; }
        public DateTime Date { get; set; }
    }
}
