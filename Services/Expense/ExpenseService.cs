using WebApplication5.Data;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using ServiceErrors.Expense;
using WebApplication5.Models.DominModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Packaging;
using static ServiceErrors.Apartment.Errors;

namespace WebApplication5.Services.Expenses;

public class ExpenseService : IExpenseService
{
    private static readonly Dictionary<Guid, Expense> _expenses = new();

    private readonly DataDBContext _context;

    public ExpenseService(DataDBContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Created>> AddExpenses(Expense expense)
    {
            await _context.Expenses.AddAsync(expense);
            await _context.SaveChangesAsync();
            return Result.Created;
    }

    public async Task<ErrorOr<Deleted>> DeleteExpenses(Guid id)
    {
        
        var expenses = await _context.Expenses.Where(x => x.Id == id).ToArrayAsync();
        if (expenses != null)
        {
            _context.Expenses.RemoveRange(expenses);
            await _context.SaveChangesAsync();
            return Result.Deleted;
        }
        return Errors.Expense.NotFound;

    }

    public async Task<ErrorOr<IEnumerable<Expense>>> GetExpensesById(Guid id)
    {
        var expenses = await _context.Expenses.Where(x => x.Id == id).ToArrayAsync();
        if (expenses != null) return expenses;
        /*if (_expenses.TryGetValue(id, out var breakfast))
        {
            return breakfast;
        }*/

        return Errors.Expense.NotFound;
    }
    public async Task<ErrorOr<IEnumerable<Expense>>> GetExpensesByApartment(Guid apartmentId)
    {

        var expenses = await _context.Expenses.Where(x => x.Bills.Count > 0).Include(x => x.Bills.Where(x => x.ApartmentId == apartmentId)).ToArrayAsync();

        /*var expenses = await _context.Bills.Where(x => x.ApartmentId == apartmentId).DistinctBy(x => x.ApartmentId).Include(x => x.Expense).Select(x => new List<Expense>
        {
            new Expense{ Id = x.Id, ExpenseName = x.Expense.ExpenseName, Bills = x.Expense.Bills,}
        }).ToArrayAsync();*/
        //var expenses = await _context.Expenses.Where(x => x.Id == id).ToArrayAsync();
        //var expenses = await _context.Bills.Where(x => x.ApartmentId == apartmentId).DistinctBy(x => x.ApartmentId).ToArrayAsync();
        if (expenses != null) return expenses;
        /*if (_expenses.TryGetValue(id, out var breakfast))
        {
            return breakfast;
        }*/

        return Errors.Expense.NotFound;
    }

    public async Task<ErrorOr<IEnumerable<Expense>>> GetExpenses()
    {
        var expenses = await _context.Expenses.Include(x => x.Bills).ToArrayAsync();
        return expenses;
    }
    public async Task<ErrorOr<Updated>> UpsertExpenses(Expense expense)
    {

        /*if (expense.ApartmentId == Guid.Empty)
        {
            var apartmentIds = await _context.Apartments.Select(x => x.Id).ToArrayAsync();
            var expenses = new List<Expense>();
            foreach (var apartmentId in apartmentIds)
            {
                expenses.Add(new Expense { ApartmentId = apartmentId, Id = expense.Id, ExpenseName = expense.ExpenseName });
            };
            _context.Expenses.UpdateRange(expenses);
            await _context.SaveChangesAsync();
            return Result.Updated;
        }*/


        _context.Expenses.Update(expense);
        await _context.SaveChangesAsync();
        return Result.Updated;

        /*var isNewlyCreated = !_expenses.ContainsKey(expense.Id);
        _expenses[expense.Id] = expense;

        return new UpsertedExpense(isNewlyCreated);*/
    }
}
