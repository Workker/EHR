using AutoMapper;
using EHR.CoreShared;
using System.Collections.Generic;

namespace EHR.UI.Models.Mappers
{
    public static class TUSSMapper
    {
        public static List<TUSSModel> MapTUSSModelsFrom(IList<TUSS> tusss)
        {
            var tussModels = new List<TUSSModel>();
            foreach (var tuss in tusss)
            {
                var tussModel = MapTUSSModelFrom(tuss);
                tussModels.Add(tussModel);
            }
            return tussModels;
        }

        public static TUSSModel MapTUSSModelFrom(TUSS tuss)
        {
            Mapper.CreateMap<TUSS, TUSSModel>();
            var tussModel = Mapper.Map<TUSS, TUSSModel>(tuss);
            return tussModel;
        }
    }
}