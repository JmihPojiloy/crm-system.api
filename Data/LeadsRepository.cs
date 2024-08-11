using api.Mapper;
using api.Dto;

namespace api.Data
{

    public class LeadsRepository : ILeadsRepository
    {
        private readonly LeadDb _context;
        private readonly LeadMapper _mapper;

        public LeadsRepository(LeadDb context, LeadMapper mapper) =>
            (_context, _mapper) = (context, mapper);

        public async Task<List<LeadDto>> GetLeadsAsync()
        {
            var leads = await _context.Leads.ToListAsync();
            return leads.Select(_mapper.MapToLead).ToList();
        }

        public async Task<LeadDto> GetLeadAsync(int id)
        {
            var lead = await _context.Leads.FindAsync(new object[] { id });
            if (lead == null) throw new ArgumentNullException(nameof(id));
            return _mapper.MapToLead(lead);
        }

        public async Task<LeadDto> GetLeadAsync(string name)
        {
            var lead = await _context.Leads.FirstOrDefaultAsync(
                p => p.ClientName.ToLower() == name.ToLower());
            if (lead == null) throw new ArgumentNullException(nameof(name));
            return _mapper.MapToLead(lead);
        }


        public async Task AddLeadAsync(LeadDto leadDto)
        {
            var lead = _mapper.MapToEntity(leadDto);
            _context.Leads.Add(lead);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateLeadAsync(LeadDto leadDto)
        {
            var lead = _mapper.MapToEntity(leadDto);
            var leadFromDb = await _context.Leads.FindAsync(new object[] { leadDto.Id });
            if (leadFromDb == null) throw new ArgumentNullException(nameof(leadFromDb));
            leadFromDb.ClientName = lead.ClientName;
            leadFromDb.ClientQuestion = lead.ClientQuestion;
            leadFromDb.Contact = lead.Contact;
            leadFromDb.Status = lead.Status;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteLeadAsync(int id)
        {
            var leadFromDb = await _context.Leads.FindAsync(new object[] { id });
            if (leadFromDb == null) return;
            _context.Leads.Remove(leadFromDb);
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}