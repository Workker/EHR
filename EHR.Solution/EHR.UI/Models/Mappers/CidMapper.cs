using AutoMapper;
using EHR.CoreShared.Entities;

namespace EHR.UI.Models.Mappers
{
    public static class CidMapper
    {
        public static CidModel MapCidModelFrom(CID cid)
        {
            Mapper.CreateMap<CID, CidModel>();
            return Mapper.Map<CID, CidModel>(cid);
        }
    }
}