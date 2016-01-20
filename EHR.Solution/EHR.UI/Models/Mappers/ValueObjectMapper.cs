using AutoMapper;
using EHR.CoreShared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EHR.Domain.Entities;

namespace EHR.UI.Models.Mappers
{
    public static class PrescriptionItemMapper
    {
        public static List<ItemPrescriptionModel> MapItemsPrescriptionModelsFrom(IList<PrescriptionItem> valuesObjects)
        {
            var defModels = new List<ItemPrescriptionModel>();
            foreach (var prescriptionItem in valuesObjects)
            {
                var PrescriptionModel = MapItemPrescriptionModelFrom(prescriptionItem);
                defModels.Add(PrescriptionModel);
            }
            return defModels;
        }

        public static ItemPrescriptionModel MapItemPrescriptionModelFrom(PrescriptionItem prescriptionItem)
        {
            Mapper.CreateMap<PrescriptionItem, ItemPrescriptionModel>();
            var prescriptionModel = new ItemPrescriptionModel();//Mapper.Map<PrescriptionItem, ItemPrescriptionModel>(prescriptionItem);
            prescriptionModel.Code = prescriptionItem.code;
            prescriptionModel.Id = prescriptionItem.Id;
            prescriptionModel.Description = prescriptionItem.Description;
            prescriptionModel.Type = (short)prescriptionItem.PrescriptionItemType;

            return prescriptionModel;
        }
    }
}