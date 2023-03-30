using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrderManagementWebAPI.DTOs
{
    public partial class Orders
    {
        [JsonIgnore]
        public int OrderNumber { get; set; }
        //[StringLength(10, MinimumLength = 1)]
        public string? Client { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string DocumentName { get; set; }
        [Required]
        [StringLength(5, MinimumLength = 1)]
        public string DocumentFormat { get; set; }
        [Required]
        [StringLength(5)]
        public string EnvelopeType { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        [Required]
        [Range(1, 6)]
        public int PagesOnEnvelope { get; set; } = 1;
        [StringLength(10, MinimumLength = 1)]
        public string LabelType { get; set; }
        [JsonIgnore]
        [StringLength(20, MinimumLength = 1)]
        public string OrderStatus { get; set; } = "New";
        [JsonIgnore]
        public int Completed { get; set; } = 0;
        [JsonIgnore]
        public DateTime DateInSystem { get; set; } = DateTime.Now;
        [JsonIgnore]
        public DateTime? DateFinished { get; set; } = null;
        [JsonIgnore]
        public DateTime? DateInProduction { get; set; } = null;
        [JsonIgnore]
        public bool? HasCustomSort { get; set; } = false;
        [JsonIgnore]
        public string CustomSortFile { get; set; }
        [JsonIgnore]
        public string CustomSortField { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrderLabels> OrderLabels { get; set; }
    }
}