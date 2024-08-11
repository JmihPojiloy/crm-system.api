using api.Dto;
using api.Entities;

namespace api.Mapper
{
    public class ProjectMapper : IMapper<ProjectDto, Project>
    {
        public ProjectDto MapToDto(Project value)
        {
            return new ProjectDto
            {
                Id = value.Id,
                Title = value.Title,
                Description = value.Description,
                ImageUrl = value.ImageUrl
            };
        }

        public Project MapToEntity(ProjectDto value)
        {
            return new Project
            {
                Id = value.Id,
                Title = value.Title,
                Description = value.Description,
                ImageUrl = value.ImageUrl
            };
        }
    }
}