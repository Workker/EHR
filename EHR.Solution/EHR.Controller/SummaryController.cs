using System;
using System.Collections.Generic;
using EHR.Domain.Entities;
using EHR.Domain.Repository;
using Workker.Framework.Domain;

namespace EHR.Controller
{
    public class SummaryController : EHRController
    {
        private Defs _defsRepository;
        public Defs DefsRepository
        {
            get { return _defsRepository ?? (_defsRepository = new Defs()); }
            set
            {
                _defsRepository = value;
            }
        }

        public override void SaveMdr(int summaryId, string mdr)
        {
            var summary = Summaries.Get<Summary>(summaryId);
            summary.Mdr = mdr;
            Summaries.Save(summary);
        }

        public override void SaveObservation(int summaryId, string observation)
        {
            var summary = Summaries.Get<Summary>(summaryId);
            summary.Observation = observation;
            Summaries.Save(summary);
        }

        public override Summary GetBy(int id)
        {
            return Summaries.Get<Summary>(id);
        }

        public override void SaveMedication(int idSummary, short medicationType, short def, string presentation,
            string presentationType, string dose, string dosage, string way, string place, string frequency, string frequencyCase, int duration)
        {
            Assertion.NotNull(def, "Medicamento não informado.");
            Assertion.IsFalse(string.IsNullOrEmpty(presentation), "Apresentação não informada.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(presentationType), "Tipo de apresentação não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(dose), "Dose não informada.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(dosage), "Dosagem não informada.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(way), "Via informada.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(place), "Lugar não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(frequency), "Frequencia não informada.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(frequencyCase), "Caso de frequencia não informado.").Validate();
            Assertion.GreaterThan(duration, 0, "Duração não informada.").Validate();

            var summary = Summaries.Get<Summary>(idSummary);
            var defObj = DefsRepository.GetById(def);
            summary.CreateMedication((MedicationTypeEnum)medicationType, defObj, presentation, presentationType, dose, dosage, way, place, frequency, frequencyCase, duration);
            Summaries.Save(summary);
        }

        public override void RemoveMedication(int idSummary, int id)
        {
            var summary = Summaries.Get<Summary>(idSummary);
            summary.RemoveMedication(id);
            Summaries.Save(summary);
        }

        public override void SaveExam(int idSummary, short type, string day, string month, string year, string description)
        {
            var summary = Summaries.Get<Summary>(idSummary);
            summary.CreateExam((ExamTypeEnum)type, int.Parse(day), int.Parse(month), int.Parse(year), description);
            Summaries.Save(summary);
        }

        public override void RemoveExam(int idSummary, int id)
        {
            Assertion.GreaterThan(idSummary, 0, "Duração não informada.").Validate();
            Assertion.GreaterThan(id, 0, "Duração não informada.").Validate();

            var summary = Summaries.Get<Summary>(idSummary);
            summary.RemoveExam(id);
            Summaries.Save(summary);
        }

        public override void SaveHighData(int idSummary, IList<ComplementaryExam> complementaryExams, IList<int> complementaryExamDeleteds, short highType,
            short conditionOfThePatientAtHigh, short destinationOfThePatientAtDischarge,
           short orientationOfMultidisciplinaryTeamsMet, int termMedicalReviewAt, short specialtyId, DateTime prescribedHigh,
            string personWhoDeliveredTheSummary, DateTime deliveredDate)
        {
            var summary = Summaries.Get<Summary>(idSummary);
            var specialty = new Types<Specialty>().Get(specialtyId);

            summary.HighData.HighType = (HighTypeEnum)highType;
            summary.HighData.ConditionOfThePatientAtHigh = (ConditionOfThePatientAtHighEnum)conditionOfThePatientAtHigh;
            summary.HighData.DestinationOfThePatientAtDischarge = (DestinationOfThePatientAtDischargeEnum)destinationOfThePatientAtDischarge;
            summary.HighData.OrientationOfMultidisciplinaryTeamsMet = (OrientationOfMultidisciplinaryTeamsMetEnum)orientationOfMultidisciplinaryTeamsMet;
            summary.HighData.TermMedicalReviewAt = termMedicalReviewAt;
            summary.HighData.Specialty = specialty;
            summary.HighData.PrescribedHigh = prescribedHigh;
            summary.HighData.PersonWhoDeliveredTheSummary = personWhoDeliveredTheSummary;
            summary.HighData.DeliveredDate = deliveredDate;

            foreach (var id in complementaryExamDeleteds)
            {
                summary.HighData.RemoveComplementaryExam(id);
            }

            foreach (var complementaryExam in complementaryExams)
            {
                summary.HighData.ComplementaryExams.Add(complementaryExam);
            }

            Summaries.Save(summary);
        }

        public override void AddView(int idSummary, int idAccount, DateTime date)
        {
            var summary = Summaries.Get<Summary>(idSummary);

            var account = FactoryRepository.GetRepository(RepositoryEnum.Accounts).Get<Account>(idAccount);
            ((Accounts)FactoryRepository.GetRepository(RepositoryEnum.Accounts)).Approve(account);

            summary.AddView(account, date);

            Summaries.Save(summary);
        }
    }
}