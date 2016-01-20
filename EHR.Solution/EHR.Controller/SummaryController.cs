using EHR.Domain.DTOs;
using EHR.Domain.Entities;
using EHR.Domain.Repository;
using System;
using System.Collections.Generic;
using Workker.Framework.Domain;

namespace EHR.Controller
{
    public class SummaryController : EhrController
    {
        private readonly DEFRepository _defsRepository;

        public SummaryController()
        {
            _defsRepository = _defsRepository ?? (_defsRepository = new DEFRepository());
        }

        [ExceptionLogger]
        public override Summary GetBy(int id)
        {
            Assertion.GreaterThan(id, 0, "Sumário de alta inválido.").Validate();

            var summary = Summaries.Get<Summary>(id);

            Assertion.NotNull(summary, "Sumário de alta não encontrado.").Validate();

            return summary;
        }

        [ExceptionLogger]
        public override void SaveMdr(int summaryId, string mdr)
        {
            #region Preconditions

            Assertion.GreaterThan(summaryId, 0, "Sumário de alta não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(mdr), "Colonização de germes multiresitentes não informada.").Validate();

            #endregion

            var summary = GetBy(summaryId);

            summary.Mdr = mdr;
            Summaries.Save(summary);
        }

        [ExceptionLogger]
        public override void SaveReasonOfAdmission(int idSummary, IList<short> reasonsOfAdmission)
        {
            var summary = Summaries.Get<Summary>(idSummary);
            var repository = new Types<ReasonOfAdmission>();

            summary.ReasonOfAdmission.Clear();

            foreach (short reason in reasonsOfAdmission)
            {
                summary.ReasonOfAdmission.Add(repository.Get(reason));
            }

            Summaries.Save(summary);
        }

        [ExceptionLogger]
        public override void SaveObservation(int summaryId, string observation)
        {
            #region Preconditions

            Assertion.GreaterThan(summaryId, 0, "Sumário de alta não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(observation), "Observação não informada.").Validate();

            #endregion

            var summary = GetBy(summaryId);

            summary.Observation = observation;
            Summaries.Save(summary);
        }

        [ExceptionLogger]
        public override void SaveMedication(int idSummary, short medicationType, short def, string description, string presentation,
            short presentationType, string dose, short dosage, short way, string place, short frequency, short frequencyCase, int duration)
        {
            #region Preconditions

            Assertion.GreaterThan(idSummary, 0, "Sumário de alta inválido.").Validate();
            Assertion.GreaterThan((int)medicationType, 0, "Tipo de medicação inválido.").Validate();
            Assertion.GreaterThan(duration, 0, "Duração não informada.").Validate();

            if (medicationType == 3)
            {
                Assertion.IsFalse(string.IsNullOrEmpty(presentation), "Apresentação não informada.").Validate();
                Assertion.GreaterThan((int)presentationType, 0, "Tipo de apresentação não informado.").Validate();
                Assertion.IsFalse(string.IsNullOrEmpty(dose), "Dose não informada.").Validate();
                Assertion.GreaterThan((int)dosage, 0, "Dosagem não informada.").Validate();
                Assertion.GreaterThan((int)way, 0, "Via informada.").Validate();
                Assertion.GreaterThan((int)frequency, 0, "Frequencia não informada.").Validate();
            }

            #endregion

            var summary = GetBy(idSummary);

            if (def != short.MinValue)
            {
                Assertion.GreaterThan((int)def, 0, "Medicamento não informado.").Validate();

                var defObj = _defsRepository.GetById(def);

                summary.CreateMedication((MedicationTypeEnum)medicationType, defObj, description, presentation, presentationType, dose, dosage, way, place, frequency, frequencyCase, duration);
            }
            else
            {
                summary.CreateMedication((MedicationTypeEnum)medicationType, description, presentation, presentationType, dose, dosage, way, place, frequency, frequencyCase, duration);
            }

            Summaries.Save(summary);
        }

        [ExceptionLogger]
        public override void RemoveMedication(int idSummary, int id)
        {
            #region Preconditions

            Assertion.GreaterThan(idSummary, 0, "Sumário de alta não informado.").Validate();
            Assertion.GreaterThan(idSummary, 0, "Medicamento não informado.").Validate();

            #endregion

            var summary = GetBy(idSummary);
            summary.RemoveMedication(id);
            Summaries.Save(summary);
        }

        [ExceptionLogger]
        public override void InactivePrescriptionForService(int idSummary, int id)
        {
            #region Preconditions

            Assertion.GreaterThan(idSummary, 0, "Sumário de alta não informado.").Validate();
            Assertion.GreaterThan(idSummary, 0, "Medicamento não informado.").Validate();

            #endregion

            var summary = GetBy(idSummary);
            summary.RemovePrescriptionForService(id);
            Summaries.Save(summary);
        }

        [ExceptionLogger]
        public override void SaveExam(int idSummary, short type, DateTime date, string description)
        {
            #region Preconditions

            Assertion.GreaterThan(idSummary, 0, "Sumário de alta inválido.").Validate();
            Assertion.GreaterThan((int)type, 0, "Tipo de exame inválido.").Validate();
            Assertion.GreaterThan(date, DateTime.MinValue, "Data do exame não informada.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(description), "Descrição não informada.").Validate();

            #endregion

            var summary = GetBy(idSummary);
            summary.CreateExam((ExamTypeEnum)type, date, description);
            Summaries.Save(summary);
        }

        [ExceptionLogger]
        public override void RemoveExam(int summaryId, int examId)
        {
            #region Preconditions

            Assertion.GreaterThan(summaryId, 0, "Duração não informada.").Validate();
            Assertion.GreaterThan(examId, 0, "Duração não informada.").Validate();

            #endregion

            var summary = GetBy(summaryId);
            summary.RemoveExam(examId);
            Summaries.Save(summary);
        }

        [ExceptionLogger]
        public override void SaveDischargeData(int idSummary, IList<ComplementaryExam> complementaryExams, IList<int> complementaryExamDeleteds, IList<MedicalReview> medicalReviews, IList<int> medicalReviewDeleteds, short highType,
            short conditionOfThePatientAtDischargeId, short destinationOfThePatientAtDischarge,
           short orientationOfMultidisciplinaryTeamsMet, DateTime prescribedDischarge,
            string personWhoDeliveredTheSummary, DateTime deliveredDate)
        {
            #region Preconditions

            Assertion.GreaterThan(idSummary, 0, "Sumário de alta inválido.").Validate();
            Assertion.NotNull(complementaryExams, "Lista de exames complementares está nula.").Validate();
            Assertion.NotNull(complementaryExamDeleteds, "Lista de exames complementares deletados está nula.").Validate();
            Assertion.GreaterThan((int)highType, 0, "Tipo de alta inválido.").Validate();
            Assertion.GreaterThan((int)conditionOfThePatientAtDischargeId, 0, "Condição de alta inválida.").Validate();
            Assertion.GreaterThan((int)destinationOfThePatientAtDischarge, 0, "Destino pós alta inválido.").Validate();
            Assertion.GreaterThan((int)orientationOfMultidisciplinaryTeamsMet, 0, "Opção de orientação de equipes multidisciplinares inválida.").Validate();
            Assertion.GreaterThan(prescribedDischarge, DateTime.MinValue, "Data de alta inválida.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(personWhoDeliveredTheSummary), "Nome da pessoa a que o sumário foi entregue não foi informado.").Validate();
            Assertion.GreaterThan(deliveredDate, DateTime.MinValue, "Data de entrega inválida.").Validate();

            #endregion

            var summary = GetBy(idSummary);

            Assertion.NotNull(summary, "Sumário de alta não encontrado.").Validate();

            var conditionOfThePatientAtDischarge = new Types<ConditionAtDischarge>().Get(conditionOfThePatientAtDischargeId);

            summary.HighData.HighType = (HighTypeEnum)highType;
            summary.HighData.ConditionAtDischarge = conditionOfThePatientAtDischarge;
            summary.HighData.DestinationAtDischarge = (DestinationOfThePatientAtDischargeEnum)destinationOfThePatientAtDischarge;
            summary.HighData.MultidisciplinaryTeamsMet = (OrientationOfMultidisciplinaryTeamsMetEnum)orientationOfMultidisciplinaryTeamsMet;

            foreach (var id in medicalReviewDeleteds)
            {
                summary.HighData.RemoveMedicalReview(id);
            }

            foreach (var medicalReview in medicalReviews)
            {
                summary.HighData.MedicalReviews.Add(medicalReview);
            }

            if (summary.Date != prescribedDischarge)
            {
                summary.Date = prescribedDischarge;
            }

            summary.HighData.PersonWhoDeliveredTheSummary = personWhoDeliveredTheSummary;
            summary.HighData.Date = deliveredDate;

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

        [ExceptionLogger]
        public override void AddToHistorical(int idSummary, int idAccount, HistoricalActionTypeEnum actionType, DateTime date, string description)
        {
            #region Preconditions

            Assertion.GreaterThan(idSummary, 0, "Sumário de alta inválido.").Validate();
            Assertion.GreaterThan(idAccount, 0, "Conta de usuário inválida.").Validate();
            Assertion.GreaterThan(date, DateTime.MinValue, "Data inválida.").Validate();

            #endregion

            var summary = GetBy(idSummary);

            Assertion.NotNull(summary, "Sumário de alta não encontrado.").Validate();

            var account = FactoryRepository.GetRepository(RepositoryEnum.Accounts).Get<Account>(idAccount);

            Assertion.NotNull(account, "Conta de usuário inválida.").Validate();

            var action = new Types<HistoricalActionType>().Get((short)actionType);

            summary.AddRecordToHistory(account, date, action, description);
            Summaries.Save(summary);
        }

        [ExceptionLogger]
        public override void FinalizeSummary(int summaryId)
        {
            Assertion.GreaterThan(summaryId, 0, "Sumario de alta invalido.").Validate();

            Summaries.FinalizeSummary(summaryId);
        }

        [ExceptionLogger]
        public override void ReOpenSummary(int summaryId)
        {
            Assertion.GreaterThan(summaryId, 0, "Sumario de alta invalido.").Validate();

            Summaries.ReOpenSummary(summaryId);
        }

        [ExceptionLogger]
        public override DischargeSummaryReportDTO GetReportData(int summaryId)
        {
            var summary = GetBy(summaryId);
            var dto = new DischargeSummaryReportDTO
            {
                //Patient
                Name = summary.Patient.Name,
                //Gender =

                MedicalRecord = summary.CodeMedicalRecord,
                //Summary
                Allergies = summary.Allergies,
                Procedures = summary.Procedures,
                Exams = summary.Exams,
                Medications = summary.Medications,
                //Professional
            };
            return dto;
        }
    }
}