
namespace WebApplication5.Models.DTO.Apartment;

public class ApartmentUpsertRequest
{
    public Guid Id { get; set; }

    public string BuildingNumber { get; set; }

    public string ApartmentNumber { get; set; }

    public Guid OwnerId { get; set; }

    public string FloorNumber { get; set; }
}
