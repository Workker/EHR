using System;
using System.Collections.Generic;
using System.Linq;
using EHR.CoreShared.Interfaces;
using Workker.Framework.Domain;

namespace EHR.Domain.Entities
{
    public class DischargeData : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual HighTypeEnum HighType { get; set; }
        public virtual ConditionOfThePatientAtDischarge ConditionOfThePatientAtDischarge { get; set; }
        public virtual DestinationOfThePatientAtDischargeEnum DestinationOfThePatientAtDischarge { get; set; }
        public virtual OrientationOfMultidisciplinaryTeamsMetEnum OrientationOfMultidisciplinaryTeamsMet { get; set; }
        public virtual int TermMedicalReviewAt { get; set; }
        public virtual Specialty Specialty { get; set; }
        public virtual DateTime? PrescribedHigh { get; set; }
        public virtual string PersonWhoDeliveredTheSummary { get; set; }
        public virtual DateTime? DeliveredDate { get; set; }
        private IList<ComplementaryExam> _complementaryExams;
        public virtual IList<ComplementaryExam> ComplementaryExams
        {
            get { return _complementaryExams ?? (_complementaryExams = new List<ComplementaryExam>()); }
        }

        public virtual void CreateComplementaryExam(string description, int period)
        {
            Assertion.IsFalse(string.IsNullOrEmpty(description), "Descrição não informada.").Validate();
            Assertion.GreaterThan(period, 0, "Periodo não informado.").Validate();

            var complementaryexam = new ComplementaryExam
            {
                Description = description,
                Period = period
            };

            ComplementaryExams.Add(complementaryexam);

            Assertion.IsTrue(ComplementaryExams.Contains(complementaryexam), "Exame complementar não foi inserido.").Validate();
        }

        public virtual void RemoveComplementaryExam(int id)
        {
            Assertion.GreaterThan(id, 0, "Id não informado.").Validate();

            var complementaryExam = ComplementaryExams.FirstOrDefault(c => c.Id == id);

            ComplementaryExams.Remove(complementaryExam);

            Assertion.IsFalse(ComplementaryExams.Contains(complementaryExam), "Exame complementar não foi removido.").Validate();
        }
    }
}