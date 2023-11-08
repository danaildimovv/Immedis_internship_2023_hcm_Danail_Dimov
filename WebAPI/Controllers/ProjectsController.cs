using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTO;
using WebAPI.Interfaces;
using WebAPI.Models;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        public ProjectsController(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {

            var projects = _mapper.Map<List<ProjectDTO>>(await _projectRepository.GetProjectsAsync());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (projects.Count == 0)
            {
                return NotFound("No projects");
            }



            return Ok(projects);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var project = _mapper.Map<ProjectDTO>(await _projectRepository.GetProjectByIdAsync(id));

            try
            {

                if (project == null)
                {
                    return NotFound("No project");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(project);
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateProject(ProjectDTO project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projectMap = _mapper.Map<Project>(project);
            var projectCreated = await _projectRepository.CreateProjectAsync(projectMap);

            if(! await projectCreated) 
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Success in creating");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, ProjectDTO project)
        {

            if (id != project.ProjectId)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projectMap = _mapper.Map<Project>(project);
            var projectUpdated = _projectRepository.UpdateProjectAsync(projectMap);

            if (!await projectUpdated)
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }
            return Ok("Updated");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _projectRepository.DeleteProjectAsync(project))
            {
                ModelState.AddModelError("", "Error");
            }
            return Ok("Succ");

        }
    }
}
