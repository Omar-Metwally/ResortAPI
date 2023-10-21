using ErrorOr;
using WebApplication5.Models.DominModels;
using WebApplication5.Services.Apartments;

namespace WebApplication5.Services.Bills;

public interface IBillService
{
    Task<ErrorOr<Created>> AddBills(Bill bill);
    Task<ErrorOr<IEnumerable<Bill>>> GetBillsById(Guid id);
    Task<ErrorOr<IEnumerable<Bill>>> GetBillsByApartment(Guid apartmentId);
    Task<ErrorOr<IEnumerable<Bill>>> GetBills();
    Task<ErrorOr<Updated>> UpsertBills(Bill expense);
    Task<ErrorOr<Deleted>> DeleteBills(Guid id, Guid? apartmentId);

}
