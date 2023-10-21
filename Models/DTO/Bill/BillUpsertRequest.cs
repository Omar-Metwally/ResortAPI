
namespace WebApplication5.Models.DTO.Bill;

public class BillUpsertRequest
{
    public Guid Id { get; set; }

    public Guid ExpenseId { get; set; }

    public Guid ApartmentId { get; set; }

    public int Amount { get; set; }

    public bool IsPaid { get; set; }

    public string? Description { get; set; }

    public DateTime? PaymentDate { get; set; }

    public DateTime DueDate { get; set; }
}
