using WebApplication5.Models.DominModels;

namespace WebApplication5.Models.DTO.Expense;

public record ExpenseGetRequest(
    Guid Id,
    Guid? ApartmentId,
    string ExpenseName,
    ICollection<DominModels.Bill> Bills);