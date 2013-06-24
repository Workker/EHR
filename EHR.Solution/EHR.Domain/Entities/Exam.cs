using System;
using EHR.Domain.Entities.Interfaces;

namespace EHR.Domain.Entities
{
    public class Exam : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual ExamTypeEnum Type { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string Description { get; set; }
    }
}