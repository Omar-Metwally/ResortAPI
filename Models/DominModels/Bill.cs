using ErrorOr;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebApplication5.Models.DTO.Bill;

namespace WebApplication5.Models.DominModels;

public class Bill
{
    public Guid Id { get; set; }

    public Guid ExpenseId { get; set; }

    public Guid ApartmentId { get; set; }

    public int Amount { get; set; }

    public bool IsPaid { get; set; }

    public string? Description { get; set; }

    public DateTime? PaymentDate { get; set; }

    public DateTime DueDate { get; set; }
    [JsonIgnore]
    public virtual Expense Expense { get; set; } = null!;
    [JsonIgnore]
    public virtual Apartment Apartment { get; set; } = null!;

    public Bill() { }

    private Bill(
    Guid Id,
    Guid ExpenseId,
    Guid ApartmentId,
    int Amount,
    bool IsPaid,
    string Description,
    DateTime PaymentDate,
    DateTime DueDate)
    {
        this.Id = Id;
        this.ExpenseId = ExpenseId;
        this.ApartmentId = ApartmentId;
        this.Amount = Amount;
        this.IsPaid = IsPaid;
        this.Description = Description;
        this.PaymentDate = PaymentDate;
        this.DueDate = DueDate;
    }

    public static ErrorOr<Bill> Create(
    Guid ExpenseId,
    DateTime DueDate,
    int Amount,
    bool IsPaid,
    Guid? ApartmentId = null,
    Guid? Id = null,
    string? Description = null,
    DateTime? PaymentDate = null
    )
    {
        List<Error> errors = new();

        /*if (name.Length is < MinNameLength or > MaxNameLength)
        {
            errors.Add(Errors.Bill.InvalidName);
        }

        if (description.Length is < MinDescriptionLength or > MaxDescriptionLength)
        {
            errors.Add(Errors.Bill.InvalidDescription);
        }*/

        if (errors.Count > 0)
        {
            return errors;
        }

        return new Bill
            (
            Id ?? Guid.NewGuid(),
            ExpenseId,
            ApartmentId ?? Guid.Empty,
            Amount,
            IsPaid,
            Description ?? string.Empty,
            PaymentDate ?? DateTime.MinValue,
            DueDate
            );
    }

    public static ErrorOr<Bill> From(BillCreateRequest request)
    {
        return Create(
            request.ExpenseId,
            request.DueDate,
            request.Amount,
            request.IsPaid,
            request.ApartmentId,
            null, 
            request.Description);
    }

    public static ErrorOr<Bill> From(Guid id, BillUpsertRequest request)
    {
        return Create(
            request.ExpenseId,
            request.DueDate,
            request.Amount,
            request.IsPaid,
            request.ApartmentId,
            request.Id,
            request.Description,
            request.PaymentDate);
    }


}
