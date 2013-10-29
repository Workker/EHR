using AutoMapper;
using EHR.Domain.Entities;
using System.Collections.Generic;

namespace EHR.UI.Models.Mappers
{
    public static class ReasonsOfAdmissionMapper
    {
        public static ReasonOfAdmissionModel MapReasonOfAdmissionModelFrom(ReasonOfAdmission reasonOfAdmission)
        {
            Mapper.CreateMap<ReasonOfAdmission, ReasonOfAdmissionModel>();
            return Mapper.Map<ReasonOfAdmission, ReasonOfAdmissionModel>(reasonOfAdmission);
        }

        public static List<ReasonOfAdmissionModel> MapReasonsOfAdmissionModelFrom(IList<ReasonOfAdmission> reasonsOfAdmission)
        {
            var reasonsOfAdmissionModel = new List<ReasonOfAdmissionModel>();
            foreach (var reasonOfAdmission in reasonsOfAdmission)
            {
                var reasonOfAdmissionModel = MapReasonOfAdmissionModelFrom(reasonOfAdmission);
                reasonsOfAdmissionModel.Add(reasonOfAdmissionModel);
            }
            return reasonsOfAdmissionModel;
        }
    }
}