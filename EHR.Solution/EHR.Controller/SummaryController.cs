using EHR.Domain.Entities;
using EHR.Domain.Repository;
using System;
using System.Collections.Generic;
using Workker.Framework.Domain;

namespace EHR.Controller
{
    public class SummaryController : EhrController
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

        [ExceptionLogger]
        public override void SaveMdr(int summaryId, string mdr)
        {
            Assertion.GreaterThan(summaryId, 0, "Sumário de alta não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(mdr), "Colonização de germes multiresitentes não informada.").Validate();

            var summary = Summaries.Get<Summary>(summaryId);

            summary.Mdr = mdr;
            Summaries.Save(summary);

            //todo: do
        }

        [ExceptionLogger]
        public override void SaveObservation(int summaryId, string observation)
        {
            Assertion.GreaterThan(summaryId, 0, "Sumário de alta não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(observation), "Observação não informada.").Validate();

            var summary = Summaries.Get<Summary>(summaryId);

            summary.Observation = observation;
            Summaries.Save(summary);

            //todo: do
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
        public override void SaveMedication(int idSummary, short medicationType, short def, string presentation,
            short presentationType, string dose, short dosage, short way, string place, short frequency, short frequencyCase, int duration)
        {
            Assertion.GreaterThan(idSummary, 0, "Sumário de alta inválido.").Validate();
            Assertion.GreaterThan((int)medicationType, 0, "Tipo de medicação inválido.").Validate();
            Assertion.GreaterThan((int)def, 0, "Medicamento não informado.");
            Assertion.IsFalse(string.IsNullOrEmpty(presentation), "Apresentação não informada.").Validate();
            Assertion.GreaterThan((int)presentationType, 0, "Tipo de apresentação não informado.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(dose), "Dose não informada.").Validate();
            Assertion.GreaterThan((int)dosage, 0, "Dosagem não informada.").Validate();
            Assertion.GreaterThan((int)way, 0, "Via informada.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(place), "Lugar não informado.").Validate();
            Assertion.GreaterThan((int)frequency, 0, "Frequencia não informada.").Validate();
            Assertion.GreaterThan((int)frequencyCase, 0, "Caso de frequencia não informado.").Validate();
            Assertion.GreaterThan(duration, 0, "Duração não informada.").Validate();

            var summary = Summaries.Get<Summary>(idSummary);
            var defObj = DefsRepository.GetById(def);

            summary.CreateMedication((MedicationTypeEnum)medicationType, defObj, presentation, presentationType, dose, dosage, way, place, frequency, frequencyCase, duration);
            Summaries.Save(summary);

            //todo: do
        }

        [ExceptionLogger]
        public override void RemoveMedication(int idSummary, int id)
        {
            Assertion.GreaterThan(idSummary, 0, "Sumário de alta não informado.").Validate();
            Assertion.GreaterThan(idSummary, 0, "Medicamento não informado.").Validate();

            var summary = Summaries.Get<Summary>(idSummary);

            summary.RemoveMedication(id);
            Summaries.Save(summary);

            //todo: do
        }

        [ExceptionLogger]
        public override void SaveExam(int idSummary, short type, DateTime date, string description)
        {
            Assertion.GreaterThan(idSummary, 0, "Sumário de alta inválido.").Validate();
            Assertion.GreaterThan((int)type, 0, "Tipo de exame inválido.").Validate();
            //todo: validade date
            Assertion.IsFalse(string.IsNullOrEmpty(description), "Dia inválido.").Validate();

            var summary = Summaries.Get<Summary>(idSummary);

            summary.CreateExam((ExamTypeEnum)type, date, description);
            Summaries.Save(summary);

            //todo: do
        }

        [ExceptionLogger]
        public override void RemoveExam(int summaryId, int examId)
        {
            Assertion.GreaterThan(summaryId, 0, "Duração não informada.").Validate();
            Assertion.GreaterThan(examId, 0, "Duração não informada.").Validate();

            var summary = Summaries.Get<Summary>(summaryId);
            summary.RemoveExam(examId);
            Summaries.Save(summary);

            //todo: do
        }

        [ExceptionLogger]
        public override void SaveHighData(int idSummary, IList<ComplementaryExam> complementaryExams, IList<int> complementaryExamDeleteds, short highType,
            short conditionOfThePatientAtHigh, short destinationOfThePatientAtDischarge,
           short orientationOfMultidisciplinaryTeamsMet, int termMedicalReviewAt, short specialtyId, DateTime prescribedHigh,
            string personWhoDeliveredTheSummary, DateTime deliveredDate)
        {
            Assertion.GreaterThan(idSummary, 0, "Sumário de alta inválido.").Validate();
            Assertion.NotNull(complementaryExams, "Lista de exames complementares está nula.").Validate();
            Assertion.NotNull(complementaryExamDeleteds, "Lista de exames complementares deletados está nula.").Validate();
            Assertion.GreaterThan((int)highType, 0, "Tipo de alta inválido.").Validate();
            Assertion.GreaterThan((int)conditionOfThePatientAtHigh, 0, "Condição de alta inválida.").Validate();
            Assertion.GreaterThan((int)destinationOfThePatientAtDischarge, 0, "Destino pós alta inválido.").Validate();
            Assertion.GreaterThan((int)orientationOfMultidisciplinaryTeamsMet, 0, "Opção de orientação de equipes multidisciplinares inválida.").Validate();
            Assertion.GreaterThan((int)specialtyId, 0, "Especialidade inválida.").Validate();
            Assertion.GreaterThan(prescribedHigh, DateTime.MinValue, "Data de alta inválida.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(personWhoDeliveredTheSummary), "Nome da pessoa a que o sumário foi entregue não foi informado.").Validate();
            Assertion.GreaterThan(deliveredDate, DateTime.MinValue, "Data de entrega inválida.").Validate();

            var summary = Summaries.Get<Summary>(idSummary);

            Assertion.NotNull(summary, "Sumário de alta não encontrado.").Validate();

            var specialty = new Types<Specialty>().Get(specialtyId);

            Assertion.NotNull(specialty, "Especialide não encontrada.").Validate();

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

            //todo: do
        }

        [ExceptionLogger]
        public override void AddView(int idSummary, int idAccount, DateTime date)
        {
            Assertion.GreaterThan(idSummary, 0, "Sumário de alta inválido.").Validate();
            Assertion.GreaterThan(idAccount, 0, "Conta de usuário inválida.").Validate();
            Assertion.GreaterThan(date, DateTime.MinValue, "Data inválida.").Validate();

            var summary = Summaries.Get<Summary>(idSummary);

            Assertion.NotNull(summary, "Sumário de alta não encontrado.").Validate();

            var account = FactoryRepository.GetRepository(RepositoryEnum.Accounts).Get<Account>(idAccount);
            ((Accounts)FactoryRepository.GetRepository(RepositoryEnum.Accounts)).Approve(account);

            Assertion.NotNull(account, "Conta de usuário inválida.").Validate();

            summary.AddView(account, date);
            Summaries.Save(summary);

            //todo: do
        }
    }
}