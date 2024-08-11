using api.Entities;
using api.Enums;
using api.Dto;

namespace api.Mapper
{
    public class LeadMapper
    {
        public LeadDto MapToLead(Lead lead)
        {
            if (lead == null) throw new ArgumentNullException(nameof(lead));

            return new LeadDto
            {
                Id = lead.Id,
                Date = lead.Date,
                ClientName = lead.ClientName,
                ClientQuestion = lead.ClientQuestion,
                Contact = lead.Contact,
                Status = (Status)lead.Status
            };
        }

        public Lead MapToEntity(LeadDto leadDto)
        {
            if (leadDto == null) throw new ArgumentNullException(nameof(leadDto));

            return new Lead
            {
                Date = leadDto.Date,
                ClientName = leadDto.ClientName,
                ClientQuestion = leadDto.ClientQuestion,
                Contact = leadDto.Contact,
                Status = (int)leadDto.Status
            };
        }
    }
}