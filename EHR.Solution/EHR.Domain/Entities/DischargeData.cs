using EHR.CoreShared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Workker.Framework.Domain;

namespace EHR.Domain.Entities
{
    public class DischargeData
    {
        #region Properties

        public virtual int Id { get; set; }
        public virtual HighTypeEnum HighType { get; set; }
        public virtual ConditionAtDischarge ConditionAtDischarge { get; set; }
        public virtual DestinationOfThePatientAtDischargeEnum DestinationAtDischarge { get; set; }
        public virtual OrientationOfMultidisciplinaryTeamsMetEnum MultidisciplinaryTeamsMet { get; set; }
        private IList<MedicalReview> _medicalReviews;
        public virtual IList<MedicalReview> MedicalReviews
        {
            get { return _medicalReviews ?? (_medicalReviews = new List<MedicalReview>()); }
        }
        public virtual DateTime? PrescribedHigh { get; set; }
        public virtual string PersonWhoDeliveredTheSummary { get; set; }
        public virtual DateTime? Date { get; set; }
        private IList<ComplementaryExam> _complementaryExams;
        public virtual IList<ComplementaryExam> ComplementaryExams
        {
            get { return _complementaryExams ?? (_complementaryExams = new List<ComplementaryExam>()); }
        }

        #endregion

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

        public virtual void CreateMedicalReview(int termMedicalReviewAt, Specialty specialty)
        {
            Assertion.GreaterThan(termMedicalReviewAt, 0, "Periodo não informado.").Validate();
            Assertion.NotNull(specialty, "Especialidade não informada").Validate();

            var medicalReview = new MedicalReview
            {
                TermMedicalReviewAt = termMedicalReviewAt,
                Specialty = specialty
            };

            MedicalReviews.Add(medicalReview);

            Assertion.IsTrue(MedicalReviews.Contains(medicalReview), "Revisão médica não inserida.").Validate();
        }

        public virtual void RemoveMedicalReview(int id)
        {
            Assertion.GreaterThan(id, 0, "Id não informado.").Validate();

            var medicalReview = MedicalReviews.FirstOrDefault(c => c.Id == id);

            MedicalReviews.Remove(medicalReview);

            Assertion.IsFalse(MedicalReviews.Contains(medicalReview), "Revisão médica não foi removida.").Validate();
        }
    }
}