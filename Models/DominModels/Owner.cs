using ErrorOr;
using System.ComponentModel.DataAnnotations;
using WebApplication5.Models.DTO.Owner;

namespace WebApplication5.Models.DominModels;

public class Owner
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public virtual ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();


    public Owner() { }

    private Owner(
    Guid Id,
    string FirstName,
    string LastName,
    ICollection<Apartment> Apartments)
    {
        this.Id = Id;
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.Apartments = Apartments;
    }

    public static ErrorOr<Owner> Create(
        string FirstName,
        string LastName,
        Guid? Id = null,
        ICollection<Apartment>? Apartments = null
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

        return new Owner
            (
            Id ?? Guid.NewGuid(),
            FirstName,
            LastName,
            Apartments ?? new List<Apartment>()
            );
    }

    public static ErrorOr<Owner> From(OwnerCreateRequest request)
    {
        return Create(
            request.FirstName,
            request.LastName);
    }

    public static ErrorOr<Owner> From(Guid id, OwnerUpsertRequest request)
    {
        return Create(
            request.FirstName,
            request.LastName,
            id);
    }
}
