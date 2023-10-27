
using WebApplication5.Models.DominModels;
using ErrorOr;

namespace WebApplication5.Services.Apartments;

public interface IApartmentService
{
    Task<ErrorOr<Created>> CreateApartment(Apartment breakfast);
    Task<ErrorOr<Apartment>> GetApartment(Guid id);
    Task<ErrorOr<IEnumerable<Apartment>>> GetApartmentsByOwnerID(Guid OwnerId);
    Task<ErrorOr<IEnumerable<Apartment>>> GetApartments();
    ErrorOr<UpsertedApartment> UpsertApartment(Apartment breakfast);
    Task<ErrorOr<Deleted>> DeleteApartment(Guid id);
}