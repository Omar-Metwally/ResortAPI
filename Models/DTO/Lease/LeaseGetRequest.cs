using WebApplication5.Models.DominModels;

namespace WebApplication5.Models.DTO.Lease;

public record LeaseGetRequest(
    Guid Id,
    Guid ApartmentId,
    DateTime StartDate,
    DateTime EndDate,
    int Amount,
    bool? IsDelivered);