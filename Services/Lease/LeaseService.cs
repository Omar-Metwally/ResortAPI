using WebApplication5.Data;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using ServiceErrors.Lease;
using WebApplication5.Models.DominModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Packaging;

namespace WebApplication5.Services.Leases;

public class LeaseService : ILeaseService
{
    private readonly DataDBContext _context;

    public LeaseService(DataDBContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Created>> AddLeases(Lease lease)
    {
        if (await _context.Apartments.AnyAsync(x => x.Id == lease.ApartmentId))
        {
            if(await _context.Leases.Where(x => x.Id == lease.ApartmentId).AllAsync(x => x.EndDate < lease.StartDate))
            {
                await _context.Leases.AddAsync(lease);
                await _context.SaveChangesAsync();
                return Result.Created;
            }
            return Errors.Lease.UnderLease;
        }
        return Errors.Lease.ApartmentDoesnNotExist;

    }

    public async Task<ErrorOr<Deleted>> DeleteLeases(Guid id)
    {

        var lease = await _context.Leases.Where(x => x.Id == id).SingleOrDefaultAsync();
        if (lease != null)
        {
            _context.Leases.Remove(lease);
            await _context.SaveChangesAsync();
            return Result.Deleted;
        }
        return Errors.Lease.NotFound;

    }

    public async Task<ErrorOr<IEnumerable<Lease>>> GetLeasesById(Guid id)
    {
        var leases = await _context.Leases.Where(x => x.Id == id).ToArrayAsync();
        if (leases != null) return leases;

        return Errors.Lease.NotFound;
    }
    public async Task<ErrorOr<IEnumerable<Lease>>> GetLeasesByApartment(Guid apartmentId)
    {
        var leases = await _context.Leases.Where(x => x.ApartmentId == apartmentId).ToArrayAsync();
        if (leases != null) return leases;


        return Errors.Lease.NotFound;
    }

    public async Task<ErrorOr<IEnumerable<Lease>>> GetLeases()
    {
        var leases = await _context.Leases.ToArrayAsync();
        return leases;
    }
    public async Task<ErrorOr<Updated>> UpsertLeases(Lease lease)
    {

        if (await _context.Leases.AnyAsync(x => x.Id == lease.Id))
        {
            _context.Leases.Update(lease);
            await _context.SaveChangesAsync();
            return Result.Updated;
        }
        return Errors.Lease.NotFound;
    }
}
