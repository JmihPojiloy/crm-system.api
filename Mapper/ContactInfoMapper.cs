using api.Entities;
using api.Dto;

namespace api.Mapper
{
    public class ContactInfoMapper : IMapper<ContactInfoDto, ContactInfo>
    {
        public ContactInfoDto MapToDto(ContactInfo value)
        {
            return new ContactInfoDto
            {
                Id = value.Id,
                Phone = value.Phone,
                Email = value.Email,
                Address = value.Address
            };
        }

        public ContactInfo MapToEntity(ContactInfoDto value)
        {
            return new ContactInfo
            {
                Phone = value.Phone,
                Email = value.Email,
                Address = value.Address
            };
        }
    }
}