using WebApplication5.Data;
using WebApplication5.Services.Apartments; 
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Models.DominModels;
using WebApplication5.Models.DTO.Apartment;
using WebApplication5.Services.Owners;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication5.Controllers;


public class ApartmentController : ApiController
{
    private readonly IApartmentService _apartment;

    private readonly DataDBContext _context;

    public ApartmentController(DataDBContext context, IApartmentService apartment)
    {
        _context = context;

        _apartment = apartment;
    }

    [HttpPost]
    public async Task<IActionResult> CreateApartment(ApartmentCreateRequest request)
    {
        ErrorOr<Apartment> requestToApartmenttResult = Apartment.From(request);

        if (requestToApartmenttResult.IsError)
        {
            return Problem(requestToApartmenttResult.Errors);
        }

        var apartment = requestToApartmenttResult.Value;
        ErrorOr<Created> createApartmentResult = await _apartment.CreateApartment(apartment);

        return createApartmentResult.Match(
            created => CreatedAtGetApartment(apartment),
            errors => Problem(errors));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetApartment(Guid id)
    {
        ErrorOr<Apartment> getApartmentResult = await _apartment.GetApartment(id);

        return getApartmentResult.Match(
            apartment => Ok(MapApartmentResponse(apartment)),
            errors => Problem(errors));
    }

    [HttpGet]
    public async Task<IActionResult> GetApartments()
    {
        ErrorOr<IEnumerable<Apartment>> getApartmentResult = await _apartment.GetApartments();

        return Ok(getApartmentResult.Value);

        /*return getApartmentResult.MatchFirst(
            apartment => Ok(MapApartmentResponse(apartment)),
            errors => Problem(errors));*/
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertApartment(Guid id, ApartmentUpsertRequest request)
    {
        ErrorOr<Apartment> requestToApartmentResult = Apartment.From(id, request);

        if (requestToApartmentResult.IsError)
        {
            return Problem(requestToApartmentResult.Errors);
        }

        var apartment = requestToApartmentResult.Value;
        ErrorOr<UpsertedApartment> upsertApartmentResult = _apartment.UpsertApartment(apartment);

        return upsertApartmentResult.Match(
            upserted => upserted.IsNewlyCreated ? CreatedAtGetApartment(apartment) : NoContent(),
            errors => Problem(errors));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteApartment(Guid id)
    {
        ErrorOr<Deleted> deleteApartmentResult = await _apartment.DeleteApartment(id);

        return deleteApartmentResult.Match(
            deleted => NoContent(),
            errors => Problem(errors));
    }
    private static ApartmentGetRequest MapApartmentResponse(Apartment apartment)
    {

        return new ApartmentGetRequest(
            apartment.Id,
            apartment.BuildingNumber,
            apartment.ApartmentNumber,
            apartment.OwnerId,
            apartment.FloorNumber,
            apartment.Leases.Any(x => x.EndDate > DateTime.Now),
            apartment.Leases,
            apartment.Owner
            );
    }
    private CreatedAtActionResult CreatedAtGetApartment(Apartment apartment)
    {
        return CreatedAtAction(
            actionName: nameof(GetApartment),
            routeValues: new { id = apartment.Id },
            value: MapApartmentResponse(apartment));
    }

}
