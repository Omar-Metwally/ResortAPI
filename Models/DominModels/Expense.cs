using ErrorOr;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using WebApplication5.Models.DTO.Expense;
using WebApplication5.Models.DTO.Owner;
using static ServiceErrors.Apartment.Errors;

namespace WebApplication5.Models.DominModels;

public class Expense
{
    public Guid Id { get; set; }

    public string ExpenseName { get; set; }

    public virtual ICollection<Bill>? Bills { get; set; } = new List<Bill>();

    public Expense() { }

    private Expense(
    Guid Id,
    string ExpenseName,
    ICollection<Bill> Bills)
    {
        this.Id = Id;
        this.ExpenseName = ExpenseName;
        this.Bills = Bills;
    }

    public static ErrorOr<Expense> Create(
        string ExpenseName,
        Guid? Id = null,
        ICollection<Bill>? Bills = null
        )
    {
        List<Error> errors = new();

        /*if (name.Length is < MinNameLength or > MaxNameLength)
        {
            errors.Add(Errors.Owner.InvalidName);
        }

        if (description.Length is < MinDescriptionLength or > MaxDescriptionLength)
        {
            errors.Add(Errors.Owner.InvalidDescription);
        }*/

        if (errors.Count > 0)
        {
            return errors;
        }

        return new Expense
            (
                Id ?? Guid.NewGuid(),
                ExpenseName,
                Bills ?? new List<Bill>()
            );
    }

    public static ErrorOr<Expense> From(ExpenseCreateRequest request)
    {
        return Create(
            request.ExpenseName);
    }

    public static ErrorOr<Expense> From(Guid id, ExpenseUpsertRequest request)
    {
        return Create(
            request.ExpenseName,
            id);
    }
}
