using ErrorOr;
using WebApplication5.Models.DominModels;
using WebApplication5.Services.Apartments;

namespace WebApplication5.Services.Leases;

public interface ILeaseService
{
    Task<ErrorOr<Created>> AddLeases(Lease lease);
    Task<ErrorOr<IEnumerable<Lease>>> GetLeasesById(Guid id);
    Task<ErrorOr<IEnumerable<Lease>>> GetLeasesByApartment(Guid apartmentId);
    Task<ErrorOr<IEnumerable<Lease>>> GetLeases();
    Task<ErrorOr<Updated>> UpsertLeases(Lease expense);
    Task<ErrorOr<Deleted>> DeleteLeases(Guid id);

}
