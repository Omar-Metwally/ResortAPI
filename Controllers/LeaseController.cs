using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Data;
using WebApplication5.Models.DominModels;
using WebApplication5.Models.DTO.Lease;
using WebApplication5.Services.Leases;

namespace WebApplication5.Controllers;

public class LeaseController : ApiController
{
    private readonly ILeaseService _lease;

    private readonly DataDBContext _context;

    public LeaseController(DataDBContext context, ILeaseService lease)
    {
        _context = context;

        _lease = lease;
    }

    [HttpPost]
    public async Task<IActionResult> AddLeases(LeaseCreateRequest request)
    {
        ErrorOr<Lease> requestToLeasetResult = Lease.From(request);

        if (requestToLeasetResult.IsError)
        {
            return Problem(requestToLeasetResult.Errors);
        }

        var lease = requestToLeasetResult.Value;
        ErrorOr<Created> createLeaseResult = await _lease.AddLeases(lease);

        return createLeaseResult.Match(
            created => CreatedAtGetLease(lease),
            errors => Problem(errors));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetLeasesByApartment(Guid id)
    {
        ErrorOr<IEnumerable<Lease>> getLeaseResult = await _lease.GetLeasesByApartment(id);

        return getLeaseResult.Match(
            leases => Ok(MapLeaseResponse(leases)),
            errors => Problem(errors));
    }

    [HttpGet]
    public async Task<IActionResult> GetLeases()
    {
        ErrorOr<IEnumerable<Lease>> getLeaseResult = await _lease.GetLeases();

        return getLeaseResult.Match(
            leases => Ok(MapLeaseResponse(leases)),
            errors => Problem(errors));

    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpsertLease(Guid id, LeaseUpsertRequest request)
    {
        ErrorOr<Lease> requestToLeaseResult = Lease.From(id, request);

        if (requestToLeaseResult.IsError)
        {
            return Problem(requestToLeaseResult.Errors);
        }

        var lease = requestToLeaseResult.Value;
        ErrorOr<Updated> upsertLeaseResult = await _lease.UpsertLeases(lease);


        return requestToLeaseResult.Match(
            created => NoContent(),
            errors => Problem(errors));

    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteLease(Guid id)
    {
        ErrorOr<Deleted> deleteLeaseResult = await _lease.DeleteLeases(id);

        return deleteLeaseResult.Match(
            deleted => NoContent(),
            errors => Problem(errors));
    }
    private static LeaseGetRequest MapLeaseResponse(Lease lease)
    {
        return new LeaseGetRequest(
            lease.Id,
            lease.ApartmentId,
            lease.StartDate,
            lease.EndDate,
            lease.Amount,
            lease.IsDelivered);
    }
    private static IEnumerable<LeaseGetRequest> MapLeaseResponse(IEnumerable<Lease> leases)
    {
        var LeasesGetRequest = new List<LeaseGetRequest>();
        foreach (var lease in leases)
        {
            LeasesGetRequest.Add(new LeaseGetRequest(
            lease.Id,
            lease.ApartmentId,
            lease.StartDate,
            lease.EndDate,
            lease.Amount,
            lease.IsDelivered));
        }
        return LeasesGetRequest;
    }
    private CreatedAtActionResult CreatedAtGetLease(Lease lease)
    {
        return CreatedAtAction(
            actionName: nameof(GetLeases),
            routeValues: new { id = lease.Id },
            value: MapLeaseResponse(lease));
    }
}
