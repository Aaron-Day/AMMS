using System.ComponentModel.DataAnnotations;

namespace AMMS.Models
{
    public class DA2408_18
    {
        [Key]
        public string Id { get; set; }

        private string AircraftId { get; set; }
        private Aircraft Aircraft { get; set; }
    }
}
