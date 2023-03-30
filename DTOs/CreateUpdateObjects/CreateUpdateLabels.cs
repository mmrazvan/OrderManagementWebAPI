using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrderManagementWebAPI.DTOs.CreateUpdateObjects
{
    public class CreateUpdateLabels
    {
        [Key]
        [JsonIgnore]
        public int LabelId { get; set; }
        public string LabelName { get; set; }
        public float Heigth { get; set; }
        public float Width { get; set; }
    }
}
