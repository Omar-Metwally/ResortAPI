using ErrorOr;
using ServiceErrors.Lease;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using WebApplication5.Models.DTO.Lease;

namespace WebApplication5.Models.DominModels;

public class Lease
{
    public Guid Id { get; set; }

    public Guid ApartmentId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int Amount { get; set; }

    public bool? IsDelivered { get; set; }
    [JsonIgnore]
    public virtual Apartment Apartment { get; set; } = null!;

    public Lease() { }

    private Lease(
    Guid Id,
    Guid ApartmentId,
    DateTime StartDate,
    DateTime EndDate,
    int Amount,
    bool? IsDelivered)
    {
        this.Id = Id;
        this.ApartmentId = ApartmentId;
        this.StartDate = StartDate;
        this.EndDate = EndDate;
        this.Amount = Amount;
        this.IsDelivered = IsDelivered;
    }

    public static ErrorOr<Lease> Create(
    Guid ApartmentId,
    DateTime StartDate,
    DateTime EndDate,
    int Amount,
    Guid? Id = null,
    bool? IsDelivered = null
    )
    {
        List<Error> errors = new();

        if (StartDate > EndDate)
        {
            errors.Add(Errors.Lease.WrongDate);
        }
        if (errors.Count > 0)
        {
            return errors;
        }

        return new Lease
            (
            Id ?? Guid.NewGuid(),
            ApartmentId,
            StartDate,
            EndDate,
            Amount,
            IsDelivered ?? false
            );
    }

    public static ErrorOr<Lease> From(LeaseCreateRequest request)
    {
        return Create(
            request.ApartmentId,
            request.StartDate,
            request.EndDate,
            request.Amount,
            null,
            request.IsDelivered);
    }

    public static ErrorOr<Lease> From(Guid id, LeaseUpsertRequest request)
    {
        return Create(
            request.ApartmentId,
            request.StartDate,
            request.EndDate,
            request.Amount,
            id,
            request.IsDelivered);
    }
}
