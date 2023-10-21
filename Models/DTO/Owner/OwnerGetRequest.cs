using WebApplication5.Models.DominModels;

namespace WebApplication5.Models.DTO.Owner;

public record OwnerGetRequest(
    Guid Id,
    string FirstName,
    string LastName,
    ICollection<DominModels.Apartment> Apartments);