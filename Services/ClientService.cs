using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolarApp.Configs;
using SolarApp.Entities;

namespace SolarApp.Services;

public class ClientService
{
    private readonly ContextConfig _contextConfig;

    public ClientService(ContextConfig contextConfig)
    {
        _contextConfig = contextConfig;
    }

    public async Task<IActionResult> CreateClient(Client client)
    {
        var clientExists = await _contextConfig.Client.FirstOrDefaultAsync(prop => prop.Id == client.Id);

        if (clientExists is not null) return new ConflictResult();

        await _contextConfig.AddAsync(client);
        await _contextConfig.SaveChangesAsync();

        return new CreatedResult("", client);
    }

    public async Task<IActionResult> GetById(Guid clientId)
    {
        var client = await _contextConfig.Client.FirstOrDefaultAsync(prop => prop.Id == clientId);

        if (client is null) return new NotFoundResult();

        return new OkObjectResult(client);
    }

    public async Task<IEnumerable<Client>> GetAll()
    {
        var clients = await _contextConfig.Client.OrderBy(prop => prop.Name).ToListAsync();

        return clients;
    }

    public async Task<IActionResult> Delete(Guid clientId)
    {
        var client = await _contextConfig.Client.FirstOrDefaultAsync(prop => prop.Id == clientId);

        if (client is null) return new NotFoundResult();

        _contextConfig.Remove(client);
        await _contextConfig.SaveChangesAsync();

        return new NoContentResult();
    }

    public async Task<IActionResult> Update(Guid clientId, Client client)
    {
        var clientResult = await _contextConfig.Client.FirstOrDefaultAsync(prop => prop.Id == clientId);

        if (clientResult is null) return new NotFoundResult();

        clientResult.Name = client.Name;
        clientResult.State = client.State;
        clientResult.City = client.City;
        clientResult.Address = client.Address;
        clientResult.UpdatedAt = DateTime.Now;

        _contextConfig.Update(clientResult);
        await _contextConfig.SaveChangesAsync();

        return new OkObjectResult(client);
    }
}
