using AutoMapper;
using EHR.Domain.Entities;

namespace EHR.UI.Models.Mappers
{
    public static class CidMapper
    {
        public static CidModel MapCidModelFrom(Cid cid)
        {
            Mapper.CreateMap<Cid, CidModel>();
            return Mapper.Map<Cid, CidModel>(cid);
        }
    }
}