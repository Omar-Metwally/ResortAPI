using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Data;
using WebApplication5.Models.DominModels;
using WebApplication5.Models.DTO.Bill;
using WebApplication5.Services.Bills;

namespace WebApplication5.Controllers;

public class BillController : ApiController
{
    private readonly IBillService _bill;

    private readonly DataDBContext _context;

    public BillController(DataDBContext context, IBillService bill)
    {
        _context = context;

        _bill = bill;
    }

    [HttpPost]
    public async Task<IActionResult> AddBills(BillCreateRequest request)
    {
        ErrorOr<Bill> requestToBilltResult = Bill.From(request);

        if (requestToBilltResult.IsError)
        {
            return Problem(requestToBilltResult.Errors);
        }

        var bill = requestToBilltResult.Value;
        ErrorOr<Created> createBillResult = await _bill.AddBills(bill);

        return createBillResult.Match(
            created => CreatedAtGetBill(bill),
            errors => Problem(errors));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetBillsByApartment(Guid id)
    {
        ErrorOr<IEnumerable<Bill>> getBillResult = await _bill.GetBillsByApartment(id);

        return getBillResult.Match(
            bills => Ok(MapBillResponse(bills)),
            errors => Problem(errors));
    }

    [HttpGet]
    public async Task<IActionResult> GetBills()
    {
        ErrorOr<IEnumerable<Bill>> getBillResult = await _bill.GetBills();

        return getBillResult.Match(
            bills => Ok(MapBillResponse(bills)),
            errors => Problem(errors));

    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpsertBill(Guid id, BillUpsertRequest request)
    {
        ErrorOr<Bill> requestToBillResult = Bill.From(id, request);

        if (requestToBillResult.IsError)
        {
            return Problem(requestToBillResult.Errors);
        }

        var bill = requestToBillResult.Value;
        ErrorOr<Updated> upsertBillResult = await _bill.UpsertBills(bill);


        return requestToBillResult.Match(
            created => NoContent(),
            errors => Problem(errors));

    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteBill(Guid id, Guid? apartmentId)
    {
        if(apartmentId == null) apartmentId = Guid.Empty;
        ErrorOr<Deleted> deleteBillResult = await _bill.DeleteBills(id, apartmentId);

        return deleteBillResult.Match(
            deleted => NoContent(),
            errors => Problem(errors));
    }
    private static BillGetRequest MapBillResponse(Bill bill)
    {
        return new BillGetRequest(
            bill.Id,
            bill.ExpenseId,
            bill.ApartmentId,
            bill.Amount,
            bill.IsPaid,
            bill.Description,
            bill.PaymentDate,
            bill.DueDate);
    }
    private static IEnumerable<BillGetRequest> MapBillResponse(IEnumerable<Bill> bills)
    {
        var BillsGetRequest = new List<BillGetRequest>();
        foreach (var bill in bills)
        {
            BillsGetRequest.Add(new BillGetRequest(
            bill.Id,
            bill.ExpenseId,
            bill.ApartmentId,
            bill.Amount,
            bill.IsPaid,
            bill.Description,
            bill.PaymentDate,
            bill.DueDate));
        }
        return BillsGetRequest;
    }
    private CreatedAtActionResult CreatedAtGetBill(Bill bill)
    {
        return CreatedAtAction(
            actionName: nameof(GetBills),
            routeValues: new { id = bill.Id },
            value: MapBillResponse(bill));
    }

}
