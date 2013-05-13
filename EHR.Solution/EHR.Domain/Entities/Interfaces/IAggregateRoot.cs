
namespace EHR.Domain.Entities.Interfaces
{
    public interface IAggregateRoot<T>
    {
        T Id { get; set; }
    }
}
