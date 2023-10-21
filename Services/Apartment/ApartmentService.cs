using WebApplication5.Data;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using ServiceErrors.Apartment;
using WebApplication5.Models.DominModels;

namespace WebApplication5.Services.Apartments;

public class ApartmentService : IApartmentService
{
    private static readonly Dictionary<Guid, Apartment> _apartments = new();

    private readonly DataDBContext _context;

    public ApartmentService(DataDBContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Created>> CreateApartment(Apartment apartment)
    {

        if(await _context.Owners.AnyAsync(x => x.Id == apartment.OwnerId))
        {
            await _context.Apartments.AddAsync(apartment);
            await _context.SaveChangesAsync();
            return Result.Created;

        }
        return Errors.Apartment.OwnerDoesnNotExist;
    }

    public async Task<ErrorOr<Deleted>> DeleteApartment(Guid id)
    {
        
        var apartment = await _context.Apartments.SingleOrDefaultAsync(x => x.Id == id);
        if (apartment != null)
        {
            _context.Apartments.Remove(apartment);
            await _context.SaveChangesAsync();
            return Result.Deleted;
        }
        return Errors.Apartment.NotFound;

        //_apartments.Remove(id);

    }

    public async Task<ErrorOr<Apartment>> GetApartment(Guid id)
    {
        var apartment = await _context.Apartments.Include(x => x.Leases).Include(x => x.Bills).SingleOrDefaultAsync(x => x.Id == id);
        if (apartment != null) return apartment;
        /*if (_apartments.TryGetValue(id, out var breakfast))
        {
            return breakfast;
        }*/

        return Errors.Apartment.NotFound;
    }

    public async Task<ErrorOr<IEnumerable<Apartment>>> GetApartments()
    {
        var apartments = await _context.Apartments.Include(x => x.Owner).ToArrayAsync();
        return apartments;
    }

    public ErrorOr<UpsertedApartment> UpsertApartment(Apartment apartment)
    {
        var isNewlyCreated = !_apartments.ContainsKey(apartment.Id);
        _apartments[apartment.Id] = apartment;

        return new UpsertedApartment(isNewlyCreated);
    }
}
