using ErrorOr;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebApplication5.Models.DTO.Apartment;

namespace WebApplication5.Models.DominModels;

public class Apartment
{
    public Guid Id { get; set; }

    public string BuildingNumber { get; set; }

    public string ApartmentNumber { get; set; }

    public Guid OwnerId { get; set; }

    public string FloorNumber { get; set; }

    public virtual ICollection<Lease> Leases { get; set; } = new List<Lease>();

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
    [JsonIgnore]
    public virtual Owner Owner { get; set; } = null!;

    public Apartment() { }

    private Apartment(
    Guid Id,
    string BuildingNumber,
    string ApartmentNumber,
    Guid OwnerId,
    string FloorNumber,
    ICollection<Lease> Leases,
    Owner Owner)
    {
        this.Id = Id;
        this.BuildingNumber = BuildingNumber;
        this.ApartmentNumber = ApartmentNumber;
        this.OwnerId = OwnerId;
        this.FloorNumber = FloorNumber;
        this.Leases = Leases;
        this.Owner = Owner;
    }

    public static ErrorOr<Apartment> Create(
        string BuildingNumber,
        string ApartmentNumber,
        Guid OwnerId,
        string FloorNumber,
        Guid? Id = null,
        ICollection<Lease>? Leases = null,
        Owner? Owner = null
        )
    {
        List<Error> errors = new();

        /*if (name.Length is < MinNameLength or > MaxNameLength)
        {
            errors.Add(Errors.Apartment.InvalidName);
        }

        if (description.Length is < MinDescriptionLength or > MaxDescriptionLength)
        {
            errors.Add(Errors.Apartment.InvalidDescription);
        }*/

        if (errors.Count > 0)
        {
            return errors;
        }

        return new Apartment
            (
            Id ?? Guid.NewGuid(),
            BuildingNumber,
            ApartmentNumber,
            OwnerId,
            FloorNumber,
            Leases ?? new List<Lease>(),
            Owner ?? null!
            ) ;
    }

    public static ErrorOr<Apartment> From(ApartmentCreateRequest request)
    {
        return Create(
            request.BuildingNumber,
            request.ApartmentNumber,
            request.OwnerId,
            request.FloorNumber
            );
    }

    public static ErrorOr<Apartment> From(Guid id, ApartmentUpsertRequest request)
    {
        return Create(
            request.BuildingNumber,
            request.ApartmentNumber,
            request.OwnerId,
            request.FloorNumber,
            id);
    }


}
