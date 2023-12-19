using Microsoft.AspNetCore.Mvc;
using SolarApp.Entities;
using SolarApp.Services;

namespace SolarApp.Controllers;

[ApiController]
[Route("/clients")]
public class ClientController : ControllerBase
{
    private readonly ClientService _clientService;

    public ClientController(ClientService clientService) => _clientService = clientService;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Client client) => await _clientService.CreateClient(client);

    [HttpGet]
    public async Task<IEnumerable<Client>> GetAll() => await _clientService.GetAll();

    [HttpGet("{clientId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid clientId) => await _clientService.GetById(clientId);

    [HttpPut("{clientId:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid clientId, [FromBody] Client client) => await _clientService.Update(clientId, client);

    [HttpDelete("{clientId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid clientId) => await _clientService.Delete(clientId);
}

