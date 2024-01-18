using Microsoft.AspNetCore.Mvc;
using SolarApp.Entities;
using SolarApp.Services;

namespace SolarApp.Controllers;

[ApiController]
[Route("/projects")]
public class ProjectController : ControllerBase
{
    private readonly ProjectService _projectService;
    private readonly IrradiationService _irradiationService;

    public ProjectController(ProjectService projectService, IrradiationService irradiationService)
    {
        _projectService = projectService;
        _irradiationService = irradiationService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Project project) => await _projectService.CreateProject(project);

    [HttpGet]
    public async Task<IEnumerable<Project>> GetAll() => await _projectService.GetAll();

    [HttpGet("{projectId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid projectId) => await _projectService.GetById(projectId);

    [HttpPut("{projectId:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid projectId, [FromBody] Project project) => await _projectService.Update(projectId, project);

    [HttpDelete("{projectId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid projectId) => await _projectService.Delete(projectId);

}