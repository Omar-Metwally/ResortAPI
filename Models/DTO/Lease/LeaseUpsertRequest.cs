
namespace WebApplication5.Models.DTO.Lease;

public class LeaseUpsertRequest
{
    public Guid Id { get; set; }

    public Guid ApartmentId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int Amount { get; set; }

    public bool IsDelivered { get; set; }
}
