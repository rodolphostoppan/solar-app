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
    private readonly IrradiationService _irradiationService;

    public ProjectService(ContextConfig contextConfig, SizingService sizingService, IrradiationService irradiationService)
    {
        _contextConfig = contextConfig;
        _sizingService = sizingService;
        _irradiationService = irradiationService;
    }

    public async Task<IActionResult> CreateProject(Project createProject)
    {
        var projectResult = await _contextConfig.Project.Include(prop => prop.Client)
                                                        .Include(prop => prop.Bills)
                                                        .FirstOrDefaultAsync(prop => prop.Id == createProject.Id);

        if (projectResult is not null) return new ConflictResult();

        if (createProject.Bills is null) return new BadRequestResult();

        var irradiation = await _irradiationService.GetCityIrradiation(createProject);

        var billsConsumption = _sizingService.CalculateBillsConsumption(createProject.Bills);

        createProject.Modules = _sizingService.NumberModules(billsConsumption, createProject.ModulesPower, irradiation!.Annual);
        createProject.Power = _sizingService.SystemPower(createProject.Modules, createProject.ModulesPower);
        createProject.Generation = _sizingService.CalculateAnnualGenerationAverage(createProject);
        createProject.MonthGeneration = _sizingService.CalculateMonthGeneration(createProject);


        await _contextConfig.AddAsync(createProject);
        await _contextConfig.SaveChangesAsync();

        return new CreatedResult("", createProject);
    }

    public async Task<IActionResult> GetById(Guid projectId)
    {
        var project = await _contextConfig.Project.Include(project => project.Client)
                                                  .Include(project => project.Bills)
                                                  .Include(project => project.Location)
                                                  .FirstOrDefaultAsync(prop => prop.Id == projectId);

        if (project is null) return new NotFoundResult();

        return new OkObjectResult(project);
    }

    public async Task<IEnumerable<Project>> GetAll()
    {
        var projects = await _contextConfig.Project.Include(project => project.Client)
                                                        .ThenInclude(client => client!.Location)
                                                   .Include(project => project.Bills)
                                                   .Include(project => project.Location)
                                                   .ToListAsync();

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
        var projectResult = await _contextConfig.Project.Include(project => project.Client)
                                                        .Include(project => project.Bills)
                                                        .Include(project => project.Location)
                                                        .FirstOrDefaultAsync(prop => prop.Id == projectId);

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
