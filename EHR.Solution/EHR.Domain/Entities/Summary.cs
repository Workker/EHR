using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EHR.CoreShared;
using EHR.Domain.Entities.Interfaces;
using System.Collections.Generic;
using Workker.Framework.Domain;

namespace EHR.Domain.Entities
{
    public class Summary : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual string Observation { get; set; }
        public virtual string Cpf { get; set; }
        public virtual DateTime Date { get; set; }

        public virtual Admission Admission { get; set; }
        public virtual IList<Allergy> Allergies { get; set; }

        private IList<Procedure> procedures;
        public virtual IList<Procedure> Procedures
        {
            get { return procedures ?? (procedures = new List<Procedure>()); }
        }

        public virtual IList<Hemotransfusion> Hemotransfusions { get; set; }
        public virtual IPatientDTO Patient { get; set; }
        public virtual ITreatmentDTO Treatment { get; set; }

        public virtual void RemoveProcedure(int id)
        {
            Assertion.GreaterThan(id, 0, "Id não informado.").Validate();

            var procedure = Procedures.FirstOrDefault(p => p.Id == id);

            Assertion.NotNull(procedure,"Procedimento não encontrado.").Validate();

            Procedures.Remove(procedure);

            Assertion.IsFalse(Procedures.Contains(procedure), "Procedimento não foi removido.").Validate();
        }

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
    }
}
