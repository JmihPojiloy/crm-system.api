using api.Dto;

namespace api.Data
{
    public interface ILeadsRepository
    {
        Task<List<LeadDto>> GetLeadsAsync();
        Task<LeadDto> GetLeadAsync(int id);
        Task<LeadDto> GetLeadAsync(string name);
        Task AddLeadAsync(LeadDto lead);
        Task UpdateLeadAsync(LeadDto lead);
        Task DeleteLeadAsync(int id);
        Task SaveAsync();
    }
}