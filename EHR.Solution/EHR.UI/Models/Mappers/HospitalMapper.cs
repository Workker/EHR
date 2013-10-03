using AutoMapper;
using EHR.CoreShared;
using EHR.Domain.Entities;
using EHR.Infrastructure.Util;
using System.Collections.Generic;

namespace EHR.UI.Models.Mappers
{
    public static class HospitalMapper
    {
        public static IList<HospitalModel> MapHospitalModelFrom(IList<Hospital> hospitals)
        {
            var hospitalModels = new List<HospitalModel>();

            foreach (Hospital hospital in hospitals)
            {
                hospitalModels.Add(MapHospitalModelFrom(hospital));
            }

            return hospitalModels;
        }

        public static HospitalModel MapHospitalModelFrom(Account account)
        {
            return MapHospitalModelFrom(account.Hospital);
        }

        public static HospitalModel MapHospitalModelFrom(Hospital hospital)
        {
            Mapper.CreateMap<Hospital, HospitalModel>();

            var hospitalModel = Mapper.Map<Hospital, HospitalModel>(hospital);

            return hospitalModel;
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