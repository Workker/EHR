using System;

namespace EHR.UI.Models
{
    public class HistoryRecordMoldel
    {
        public int Id { get; set; }
        public AccountModel Account { get; set; }
        public string Action { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}