using api.Data.SiteContentData;
using api.Dto;
using api.Entities;
using api.Mapper;
using api.Models;

namespace api.Controllers
{
    [ApiController]
    [Route("content")]
    public class ContentController : ControllerBase
    {
        private readonly IRepository<MainContentDto> _repository;

        public ContentController(IRepository<MainContentDto> repository) =>
             _repository = repository;

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<MainContentDto>>> GetAllMainContent()
        {
            try
            {
                var mainContents = await _repository.GetAllAsync();
                return Ok(mainContents);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<MainContentDto>> GetByIdMainContent(int id)
        {
            try
            {
                var mainContent = await _repository.GetByIdAsync(id);
                return Ok(mainContent);
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("name/{name}")]
        [AllowAnonymous]
        public async Task<ActionResult<MainContentDto>> GetByNameMainContent(string name)
        {
            try
            {
                var mainContent = await _repository.GetByNameAsync(name);
                return Ok(mainContent);
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateMainContent(MainContentCreateModel model)
        {
            var mainContentDto = new MainContentDto
            {
                Id = 0,
                HeaderContent = model.HeaderContent,
                MenuButtonText = model.MenuButtonText
            };

            try
            {
                await _repository.AddAsync(mainContentDto);
                return CreatedAtAction(nameof(GetByNameMainContent), new { name = mainContentDto.HeaderContent }, mainContentDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateMainContent(MainContentDto mainContentDto)
        {
            try
            {
                await _repository.UpdateAsync(mainContentDto);
                return NoContent();
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteMainContent(int id)
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
    }
}