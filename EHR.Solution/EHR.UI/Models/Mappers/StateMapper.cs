using AutoMapper;
using EHR.CoreShared;

namespace EHR.UI.Models.Mappers
{
    public static class StateMapper
    {
        public static StateModel MapSpecialtyModelFrom(State state)
        {
            Mapper.CreateMap<State, StateModel>();
            var specialtyModel = Mapper.Map<State, StateModel>(state);
            return specialtyModel;
        }
    }
}