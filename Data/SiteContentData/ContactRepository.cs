using api.Dto;
using api.Entities;
using api.Mapper;

namespace api.Data.SiteContentData
{
    public class ContactRepository : IRepository<ContactInfoDto>
    {
        private readonly LeadDb _context;
        private readonly IMapper<ContactInfoDto, ContactInfo> _mapper;

        public ContactRepository(LeadDb context, IMapper<ContactInfoDto, ContactInfo> mapper) =>
            (_context, _mapper) = (context, mapper);



        public async Task<IEnumerable<ContactInfoDto>> GetAllAsync()
        {
            var contacts = await _context.ContactInfos.ToListAsync();
            return contacts.Select(_mapper.MapToDto).ToList();
        }

        public async Task<ContactInfoDto> GetByIdAsync(int id)
        {
            var contactInfo = await _context.ContactInfos.FindAsync(new object[] { id });
            if (contactInfo == null) throw new ArgumentNullException(nameof(id));
            return _mapper.MapToDto(contactInfo);
        }

        public async Task<ContactInfoDto> GetByNameAsync(string name)
        {
            var contactInfo = await _context.ContactInfos.FirstOrDefaultAsync(
                c => c.Phone.ToLower() == name.ToLower());
            if (contactInfo == null) throw new ArgumentNullException(nameof(name));
            return _mapper.MapToDto(contactInfo);
        }

        public async Task AddAsync(ContactInfoDto value)
        {
            var contactInfo = _mapper.MapToEntity(value);
            await _context.ContactInfos.AddAsync(contactInfo);
            await SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var contactInfoFromDb = await _context.ContactInfos.FindAsync(new object[] { id });
            if (contactInfoFromDb == null) throw new ArgumentNullException(nameof(id));
            _context.ContactInfos.Remove(contactInfoFromDb);
            await SaveAsync();
        }

        public async Task UpdateAsync(ContactInfoDto value)
        {
            var contactInfoFromDb = await _context.ContactInfos.FindAsync(new object[] { value.Id });
            if (contactInfoFromDb == null) throw new ArgumentNullException(nameof(value.Id));
            contactInfoFromDb.Phone = value.Phone;
            contactInfoFromDb.Email = value.Email;
            contactInfoFromDb.Address = value.Address;

            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}