using WebApplication5.Models.DominModels;

namespace WebApplication5.Models.DTO.Bill;

public record BillGetRequest(
    Guid Id,
    Guid ExpenseId,
    Guid ApartmentId,
    int Amount,
    bool IsPaid,
    string? Description,
    DateTime? PaymentDate,
    DateTime DueDate);
