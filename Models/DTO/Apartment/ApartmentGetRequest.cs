using WebApplication5.Models.DominModels;
using WebApplication5.Models.DTO.Lease;

namespace WebApplication5.Models.DTO.Apartment;

public record ApartmentGetRequest(
    Guid Id,
    string BuildingNumber,
    string ApartmentNumber,
    Guid OwnerId,
    string FloorNumber,
    ICollection<DominModels.Lease> Leases,
    DominModels.Owner Owner);