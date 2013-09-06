﻿using AutoMapper;
using EHR.CoreShared;
using EHR.Domain.Entities;
using EHR.Infrastructure.Util;
using System.Collections.Generic;

namespace EHR.UI.Models.Mappers
{
    public static class HospitalMapper
    {
        public static List<HospitalModel> MapHospitalModelFrom(Account accountObject)
        {
            Mapper.CreateMap<Hospital, HospitalModel>();

            var hospitalModels = new List<HospitalModel> { Mapper.Map<Hospital, HospitalModel>(accountObject.Hospital) };

            return hospitalModels;
        }

        public static HospitalModel MapHospitalModelFrom(DbEnum hospital)
        {
            var hospitalModel = new HospitalModel
            {
                Name = EnumUtil.GetDescriptionFromEnumValue(hospital)
            };

            return hospitalModel;
        }
    }
}