using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolarApp.Configs;
using SolarApp.Entities;

namespace SolarApp.Services;

public class ProjectService
{
    private readonly ContextConfig _contextConfig;
    private readonly SizingService _sizingService;

    public ProjectService(ContextConfig contextConfig, SizingService sizingService)
    {
        _contextConfig = contextConfig;
        _sizingService = sizingService;
    }

    public async Task<IActionResult> CreateProject(Project createProject)
    {
        var projectResult = await _contextConfig.Project.Include(prop => prop.Client)
                                                        .Include(prop => prop.Bills)
                                                        .FirstOrDefaultAsync(prop => prop.Id == createProject.Id);

        if (projectResult is not null) return new ConflictResult();

        if (createProject.Bills is null) return new BadRequestResult();

        var irradiation = _sizingService.IrradiationAnnualAverageCalculator(createProject);

        var billConsumption = createProject.Bills.Select(prop => prop.Consumption).First();

        if (billConsumption != 0)
        {
            createProject.Generation = Math.Round(_sizingService.SizingByConsumption(createProject.Bills, createProject.ModulesPower, irradiation) / 12, 2);
        }
        else
        {
            createProject.Generation = Math.Round(_sizingService.SizingByAmount(createProject.Bills, createProject.ModulesPower, irradiation) / 12, 2);
        }

        createProject.Modules = _sizingService.NumberModules(createProject.Generation, createProject.ModulesPower, irradiation);
        createProject.Power = _sizingService.SystemPower(createProject.Modules, createProject.ModulesPower);

        await _contextConfig.AddAsync(createProject);
        await _contextConfig.SaveChangesAsync();

        return new CreatedResult("", createProject);
    }

    public async Task<IActionResult> GetById(Guid projectId)
    {
        var project = await _contextConfig.Project.FirstOrDefaultAsync(prop => prop.Id == projectId);

        if (project is null) return new NotFoundResult();

        return new OkObjectResult(project);
    }

    public async Task<IEnumerable<Project>> GetAll()
    {
        var projects = await _contextConfig.Project.OrderBy(prop => prop.Client).ToListAsync();

        return projects;
    }

    public async Task<IActionResult> Delete(Guid projectId)
    {
        var project = await _contextConfig.Project.FirstOrDefaultAsync(prop => prop.Id == projectId);

        if (project is null) return new NotFoundResult();

        _contextConfig.Remove(project);
        await _contextConfig.SaveChangesAsync();

        return new NoContentResult();
    }

    public async Task<IActionResult> Update(Guid projectId, Project project)
    {
        var projectResult = await _contextConfig.Project.FirstOrDefaultAsync(prop => prop.Id == projectId);

        if (projectResult is null) return new NotFoundResult();

        projectResult.Client = project.Client;
        projectResult.Bills = project.Bills;
        projectResult.Ground = project.Ground;
        projectResult.Power = project.Power;
        projectResult.Modules = project.Modules;
        projectResult.Inverter = project.Inverter;
        projectResult.Generation = project.Generation;
        projectResult.UpdatedAt = DateTime.Now;

        _contextConfig.Update(projectResult);
        await _contextConfig.SaveChangesAsync();

        return new OkObjectResult(project);
    }
}
