using EHR.CoreShared;

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