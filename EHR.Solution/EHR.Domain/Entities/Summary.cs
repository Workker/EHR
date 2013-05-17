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
        public virtual string Cpf { get; set; }
        public virtual DateTime? Date { get; set; }
        public virtual Admission Admission { get; set; }

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

        private IList<Drug> _drugs;
        public virtual IList<Drug> Drugs
        {
            get { return _drugs ?? (_drugs = new List<Drug>()); }
        }

        private IList<Hemotransfusion> _hemotransfusions;
        public virtual IList<Hemotransfusion> Hemotransfusions
        {
            get { return _hemotransfusions ?? (_hemotransfusions = new List<Hemotransfusion>()); }
        }

        public virtual IPatientDTO Patient { get; set; }
        public virtual ITreatmentDTO Treatment { get; set; }

        #endregion

        #region Procedure

        public virtual void CreateProcedure(int month, int day, int year, Tus tus)
        {
            Assertion.GreaterThan(month, 0, "Mês inválido.").Validate();
            Assertion.GreaterThan(day, 0, "Dia inválido.").Validate();
            Assertion.GreaterThan(year, 0, "Ano inválido.").Validate();
            Assertion.NotNull(tus, "Tus não informado.").Validate();
            Assertion.GreaterThan(tus.Id, short.Parse("0"), "Tus inválido.").Validate();

            //TODO: serviço para validar data
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

        public virtual void CreateAllergy(string theWitch, IList<AllergyType> types)
        {
            Assertion.GreaterThan(types.Count, 0, "Não foi selecionado um tipo de alergia.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(theWitch), "Motivo da alergia não informado.").Validate();

            var allergy = new Allergy(theWitch, types);

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

    }
}
