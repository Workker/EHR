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
            var defObj = DefsRepository.Get<Def>(def);
            summary.CreateMedication((MedicationTypeEnum)medicationType, defObj, presentation, presentationType, dose, dosage, way, place, frequency, frequencyCase, duration);
            Summaries.Save(summary);
        }

        public override void RemoveMedication(int idSummary, int id)
        {
            var summary = Summaries.Get<Summary>(idSummary);
            summary.RemoveMedication(id);
            Summaries.Save(summary);
        }
    }
}