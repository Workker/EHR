using AutoMapper;
using EHR.CoreShared.Entities;
using EHR.Domain.Entities;
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
            hospitalModel.State = StateMapper.MapSpecialtyModelFrom(hospital.State);

            return hospitalModel;
        }
    }
}