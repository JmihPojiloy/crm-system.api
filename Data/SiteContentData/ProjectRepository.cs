using api.Dto;
using api.Entities;
using api.Mapper;

namespace api.Data.SiteContentData
{
    public class ProjectRepository : IRepository<ProjectDto>
    {
        private readonly LeadDb _context;
        private readonly IMapper<ProjectDto, Project> _mapper;

        public ProjectRepository(LeadDb context, IMapper<ProjectDto, Project> mapper) =>
            (_context, _mapper) = (context, mapper);


        public async Task<IEnumerable<ProjectDto>> GetAllAsync()
        {
            var projects = await _context.Projects.ToListAsync();
            var projectDtos = projects.Select(_mapper.MapToDto).ToList();
            return projectDtos;
        }

        public async Task<ProjectDto> GetByIdAsync(int id)
        {
            var project = await _context.Projects.FindAsync(new object[] { id });
            if (project == null) throw new ArgumentNullException(nameof(id));
            return _mapper.MapToDto(project);
        }

        public async Task<ProjectDto> GetByNameAsync(string name)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(
                p => p.Title.ToLower() == name.ToLower()
            );
            if (project == null) throw new ArgumentNullException(nameof(name));
            return _mapper.MapToDto(project);
        }

        public async Task AddAsync(ProjectDto value)
        {
            var project = _mapper.MapToEntity(value);
            await _context.Projects.AddAsync(project);
            await SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var projectFromDb = await _context.Projects.FindAsync(new object[] { id });
            if (projectFromDb == null) throw new ArgumentNullException(nameof(id));
            _context.Projects.Remove(projectFromDb);
            await SaveAsync();
        }

        public async Task UpdateAsync(ProjectDto value)
        {
            var projectFromDb = await _context.Projects.FindAsync(new object[] { value.Id });
            if (projectFromDb == null) throw new ArgumentNullException(nameof(value.Id));
            projectFromDb.Title = value.Title;
            projectFromDb.Description = value.Description;
            projectFromDb.ImageUrl = value.ImageUrl;
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}