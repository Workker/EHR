using EHR.CoreShared.Entities;

namespace EHR.Domain.Mapping
{
    public class StateMap : ValueObjectMap<State>
    {
        StateMap()
        {
            Map(s => s.Acronym);
        }
    }
}