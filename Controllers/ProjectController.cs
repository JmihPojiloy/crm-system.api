using api.Data.SiteContentData;
using api.Dto;
using api.Entities;
using api.Mapper;
using api.Models;

namespace api.Controllers
{
    [ApiController]
    [Route("project")]
    public class ProjectController : ControllerBase
    {
        private readonly IRepository<ProjectDto> _repository;

        public ProjectController(IRepository<ProjectDto> repository) =>
            _repository = repository;

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<ProjectDto>>> GetAllProjects()
        {
            var projects = await _repository.GetAllAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProjectDto>> GetProjectById(int id)
        {
            try
            {
                var project = await _repository.GetByIdAsync(id);
                return Ok(project);
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("name/{name}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProjectDto>> GetProjectByName(string name)
        {
            try
            {
                var project = await _repository.GetByNameAsync(name);
                return Ok(project);
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddProject(ProjectCreateModel model)
        {
            ProjectDto projectDto = new ProjectDto
            {
                Id = 0,
                Title = model.Title,
                Description = model.Description,
                ImageUrl = model.ImageUrl
            };

            try
            {
                await _repository.AddAsync(projectDto);
                return CreatedAtAction(nameof(GetProjectByName), new { name = projectDto.Title }, projectDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return NoContent();
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateProject(ProjectDto projectDto)
        {
            try
            {
                await _repository.UpdateAsync(projectDto);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}