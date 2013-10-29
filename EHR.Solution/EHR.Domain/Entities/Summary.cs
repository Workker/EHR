using EHR.CoreShared;
using EHR.CoreShared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Workker.Framework.Domain;

namespace EHR.Domain.Entities
{
    public class Summary : IAggregateRoot<int>
    {
        #region Properties

        public virtual int Id { get; set; }
        public virtual string Observation { get; set; }
        public virtual DateTime? EntryDateTreatment { get; set; }
        public virtual string CodeMedicalRecord { get; set; }
        public virtual DbEnum Hospital { get; set; }
        public virtual string Cpf { get; set; }
        public virtual DateTime? Date { get; set; }
        public virtual Account Account { get; set; }
        public virtual string Mdr { get; set; }
        public virtual DischargeData HighData { get; set; }
        public virtual IPatient Patient { get; set; }
        public virtual ITreatment Treatment { get; set; }
        public virtual string TreatmentId { get; set; }
        public virtual bool Finalized { get; set; }

        private IList<ReasonOfAdmission> _reasonOfAdmission;
        public virtual IList<ReasonOfAdmission> ReasonOfAdmission
        {
            get { return _reasonOfAdmission ?? (_reasonOfAdmission = new List<ReasonOfAdmission>()); }
        }

        private IList<Allergy> _allergies;
        public virtual IList<Allergy> Allergies
        {
            get { return _allergies ?? (_allergies = new List<Allergy>()); }
        }

        private IList<Diagnostic> _diagnostics;
        public virtual IList<Diagnostic> Diagnostics
        {
            get { return _diagnostics ?? (_diagnostics = new List<Diagnostic>()); }
        }

        private IList<Procedure> _procedures;
        public virtual IList<Procedure> Procedures
        {
            get { return _procedures ?? (_procedures = new List<Procedure>()); }
        }

        private IList<Medication> _medications;
        public virtual IList<Medication> Medications
        {
            get { return _medications ?? (_medications = new List<Medication>()); }
        }

        private IList<Exam> _exams;
        public virtual IList<Exam> Exams
        {
            get { return _exams ?? (_exams = new List<Exam>()); }
        }

        private IList<Hemotransfusion> _hemotransfusions;
        public virtual IList<Hemotransfusion> Hemotransfusions
        {
            get { return _hemotransfusions ?? (_hemotransfusions = new List<Hemotransfusion>()); }
        }

        private IList<HistoryRecord> _history;
        public virtual IList<HistoryRecord> History
        {
            get { return _history ?? (_history = new List<HistoryRecord>()); }
        }

        #endregion

        #region Procedure

        public virtual void CreateProcedure(int month, int day, int year, TUSS tus)
        {
            Assertion.GreaterThan(month, 0, "Mês inválido.").Validate();
            Assertion.GreaterThan(day, 0, "Dia inválido.").Validate();
            Assertion.GreaterThan(year, 0, "Ano inválido.").Validate();
            Assertion.NotNull(tus, "Tus não informado.").Validate();
            Assertion.GreaterThan(tus.Id, short.Parse("0"), "Tus inválido.").Validate();

            var date = new DateTime(year, month, day);
            var procedure = new Procedure { Date = date, Tus = tus };

            Procedures.Add(procedure);

            Assertion.IsTrue(Procedures.Contains(procedure), "Procedimento não foi inserido corretamente.").Validate();
        }

        public virtual void CreateProcedure(int month, int day, int year, string description)
        {
            Assertion.GreaterThan(month, 0, "Mês inválido.").Validate();
            Assertion.GreaterThan(day, 0, "Dia inválido.").Validate();
            Assertion.GreaterThan(year, 0, "Ano inválido.").Validate();

            var date = new DateTime(year, month, day);
            var procedure = new Procedure { Date = date, Description = description };

            Procedures.Add(procedure);

            Assertion.IsTrue(Procedures.Contains(procedure), "Procedimento não foi inserido corretamente.").Validate();
        }

        public virtual void RemoveProcedure(int id)
        {
            Assertion.GreaterThan(id, 0, "Id não informado.").Validate();

            var procedure = Procedures.FirstOrDefault(p => p.Id == id);

            Assertion.NotNull(procedure, "Procedimento não encontrado.").Validate();

            Procedures.Remove(procedure);

            Assertion.IsFalse(Procedures.Contains(procedure), "Procedimento não foi removido.").Validate();
        }

        #endregion

        #region Allergy

        public virtual Allergy CreateAllergy(string theWitch, IList<AllergyType> types)
        {
            Assertion.GreaterThan(types.Count, 0, "Não foi selecionado um tipo de alergia.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(theWitch), "Motivo da alergia não informado.").Validate();

            return new Allergy { TheWhich = theWitch, Types = types };
        }

        public virtual void AddAllergy(Allergy allergy)
        {
            Allergies.Add(allergy);

            Assertion.IsTrue(Allergies.Contains(allergy), "Alergia não foi inserida corretamente.").Validate();
        }

        public virtual void RemoveAllergy(int id)
        {
            Assertion.GreaterThan(id, 0, "Id não informado.").Validate();

            var allergy = Allergies.FirstOrDefault(p => p.Id == id);

            Assertion.NotNull(allergy, "Alergia não encontrada.").Validate();

            Allergies.Remove(allergy);

            Assertion.IsFalse(Allergies.Contains(allergy), "Alergia não foi removida.").Validate();
        }

        #endregion

        #region Diagnostic

        public virtual void CreateDiagnostic(DiagnosticType diagnosticType, CID cid)
        {
            Assertion.NotNull(diagnosticType, "Tipo do diagnostico não informado.").Validate();
            Assertion.NotNull(cid, "CID não informado").Validate();

            var diagnostic = new Diagnostic { Cid = cid, Type = diagnosticType };

            Diagnostics.Add(diagnostic);

            Assertion.IsTrue(Diagnostics.Contains(diagnostic), "Diagnostico não foi atribuido corretamente ao sumário.").Validate();

        }

        public virtual void CreateDiagnostic(DiagnosticType diagnosticType, string description)
        {
            Assertion.NotNull(diagnosticType, "Tipo do diagnostico não informado.").Validate();
            Assertion.NotNull(description, "CID não informado.").Validate();

            var diagnostic = new Diagnostic { Type = diagnosticType, Description = description };

            Diagnostics.Add(diagnostic);

            Assertion.IsTrue(Diagnostics.Contains(diagnostic), "Diagnostico não foi atribuido corretamente ao sumário.").Validate();
        }

        public virtual void RemoveDiagnostic(int id)
        {
            Assertion.GreaterThan(id, 0, "Id não informado.").Validate();

            var diagnostic = Diagnostics.FirstOrDefault(p => p.Id == id);

            Assertion.NotNull(diagnostic, "Diagnostico não encontrado.").Validate();

            Diagnostics.Remove(diagnostic);

            Assertion.IsFalse(Diagnostics.Contains(diagnostic), "Diagnostico não foi removido.").Validate();
        }

        #endregion

        #region Hemotransfusion

        public virtual void CreateHemotransfusion(HemotransfusionType hemotransfusionType, List<ReactionType> reactionTypes)
        {
            Assertion.NotNull(hemotransfusionType, "tipo da homotransfusão não informado.").Validate();

            var hemotransfusion = new Hemotransfusion { Type = hemotransfusionType, Reactions = reactionTypes };

            Hemotransfusions.Add(hemotransfusion);

            Assertion.IsTrue(Hemotransfusions.Contains(hemotransfusion), "Hemotransfusão não foi inserida corretamente.").Validate();
        }

        public virtual void RemoveHemotransfusion(int id)
        {
            Assertion.GreaterThan(id, 0, "Id não informado.").Validate();

            var hemotransfusion = Hemotransfusions.FirstOrDefault(p => p.Id == id);

            Assertion.NotNull(hemotransfusion, "Hemotransfusão não encontrada.").Validate();

            Hemotransfusions.Remove(hemotransfusion);

            Assertion.IsFalse(Hemotransfusions.Contains(hemotransfusion), "Hemotransfusão não foi removida.").Validate();
        }

        #endregion

        #region Medication

        public virtual void CreateMedication(MedicationTypeEnum type, DEF def, string description, string presentation, short presentationType,
            string dose, short dosage, short way, string place, short frequency, short frequencyCase, int duration)
        {
            Assertion.NotNull(def, "Medicamento não informado.");
            Assertion.GreaterThan(duration, 0, "Duração não informada.").Validate();

            if (((short)type) == 3)
            {
                Assertion.IsFalse(string.IsNullOrEmpty(presentation), "Apresentação não informada.").Validate();
                Assertion.GreaterThan((int)presentationType, 0, "Tipo de apresentação não informado.").Validate();
                Assertion.IsFalse(string.IsNullOrEmpty(dose), "Dose não informada.").Validate();
                Assertion.GreaterThan((int)dosage, 0, "Dosagem não informada.").Validate();
                Assertion.GreaterThan((int)way, 0, "Via informada.").Validate();
                Assertion.GreaterThan((int)frequency, 0, "Frequencia não informada.").Validate();
            }

            var medication = new Medication
                                 {
                                     Type = type,
                                     Def = def,
                                     Presentation = presentation,
                                     PresentationType = (PresentationTypeEnum)presentationType,
                                     Dose = dose,
                                     Dosage = (DosageEnum)dosage,
                                     Way = (WayEnum)way,
                                     Place = place,
                                     Frequency = (FrequencyEnum)frequency,
                                     FrequencyCase = (FrequencyCaseEnum)frequencyCase,
                                     Duration = duration,
                                     Description = description
                                 };

            Medications.Add(medication);

            Assertion.IsTrue(Medications.Contains(medication), "Medicamento não foi inserido.").Validate();
        }

        public virtual void CreateMedication(MedicationTypeEnum type, string description, string presentation, short presentationType,
            string dose, short dosage, short way, string place, short frequency, short frequencyCase, int duration)
        {
            Assertion.GreaterThan(duration, 0, "Duração não informada.").Validate();

            if (((short)type) == 3)
            {
                Assertion.IsFalse(string.IsNullOrEmpty(presentation), "Apresentação não informada.").Validate();
                Assertion.GreaterThan((int)presentationType, 0, "Tipo de apresentação não informado.").Validate();
                Assertion.IsFalse(string.IsNullOrEmpty(dose), "Dose não informada.").Validate();
                Assertion.GreaterThan((int)dosage, 0, "Dosagem não informada.").Validate();
                Assertion.GreaterThan((int)way, 0, "Via informada.").Validate();
                Assertion.GreaterThan((int)frequency, 0, "Frequencia não informada.").Validate();
            }

            var medication = new Medication
            {
                Type = type,
                Presentation = presentation,
                PresentationType = (PresentationTypeEnum)presentationType,
                Dose = dose,
                Dosage = (DosageEnum)dosage,
                Way = (WayEnum)way,
                Place = place,
                Frequency = (FrequencyEnum)frequency,
                FrequencyCase = (FrequencyCaseEnum)frequencyCase,
                Duration = duration,
                Description = description
            };

            Medications.Add(medication);

            Assertion.IsTrue(Medications.Contains(medication), "Medicamento não foi inserido.").Validate();
        }

        public virtual void RemoveMedication(int id)
        {
            Assertion.GreaterThan(id, 0, "Id não informado.").Validate();

            var medication = Medications.FirstOrDefault(m => m.Id == id);

            Assertion.NotNull(medication, "Medicamento não encontrado.").Validate();

            Medications.Remove(medication);

            Assertion.IsFalse(Medications.Contains(medication), "Medicamento não foi removido.").Validate();
        }

        #endregion

        #region Exam

        public virtual void CreateExam(ExamTypeEnum type, DateTime date, string description)
        {
            Assertion.NotNull(Date, "Tipo de exame não informado.");
            Assertion.IsFalse(string.IsNullOrEmpty(description), "Descrição não informada.").Validate();

            var exam = new Exam
            {
                Type = type,
                Date = date,
                Description = description
            };

            Exams.Add(exam);

            Assertion.IsTrue(Exams.Contains(exam), "Exame não foi inserido.").Validate();
        }

        public virtual void RemoveExam(int id)
        {
            Assertion.GreaterThan(id, 0, "Id não informado.").Validate();

            var exam = Exams.FirstOrDefault(m => m.Id == id);

            Assertion.NotNull(exam, "Exame não encontrado.").Validate();

            Exams.Remove(exam);

            Assertion.IsFalse(Exams.Contains(exam), "Exame não foi removido.").Validate();
        }

        #endregion

        #region History

        public virtual void AddRecordToHistory(Account account, DateTime date, HistoricalActionType action, string description)
        {
            var record = new HistoryRecord
                             {
                                 Account = account,
                                 Action = action,
                                 Date = date,
                                 Description = description
                             };

            History.Add(record);
        }

        #endregion
    }
}