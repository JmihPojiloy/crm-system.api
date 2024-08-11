using api.Data.SiteContentData;
using api.Dto;
using api.Models;

namespace api.Controllers
{
    [ApiController]
    [Route("blog")]
    public class BlogController : ControllerBase
    {
        private readonly IRepository<BlogPostDto> _repository;

        public BlogController(IRepository<BlogPostDto> repository) =>
            _repository = repository;


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<BlogPostDto>>> GetAll()
        {
            IEnumerable<BlogPostDto> blogPosts = await _repository.GetAllAsync();
            return Ok(blogPosts);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<BlogPostDto>> GetById(int id)
        {
            try
            {
                var blogPost = await _repository.GetByIdAsync(id);
                return Ok(blogPost);
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("name/{name}")]
        [AllowAnonymous]
        public async Task<ActionResult<BlogPostDto>> GetByName(string name)
        {
            try
            {
                var blogPost = await _repository.GetByNameAsync(name);
                return Ok(blogPost);
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddBlogPost(BlogPostCreateModel model)
        {
            var blogPostDto = new BlogPostDto
            {
                Id = 0,
                Title = model.Title,
                Content = model.Content,
                ImageUrl = model.ImageUrl,
                PublishDate = DateTime.Now
            };

            try
            {
                await _repository.AddAsync(blogPostDto);
                return CreatedAtAction(nameof(GetByName), new { name = blogPostDto.Title }, blogPostDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteBlogPost(int id)
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
        public async Task<IActionResult> UpdateBlogPost(BlogPostDto blogPostDto)
        {
            try
            {
                await _repository.UpdateAsync(blogPostDto);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}