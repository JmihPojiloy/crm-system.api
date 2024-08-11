using api.Data;
using api.Dto;
using api.Models;


namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeadController : ControllerBase
    {
        private readonly ILeadsRepository _leadsRepository;

        public LeadController(ILeadsRepository leadsRepository) =>
            _leadsRepository = leadsRepository;

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<List<LeadDto>>> GetAll()
        {
            var result = await _leadsRepository.GetLeadsAsync();
            return Ok(result);
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<LeadDto>> GetById(int id)
        {
            try
            {
                var lead = await _leadsRepository.GetLeadAsync(id);
                return Ok(lead);
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("name/{name}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<LeadDto>> GetByName(string name)
        {
            try
            {
                var lead = await _leadsRepository.GetLeadAsync(name);
                return Ok(lead);
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(LeadCreateModel model)
        {
            LeadDto lead = new LeadDto
            {
                Id = 0,
                Date = DateTime.Now,
                ClientName = model.ClientName,
                ClientQuestion = model.ClientQuestion,
                Contact = model.Contact,
                Status = Enums.Status.Recived
            };

            try
            {
                await _leadsRepository.AddLeadAsync(lead);
                return CreatedAtAction(nameof(GetByName), new { name = lead.ClientName }, lead);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(int id, LeadDto lead)
        {
            try
            {
                if (id != lead.Id)
                    return BadRequest();

                await _leadsRepository.UpdateLeadAsync(lead);

                return NoContent();
            }
            catch (ArgumentNullException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var lead = await _leadsRepository.GetLeadAsync(id);

                if (lead is null)
                    return NotFound();

                await _leadsRepository.DeleteLeadAsync(id);

                return NoContent();
            }
            catch (ArgumentNullException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Save")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Save()
        {
            await _leadsRepository.SaveAsync();
            return NoContent();
        }
    }
}