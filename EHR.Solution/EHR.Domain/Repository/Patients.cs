using EHR.CoreShared;
using NHibernate;
using Workker.Framework.Domain;

namespace EHR.Domain.Repository
{
    public class Patients : BaseRepository
    {
        public Patients() { }
        public Patients(ISession session) : base(session) { }

        [ExceptionLogger]
        public virtual Patient GetBy(int id)
        {
            Assertion.GreaterThan(id, 0, "Paciente inválido.").Validate();

            var patient = base.Get<Patient>(id);

            Assertion.NotNull(patient, "Paciente inválido.").Validate();

            return patient;
        }
    }
}