using api.Dto;
using api.Entities;
using api.Models;

namespace api.Mapper
{
    public class BlogMapper : IMapper<BlogPostDto, BlogPost>
    {
        public BlogPost MapToEntity(BlogPostDto value)
        {
            return new BlogPost
            {
                Title = value.Title,
                Content = value.Content,
                ImageUrl = value.ImageUrl,
                PublishDate = value.PublishDate
            };
        }

        public BlogPostDto MapToDto(BlogPost value)
        {
            return new BlogPostDto
            {
                Id = value.Id,
                Title = value.Title,
                Content = value.Content,
                ImageUrl = value.ImageUrl,
                PublishDate = value.PublishDate
            };
        }
    }
}