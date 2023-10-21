using WebApplication5.Data;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using ServiceErrors.Owner;
using WebApplication5.Models.DominModels;

namespace WebApplication5.Services.Owners;

public class OwnerService : IOwnerService
{
    private static readonly Dictionary<Guid, Owner> _apartments = new();

    private readonly DataDBContext _context;

    public OwnerService(DataDBContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Created>> CreateOwner(Owner apartment)
    {
        await _context.Owners.AddAsync(apartment);
        await _context.SaveChangesAsync();

        return Result.Created;
    }

    public async Task<ErrorOr<Deleted>> DeleteOwner(Guid id)
    {
        
        var apartment = await _context.Owners.SingleOrDefaultAsync(x => x.Id == id);
        if (apartment != null)
        {
            _context.Owners.Remove(apartment);
            await _context.SaveChangesAsync();
            return Result.Deleted;
        }
        return Errors.Owner.NotFound;

        //_apartments.Remove(id);

    }

    public async Task<ErrorOr<Owner>> GetOwner(Guid id)
    {
        var apartment = await _context.Owners.SingleOrDefaultAsync(x => x.Id == id);
        if (apartment != null) return apartment;
        /*if (_apartments.TryGetValue(id, out var breakfast))
        {
            return breakfast;
        }*/

        return Errors.Owner.NotFound;
    }

    public async Task<ErrorOr<IEnumerable<Owner>>> GetOwners()
    {
        var owners = await _context.Owners.Include(x => x.Apartments).ToArrayAsync();
        return owners;
    }

    public ErrorOr<UpsertedOwner> UpsertOwner(Owner apartment)
    {
        var isNewlyCreated = !_apartments.ContainsKey(apartment.Id);
        _apartments[apartment.Id] = apartment;

        return new UpsertedOwner(isNewlyCreated);
    }
}
