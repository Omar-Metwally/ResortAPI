
using WebApplication5.Models.DominModels;
using ErrorOr;

namespace WebApplication5.Services.Owners;

public interface IOwnerService
{
    Task<ErrorOr<Created>> CreateOwner(Owner breakfast);
    Task<ErrorOr<Owner>> GetOwner(Guid id);
    Task<ErrorOr<IEnumerable<Owner>>> GetOwners();
    ErrorOr<UpsertedOwner> UpsertOwner(Owner breakfast);
    Task<ErrorOr<Deleted>> DeleteOwner(Guid id);
}