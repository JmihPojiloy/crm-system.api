using api.Dto;
using api.Entities;

namespace api.Mapper
{
    public class ServiceMapper : IMapper<ServiceDto, Service>
    {
        public ServiceDto MapToDto(Service value)
        {
            return new ServiceDto
            {
                Id = value.Id,
                Title = value.Title,
                Description = value.Description
            };
        }

        public Service MapToEntity(ServiceDto value)
        {
            return new Service
            {
                Id = value.Id,
                Title = value.Title,
                Description = value.Description
            };
        }
    }
}