using api.Enums;

namespace api.Dto
{
    public class LeadDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? ClientName { get; set; }
        public string? ClientQuestion { get; set; }
        public string? Contact { get; set; }
        public Status Status { get; set; }
    }
}