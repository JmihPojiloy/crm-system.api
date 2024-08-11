using api.Entities;
using api.Dto;
using api.Mapper;
using api.Data.SiteContentData;
using api.Models;

namespace api.Controllers
{
    [ApiController]
    [Route("service")]
    public class ServiceController : ControllerBase
    {
        private readonly IRepository<ServiceDto> _repository;

        public ServiceController(IRepository<ServiceDto> repository) =>
            _repository = repository;


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<ServiceDto>>> GetAllServices()
        {
            var services = await _repository.GetAllAsync();
            return Ok(services);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceDto>> GetServiceById(int id)
        {
            try
            {
                var service = await _repository.GetByIdAsync(id);
                return Ok(service);
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("name/{name}")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceDto>> GetServiceByName(string name)
        {
            try
            {
                var service = await _repository.GetByNameAsync(name);
                return Ok(service);
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddService(ServiceCreateModel model)
        {
            var serviceDto = new ServiceDto
            {
                Id = 0,
                Title = model.Title,
                Description = model.Description,
            };

            try
            {
                await _repository.AddAsync(serviceDto);
                return CreatedAtAction(nameof(GetServiceByName), new { name = serviceDto.Title }, serviceDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteService(int id)
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
        public async Task<IActionResult> UpdateService(ServiceDto serviceDto)
        {
            try
            {
                await _repository.UpdateAsync(serviceDto);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}