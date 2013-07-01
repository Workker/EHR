using AutoMapper;
using EHR.Domain.Entities;
using EHR.UI.Models;

namespace EHR.UI.Mappers
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