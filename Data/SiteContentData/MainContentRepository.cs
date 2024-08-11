using api.Dto;
using api.Entities;
using api.Mapper;

namespace api.Data.SiteContentData
{
    public class MainContentRepository : IRepository<MainContentDto>
    {
        private readonly LeadDb _context;
        private readonly IMapper<MainContentDto, MainContent> _mapper;

        public MainContentRepository(LeadDb context, IMapper<MainContentDto, MainContent> mapper) =>
            (_context, _mapper) = (context, mapper);


        public async Task<IEnumerable<MainContentDto>> GetAllAsync()
        {
            var mainContents = await _context.MainContents.ToListAsync();
            return mainContents.Select(_mapper.MapToDto).ToList();
        }

        public async Task<MainContentDto> GetByIdAsync(int id)
        {
            var mainContent = await _context.MainContents.FindAsync(new object[] { id });
            if (mainContent == null) throw new ArgumentNullException(nameof(id));
            return _mapper.MapToDto(mainContent);
        }

        public async Task<MainContentDto> GetByNameAsync(string name)
        {
            var mainContent = await _context.MainContents.FirstOrDefaultAsync(
                m => m.HeaderContent.ToLower() == name.ToLower()
            );
            if (mainContent == null) throw new ArgumentNullException(nameof(name));
            return _mapper.MapToDto(mainContent);
        }

        public async Task AddAsync(MainContentDto value)
        {
            var mainContent = _mapper.MapToEntity(value);
            await _context.MainContents.AddAsync(mainContent);
            await SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var mainContent = await _context.MainContents.FindAsync(new object[] { id });
            if (mainContent == null) throw new ArgumentNullException(nameof(id));
            _context.MainContents.Remove(mainContent);
            await SaveAsync();
        }

        public async Task UpdateAsync(MainContentDto value)
        {
            var maincontentFromDb = await _context.MainContents.FindAsync(new object[] { value.Id });
            if (maincontentFromDb == null) throw new ArgumentNullException(nameof(value.Id));
            maincontentFromDb.HeaderContent = value.HeaderContent;
            maincontentFromDb.MenuButtonText = value.MenuButtonText;
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}