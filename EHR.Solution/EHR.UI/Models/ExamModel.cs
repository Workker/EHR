
using System;

namespace EHR.UI.Models
{
    public class ExamModel
    {
        public int Id { get; set; }
        public short Type { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}