using api.Data.SiteContentData;
using api.Dto;
using api.Entities;
using api.Mapper;
using api.Models;

namespace api.Controllers
{
    [ApiController]
    [Route("contact")]
    public class ContactController : ControllerBase
    {
        private readonly IRepository<ContactInfoDto> _repository;

        public ContactController(IRepository<ContactInfoDto> repository) =>
            _repository = repository;


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<ContactInfoDto>>> GetAllContactInfos()
        {
            try
            {
                var contacts = await _repository.GetAllAsync();
                return Ok(contacts);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ContactInfoDto>> GetByIdContactInfo(int id)
        {
            try
            {
                var contactInfo = await _repository.GetByIdAsync(id);
                return Ok(contactInfo);
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("name/{name}")]
        [AllowAnonymous]
        public async Task<ActionResult<ContactInfoDto>> GetByNameContactInfo(string name)
        {
            try
            {
                var contactInfo = await _repository.GetByNameAsync(name);
                return Ok(contactInfo);
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateContactInfo(ContactInfoCreateModel model)
        {
            var contactInfoDto = new ContactInfoDto
            {
                Id = 0,
                Phone = model.Phone,
                Email = model.Email,
                Address = model.Address
            };

            try
            {
                await _repository.AddAsync(contactInfoDto);
                return CreatedAtAction(nameof(GetByNameContactInfo), new { name = contactInfoDto.Phone }, contactInfoDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateContactInfo(ContactInfoDto contactInfoDto)
        {
            try
            {
                await _repository.UpdateAsync(contactInfoDto);
                return NoContent();
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteContactInfo(int id)
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