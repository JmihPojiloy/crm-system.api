using api.Dto;
using api.Entities;
using api.Models;

namespace api.Mapper
{
    public class MainContentMapper : IMapper<MainContentDto, MainContent>
    {
        public MainContentDto MapToDto(MainContent value)
        {
            return new MainContentDto
            {
                Id = value.Id,
                HeaderContent = value.HeaderContent,
                MenuButtonText = value.MenuButtonText
            };
        }

        public MainContent MapToEntity(MainContentDto value)
        {
            return new MainContent
            {
                HeaderContent = value.HeaderContent,
                MenuButtonText = value.MenuButtonText
            };
        }
    }
}