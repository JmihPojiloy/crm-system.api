using api.Dto;
using api.Entities;
using api.Mapper;

namespace api.Data.SiteContentData
{
    public class BlogRepository : IRepository<BlogPostDto>
    {
        private readonly LeadDb _context;
        private readonly IMapper<BlogPostDto, BlogPost> _mapper;

        public BlogRepository(LeadDb context, IMapper<BlogPostDto, BlogPost> mapper) =>
            (_context, _mapper) = (context, mapper);


        public async Task<IEnumerable<BlogPostDto>> GetAllAsync()
        {
            var blogPosts = await _context.BlogPost.ToListAsync();
            var blogPostsDtos = blogPosts.Select(_mapper.MapToDto).ToList();
            return blogPostsDtos;
        }

        public async Task<BlogPostDto> GetByIdAsync(int id)
        {
            var blogPost = await _context.BlogPost.FindAsync(new object[] { id });
            if (blogPost == null) throw new ArgumentNullException(nameof(id));
            var blogPostDto = _mapper.MapToDto(blogPost);
            return blogPostDto;
        }

        public async Task<BlogPostDto> GetByNameAsync(string name)
        {
            var blogPost = await _context.BlogPost.FirstOrDefaultAsync(
                b => b.Title.ToLower() == name.ToLower());
            if (blogPost == null) throw new ArgumentNullException(nameof(name));
            var blogPostDto = _mapper.MapToDto(blogPost);
            return blogPostDto;
        }

        public async Task AddAsync(BlogPostDto value)
        {
            var blogPost = _mapper.MapToEntity(value);
            await _context.BlogPost.AddAsync(blogPost);
            await SaveAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var blogPost = await _context.BlogPost.FindAsync(new object[] { id });
            if (blogPost == null) throw new ArgumentNullException(nameof(id));
            _context.BlogPost.Remove(blogPost);
            await SaveAsync();
        }

        public async Task UpdateAsync(BlogPostDto value)
        {
            var blogPostFromDb = await _context.BlogPost.FindAsync(new object[] { value.Id });
            if (blogPostFromDb == null) throw new ArgumentNullException(nameof(value));
            blogPostFromDb.Title = value.Title;
            blogPostFromDb.Content = value.Content;
            blogPostFromDb.ImageUrl = value.ImageUrl;
            blogPostFromDb.PublishDate = value.PublishDate;

            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}