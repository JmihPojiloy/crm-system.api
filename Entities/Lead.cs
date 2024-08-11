using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities
{
    public class Lead
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? ClientName { get; set; }
        public string? ClientQuestion { get; set; }
        public string? Contact { get; set; }
        public int Status { get; set; }
    }
}