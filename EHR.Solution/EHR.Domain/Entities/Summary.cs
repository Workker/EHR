using EHR.CoreShared;
using EHR.Domain.Entities.Interfaces;
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
        public virtual Admission Admission { get; set; }
        public virtual Account Account { get; set; }
        public virtual string Mdr { get; set; }
        public virtual HighData HighData { get; set; }

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

        public virtual IPatientDTO Patient { get; set; }
        public virtual ITreatmentDTO Treatment { get; set; }

        private IList<View> _views;
        public virtual IList<View> Views
        {
            get { return _views ?? (_views = new List<View>()); }
        }

        #endregion

        #region LastVisitors

        public virtual void AddView(Account account, DateTime date)
        {
            Views.Add(new View() { Account = account, VisiteDate = date });
        }

        #endregion

        #region Procedure

        public virtual void CreateProcedure(int month, int day, int year, Tus tus)
        {
            Assertion.GreaterThan(month, 0, "Mês inválido.").Validate();
            Assertion.GreaterThan(day, 0, "Dia inválido.").Validate();
            Assertion.GreaterThan(year, 0, "Ano inválido.").Validate();
            Assertion.NotNull(tus, "Tus não informado.").Validate();
            Assertion.GreaterThan(tus.Id, short.Parse("0"), "Tus inválido.").Validate();

            var date = new DateTime(year, month, day);
            var procedure = new Procedure(tus, date);

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

            return new Allergy(theWitch, types);
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

        public virtual void CreateDiagnostic(DiagnosticType diagnosticType, Cid cid)
        {
            Assertion.NotNull(diagnosticType, "Tipo do diagnostico não informado.").Validate();
            Assertion.NotNull(cid, "Cid não informado").Validate();

            var diagnostic = new Diagnostic(diagnosticType, cid);

            Assertion.NotNull(diagnostic, "Diagnostico não foi criado corretamente.").Validate();

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

        public virtual void CreateMedication(MedicationTypeEnum type, Def def, string presentation, string presentationType,
            string dose, string dosage, string way, string place, string frequency, string frequencyCase, int duration)
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

            var medication = new Medication()
                                 {
                                     Type = type,
                                     Def = def,
                                     Presentation = presentation,
                                     PresentationType = presentationType,
                                     Dose = dose,
                                     Dosage = dosage,
                                     Way = way,
                                     Place = place,
                                     Frequency = frequency,
                                     FrequencyCase = frequencyCase,
                                     Duration = duration
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

        public virtual void CreateExam(ExamTypeEnum type, int day, int month, int year, string description)
        {
            Assertion.NotNull(Date, "Tipo de exame não informado.");
            Assertion.IsFalse(string.IsNullOrEmpty(description), "Descrição não informada.").Validate();

            var exam = new Exam()
            {
                Type = type,
                Date = new DateTime(year, month, day),
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

    }
}
