using ErrorOr;
using WebApplication5.Models.DominModels;
using WebApplication5.Services.Apartments;

namespace WebApplication5.Services.Expenses;

public interface IExpenseService
{
    Task<ErrorOr<Created>> AddExpenses(Expense expense);
    Task<ErrorOr<IEnumerable<Expense>>> GetExpensesById(Guid id);
    Task<ErrorOr<IEnumerable<Expense>>> GetExpensesByApartment(Guid apartmentId);
    Task<ErrorOr<IEnumerable<Expense>>> GetExpenses();
    Task<ErrorOr<Updated>> UpsertExpenses(Expense expense);
    Task<ErrorOr<Deleted>> DeleteExpenses(Guid id);

}
