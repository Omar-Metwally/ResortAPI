namespace WebApplication5.Models.DTO.Apartment;

public class ApartmentCreateRequest
{
    public string BuildingNumber { get; set; }

    public string ApartmentNumber { get; set; }

    public Guid OwnerId { get; set; }

    public string FloorNumber { get; set; }
}
