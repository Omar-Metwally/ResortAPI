using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApplication5.Data;
using WebApplication5.Models.DominModels;
using WebApplication5.Models.DTO.Expense;
using WebApplication5.Services.Expenses;

namespace WebApplication5.Controllers;

public class ExpensesController : ApiController
{
    private readonly IExpenseService _expense;

    private readonly DataDBContext _context;

    public ExpensesController(DataDBContext context, IExpenseService expense)
    {
        _context = context;

        _expense = expense;
    }

    [HttpPost]
    public async Task<IActionResult> AddExpenses(ExpenseCreateRequest request)
    {
        ErrorOr<Expense> requestToExpensetResult = Expense.From(request);

        if (requestToExpensetResult.IsError)
        {
            return Problem(requestToExpensetResult.Errors);
        }

        var expense = requestToExpensetResult.Value;
        ErrorOr<Created> createExpenseResult = await _expense.AddExpenses(expense);

        return createExpenseResult.Match(
            created => CreatedAtGetExpense(expense),
            errors => Problem(errors));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetExpensesByApartment(Guid id)
    {
        ErrorOr<IEnumerable<Expense>> getExpenseResult = await _expense.GetExpensesByApartment(id);

        return getExpenseResult.Match(
            expenses => Ok(MapExpenseResponse(expenses)),
            errors => Problem(errors));
    }

    [HttpGet]
    public async Task<IActionResult> GetExpenses()
    {
        ErrorOr<IEnumerable<Expense>> getExpenseResult = await _expense.GetExpenses();

        return getExpenseResult.Match(
            expenses => Ok(MapExpenseResponse(expenses)),
            errors => Problem(errors));

    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpsertExpense(Guid id, ExpenseUpsertRequest request)
    {
        ErrorOr<Expense> requestToExpenseResult = Expense.From(id, request);

        if (requestToExpenseResult.IsError)
        {
            return Problem(requestToExpenseResult.Errors);
        }

        var expense = requestToExpenseResult.Value;
        ErrorOr<Updated> upsertExpenseResult = await _expense.UpsertExpenses(expense);


        return requestToExpenseResult.Match(
            created => NoContent(),
            errors => Problem(errors));

    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteExpense(Guid id)
    {
        ErrorOr<Deleted> deleteExpenseResult = await _expense.DeleteExpenses(id);

        return deleteExpenseResult.Match(
            deleted => NoContent(),
            errors => Problem(errors));
    }
    private static ExpenseGetRequest MapExpenseResponse(Expense expense)
    {
        return new ExpenseGetRequest(
            expense.Id,
            null,
            expense.ExpenseName,
            expense.Bills);
    }
    private static IEnumerable<ExpenseGetRequest> MapExpenseResponse(IEnumerable<Expense> expenses)
    {
        var ExpensesGetRequest = new List<ExpenseGetRequest>();
        foreach (var expense in expenses)
        {
            ExpensesGetRequest.Add(new ExpenseGetRequest ( expense.Id, null, expense.ExpenseName, expense.Bills ));
        }
        return ExpensesGetRequest;
    }
    private CreatedAtActionResult CreatedAtGetExpense(Expense expense)
    {
        return CreatedAtAction(
            actionName: nameof(GetExpenses),
            routeValues: new { id = expense.Id },
            value: MapExpenseResponse(expense));
    }
}
