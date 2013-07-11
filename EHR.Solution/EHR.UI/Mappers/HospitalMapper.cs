using System;
using AutoMapper;
using EHR.CoreShared;
using EHR.Domain.Entities;
using EHR.Domain.Util;
using EHR.UI.Models;
using System.Collections.Generic;

namespace EHR.UI.Mappers
{
    public static class HospitalMapper
    {
        public static List<HospitalModel> MapHospitalModelFrom(Account accountObject)
        {
            Mapper.CreateMap<Hospital, HospitalModel>();

            var hospitalModels = new List<HospitalModel>();

            foreach (var hospital in accountObject.Hospitals)
            {
                hospitalModels.Add(Mapper.Map<Hospital, HospitalModel>(hospital));
            }

            return hospitalModels;
        }

        public static HospitalModel MapHospitalModelFrom(DbEnum hospital)
        {
            var hospitalModel = new HospitalModel()
            {
                Name =
                    EnumUtil.GetDescriptionFromEnumValue(hospital)
            };

            return hospitalModel;
        }
    }
}