using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolarApp.Configs;
using SolarApp.Entities;

namespace SolarApp.Services;

public class BillService
{
    private readonly ContextConfig _contextConfig;

    public BillService(ContextConfig contextConfig)
    {
        _contextConfig = contextConfig;
    }

    public async Task<IActionResult> CreateBill(Bill bill)
    {
        var billExists = await _contextConfig.Bill.FirstOrDefaultAsync(prop => prop.Id == bill.Id);

        if (billExists is not null) return new ConflictResult();

        await _contextConfig.AddAsync(bill);
        await _contextConfig.SaveChangesAsync();

        return new CreatedResult("", bill);
    }

    public async Task<IActionResult> GetById(Guid billId)
    {
        var bill = await _contextConfig.Bill.FirstOrDefaultAsync(prop => prop.Id == billId);

        if (bill is null) return new NotFoundResult();

        return new OkObjectResult(bill);
    }

    public async Task<IEnumerable<Bill>> GetAll()
    {
        var bills = await _contextConfig.Bill.OrderBy(prop => prop.Project).ToListAsync();

        return bills;
    }

    public async Task<IActionResult> Delete(Guid billId)
    {
        var bill = await _contextConfig.Bill.FirstOrDefaultAsync(prop => prop.Id == billId);

        if (bill is null) return new NotFoundResult();

        _contextConfig.Remove(bill);
        await _contextConfig.SaveChangesAsync();

        return new NoContentResult();
    }

    public async Task<IActionResult> Update(Guid billId, Bill bill)
    {
        var billResult = await _contextConfig.Bill.FirstOrDefaultAsync(prop => prop.Id == billId);

        if (billResult is null) return new NotFoundResult();

        billResult.UC = bill.UC;
        billResult.Holder = bill.Holder;
        billResult.Location!.State = bill.Location!.State;
        billResult.Location.City = bill.Location.City;
        billResult.Location.Address = bill.Location.Address;
        billResult.Amount = bill.Amount;
        billResult.Consumption = bill.Consumption;
        billResult.Project = bill.Project;
        billResult.UpdatedAt = DateTime.Now;

        _contextConfig.Update(billResult);
        await _contextConfig.SaveChangesAsync();

        return new OkObjectResult(bill);
    }
}
