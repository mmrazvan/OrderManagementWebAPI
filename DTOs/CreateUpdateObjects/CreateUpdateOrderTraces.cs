using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrderManagementWebAPI.DTOs.CreateUpdateObjects
{
    public class CreateUpdateOrderTraces
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public int OrderNumber { get; set; }
        [JsonIgnore]
        public string? IdBoxNumber { get; set; }
        [JsonIgnore]
        public DateTime? DateOut { get; set; }
        [StringLength(5, MinimumLength = 1)]
        public string MachineId { get; set; }
    }
}
