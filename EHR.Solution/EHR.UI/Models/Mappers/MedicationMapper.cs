using AutoMapper;
using EHR.Domain.Entities;
using System.Collections.Generic;

namespace EHR.UI.Models.Mappers
{
    public static class MedicationMapper
    {
        public static List<MedicationModel> MapMedicationModelsFrom(IList<Medication> medications)
        {
            var medicationsModel = new List<MedicationModel>();
            foreach (var medication in medications)
            {
                var medicationModel = MapMedicationModelFrom(medication);
                medicationsModel.Add(medicationModel);
            }
            return medicationsModel;
        }

        public static MedicationModel MapMedicationModelFrom(Medication medication)
        {
            Mapper.CreateMap<Medication, MedicationModel>().ForMember(def => def.Def, so => so.Ignore());
            var medicationModel = Mapper.Map<Medication, MedicationModel>(medication);
            medicationModel.Def = DefMapper.MapDefModelFrom(medication.Def);
            return medicationModel;
        }
    }
}