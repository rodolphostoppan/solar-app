using Microsoft.AspNetCore.Mvc;
using SolarApp.Entities;
using SolarApp.Services;

namespace SolarApp.Controllers;

[ApiController]
[Route("/bills")]
public class BillController : ControllerBase
{
    private readonly BillService _billService;

    public BillController(BillService billService) => _billService = billService;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Bill bill) => await _billService.CreateBill(bill);

    [HttpGet]
    public async Task<IEnumerable<Bill>> GetAll() => await _billService.GetAll();

    [HttpGet("{billId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid billId) => await _billService.GetById(billId);

    [HttpPut("{billId:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid billId, [FromBody] Bill bill) => await _billService.Update(billId, bill);

    [HttpDelete("{billId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid billId) => await _billService.Delete(billId);
}

