using WebApplication5.Data;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using ServiceErrors.Bill;
using WebApplication5.Models.DominModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Packaging;

namespace WebApplication5.Services.Bills;

public class BillService : IBillService
{
    private readonly DataDBContext _context;

    public BillService(DataDBContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Created>> AddBills(Bill bill)
    {
        if (await _context.Expenses.AnyAsync(x => x.Id == bill.ExpenseId))
        {
            var apartmentIds = await _context.Apartments.Select(x => x.Id).ToArrayAsync();

            if (bill.ApartmentId == Guid.Empty)
            {
                var bills = new List<Bill>();
                foreach (var apartmentId in apartmentIds)
                {
                    bills.Add(new Bill
                    {
                        ApartmentId = apartmentId,
                        ExpenseId = bill.ExpenseId,
                        Amount = bill.Amount,
                        Description = bill.Description,
                        DueDate = bill.DueDate,
                        IsPaid = false,
                        Id = bill.Id
                    });
                };
                await _context.Bills.AddRangeAsync(bills);
                await _context.SaveChangesAsync();
                return Result.Created;
            }
            if (apartmentIds.Contains(bill.ApartmentId))
            {
                await _context.Bills.AddAsync(bill);
                await _context.SaveChangesAsync();
                return Result.Created;
            }
        }
        return Errors.Bill.ApartmentOrExpenseDoesnNotExist;

    }

    public async Task<ErrorOr<Deleted>> DeleteBills(Guid id,Guid? apartmentId)
    {
        if (apartmentId == Guid.Empty)
        {
            var bills = await _context.Bills.Where(x => x.Id == id).ToArrayAsync();
            if (bills != null)
            {
                _context.Bills.RemoveRange(bills);
                await _context.SaveChangesAsync();
                return Result.Deleted;
            }
            return Errors.Bill.NotFound;
        }
        if(await _context.Apartments.AnyAsync(x => x.Id == apartmentId))
        {
            var bill = await _context.Bills.Where(x => x.Id == id && x.ApartmentId == apartmentId).ToArrayAsync();
            if (bill != null)
            {
                _context.Bills.RemoveRange(bill);
                await _context.SaveChangesAsync();
                return Result.Deleted;
            }
            return Errors.Bill.NotFound;
        }
        return Errors.Bill.NotFound;


    }

    public async Task<ErrorOr<IEnumerable<Bill>>> GetBillsById(Guid id)
    {
        var bills = await _context.Bills.Where(x => x.Id == id).ToArrayAsync();
        if (bills != null) return bills;

        return Errors.Bill.NotFound;
    }
    public async Task<ErrorOr<IEnumerable<Bill>>> GetBillsByApartment(Guid apartmentId)
    {
        var bills = await _context.Bills.Where(x => x.ApartmentId == apartmentId).ToArrayAsync();
        if (bills != null) return bills;


        return Errors.Bill.NotFound;
    }

    public async Task<ErrorOr<IEnumerable<Bill>>> GetBills()
    {
        var bills = await _context.Bills.ToArrayAsync();
        return bills;
    }
    public async Task<ErrorOr<Updated>> UpsertBills(Bill bill)
    {

        if (bill.ApartmentId == Guid.Empty)
        {
            var apartmentIds = await _context.Apartments.Select(x => x.Id).ToArrayAsync();
            var bills = new List<Bill>();
            foreach (var apartmentId in apartmentIds)
            {
                bills.Add(new Bill
                {
                    ApartmentId = apartmentId,
                    ExpenseId = bill.ExpenseId,
                    Amount = bill.Amount,
                    Description = bill.Description,
                    DueDate = bill.DueDate,
                    IsPaid = false,
                });
            };
            _context.Bills.UpdateRange(bills);
            await _context.SaveChangesAsync();
            return Result.Updated;
        }


        _context.Bills.Update(bill);
        await _context.SaveChangesAsync();
        return Result.Updated;

    }
}
