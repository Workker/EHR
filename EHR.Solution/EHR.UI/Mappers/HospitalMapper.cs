using AutoMapper;
using EHR.Domain.Entities;
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
    }
}