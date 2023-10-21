using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Data;
using WebApplication5.Models.DominModels;
using WebApplication5.Models.DTO.Owner;
using WebApplication5.Services.Owners;

namespace WebApplication5.Controllers;

public class OwnerController : ApiController
{
    private readonly IOwnerService _owner;

    private readonly DataDBContext _context;

    public OwnerController(DataDBContext context, IOwnerService owner)
    {
        _context = context;

        _owner = owner;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOwner(OwnerCreateRequest request)
    {
        ErrorOr<Owner> requestToOwnertResult = Owner.From(request);

        if (requestToOwnertResult.IsError)
        {
            return Problem(requestToOwnertResult.Errors);
        }

        var owner = requestToOwnertResult.Value;
        ErrorOr<Created> createOwnerResult = await _owner.CreateOwner(owner);

        return createOwnerResult.Match(
            created => CreatedAtGetOwner(owner),
            errors => Problem(errors));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOwner(Guid id)
    {
        ErrorOr<Owner> getOwnerResult = await _owner.GetOwner(id);

        return getOwnerResult.Match(
            owner => Ok(MapOwnerResponse(owner)),
            errors => Problem(errors));
    }

    [HttpGet]
    public async Task<IActionResult> GetOwners()
    {
        ErrorOr<IEnumerable<Owner>> getOwnerResult = await _owner.GetOwners();

        return Ok(getOwnerResult.Value);

    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertOwner(Guid id, OwnerUpsertRequest request)
    {
        ErrorOr<Owner> requestToOwnerResult = Owner.From(id, request);

        if (requestToOwnerResult.IsError)
        {
            return Problem(requestToOwnerResult.Errors);
        }

        var owner = requestToOwnerResult.Value;
        ErrorOr<UpsertedOwner> upsertOwnerResult = _owner.UpsertOwner(owner);

        return upsertOwnerResult.Match(
            upserted => upserted.IsNewlyCreated ? CreatedAtGetOwner(owner) : NoContent(),
            errors => Problem(errors));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteOwner(Guid id)
    {
        ErrorOr<Deleted> deleteOwnerResult = await _owner.DeleteOwner(id);

        return deleteOwnerResult.Match(
            deleted => NoContent(),
            errors => Problem(errors));
    }
    private static OwnerGetRequest MapOwnerResponse(Owner owner)
    {
        return new OwnerGetRequest(
            owner.Id,
            owner.FirstName,
            owner.LastName,
            owner.Apartments);
    }
    private CreatedAtActionResult CreatedAtGetOwner(Owner owner)
    {
        return CreatedAtAction(
            actionName: nameof(GetOwner),
            routeValues: new { id = owner.Id },
            value: MapOwnerResponse(owner));
    }

}
