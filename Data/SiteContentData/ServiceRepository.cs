using api.Entities;
using api.Dto;
using api.Mapper;

namespace api.Data.SiteContentData
{
    public class ServiceRepository : IRepository<ServiceDto>
    {
        private readonly LeadDb _context;
        private readonly IMapper<ServiceDto, Service> _mapper;

        public ServiceRepository(LeadDb context, IMapper<ServiceDto, Service> mapper) =>
            (_context, _mapper) = (context, mapper);


        public async Task<IEnumerable<ServiceDto>> GetAllAsync()
        {
            var services = await _context.Services.ToListAsync();
            return services.Select(_mapper.MapToDto).ToList();
        }

        public async Task<ServiceDto> GetByIdAsync(int id)
        {
            var service = await _context.Services.FindAsync(new object[] { id });
            if (service == null) throw new ArgumentNullException(nameof(id));
            return _mapper.MapToDto(service);
        }

        public async Task<ServiceDto> GetByNameAsync(string name)
        {
            var service = await _context.Services.FirstOrDefaultAsync(
                s => s.Title.ToLower() == name.ToLower()
            );
            if (service == null) throw new ArgumentNullException(nameof(name));
            return _mapper.MapToDto(service);
        }

        public async Task AddAsync(ServiceDto value)
        {
            var service = _mapper.MapToEntity(value);
            await _context.Services.AddAsync(service);
            await SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var serviceFromDb = await _context.Services.FindAsync(new object[] { id });
            if (serviceFromDb == null) throw new ArgumentNullException(nameof(id));
            _context.Services.Remove(serviceFromDb);
            await SaveAsync();
        }

        public async Task UpdateAsync(ServiceDto value)
        {
            var serviceFromDb = await _context.Services.FindAsync(new object[] { value.Id });
            if (serviceFromDb == null) throw new ArgumentNullException(nameof(value.Id));
            serviceFromDb.Title = value.Title;
            serviceFromDb.Description = value.Description;
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}