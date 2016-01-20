using AutoMapper;
using EHR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EHR.UI.Models.Mappers
{
    public class PrescriptionsForServiceMapper
    {
        public static List<PrescriptionForServiceModel> MapDefModelsFrom(IList<PrescriptionForService> defs)
        {
            var defModels = new List<PrescriptionForServiceModel>();
            foreach (var def in defs)
            {
                var defModel = MapDefModelFrom(def);
                defModels.Add(defModel);
            }
            return defModels;
        }

        public static PrescriptionForServiceModel MapDefModelFrom(PrescriptionForService prescriptionForService)
        {
            Mapper.CreateMap<PrescriptionForService, PrescriptionForServiceModel>();
            var prescriptionForServiceModel = new PrescriptionForServiceModel();  //Mapper.Map<PrescriptionForService, PrescriptionForServiceModel>(prescriptionForService);
            prescriptionForServiceModel.Description = prescriptionForService.Observation;
            prescriptionForServiceModel.Dosage =(short) prescriptionForService.Dosage;
            prescriptionForServiceModel.Dose = prescriptionForService.Dose;
            prescriptionForServiceModel.Duration = prescriptionForService.Duration;
            prescriptionForServiceModel.Frequency =(short) prescriptionForService.Frequency;
            prescriptionForServiceModel.FrequencyCase =(short) prescriptionForService.FrequencyCase;
            prescriptionForServiceModel.Id = prescriptionForService.Id;
            prescriptionForServiceModel.Place = prescriptionForService.Place;
            prescriptionForServiceModel.Way = (short)prescriptionForService.Way;

            prescriptionForServiceModel.PrescriptionDateModel = new PrescriptionDateModel();
            prescriptionForServiceModel.PrescriptionDateModel.PrescriptionHighYear = prescriptionForService.OpenDate.Value.Year;
            prescriptionForServiceModel.PrescriptionDateModel.PrescriptionHighMonth = prescriptionForService.OpenDate.Value.Month;
            prescriptionForServiceModel.PrescriptionDateModel.PrescriptionHighDay = prescriptionForService.OpenDate.Value.Day;
            prescriptionForServiceModel.PrescriptionDateModel.PrescriptionHighHour = prescriptionForService.OpenDate.Value.Hour;
            prescriptionForServiceModel.PrescriptionDateModel.PrescriptionHighMinute = prescriptionForService.OpenDate.Value.Minute;

            prescriptionForServiceModel.Presentation = prescriptionForService.Presentation;
            prescriptionForServiceModel.PresentationType =(short) prescriptionForService.PresentationType;
            prescriptionForServiceModel.Type = (short) prescriptionForService.TypePrescription;
            prescriptionForServiceModel.TypePrescription = PrescriptionItemMapper.MapItemPrescriptionModelFrom(prescriptionForService.PrescriptionItem);

            
            return prescriptionForServiceModel;
        }
    }


    public class ItemPrescriptionsMapper
    {
        public static List<PrescriptionForServiceModel> MapDefModelsFrom(IList<PrescriptionForService> defs)
        {
            var defModels = new List<PrescriptionForServiceModel>();
            foreach (var def in defs)
            {
                var defModel = MapDefModelFrom(def);
                defModels.Add(defModel);
            }
            return defModels;
        }

        public static PrescriptionForServiceModel MapDefModelFrom(PrescriptionForService prescriptionForService)
        {
            Mapper.CreateMap<PrescriptionForService, PrescriptionForServiceModel>();
            var prescriptionForServiceModel = Mapper.Map<PrescriptionForService, PrescriptionForServiceModel>(prescriptionForService);
            prescriptionForServiceModel.Description = prescriptionForService.Observation;
            prescriptionForServiceModel.Dosage = (short)prescriptionForService.Dosage;
            prescriptionForServiceModel.Dose = prescriptionForService.Dose;
            prescriptionForServiceModel.Duration = prescriptionForService.Duration;
            prescriptionForServiceModel.Frequency = (short)prescriptionForService.Frequency;
            prescriptionForServiceModel.FrequencyCase = (short)prescriptionForService.FrequencyCase;
            prescriptionForServiceModel.Id = prescriptionForService.Id;
            prescriptionForServiceModel.Place = prescriptionForService.Place;
            prescriptionForServiceModel.Way = (short)prescriptionForService.Way;

            // prescriptionForServiceModel.PrescriptionDateModel = ;

            prescriptionForServiceModel.Presentation = prescriptionForService.Presentation;
            prescriptionForServiceModel.PresentationType = (short)prescriptionForService.PresentationType;
            prescriptionForServiceModel.Type = (short)prescriptionForService.TypePrescription;

            //prescriptionForServiceModel.TypePrescription = prescriptionForService.Place;


            return prescriptionForServiceModel;
        }
    }
}