using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace VaporStore.DataProcessor.Dto.Import
{
    public class ImportCardDto
    {
        [Required]
        [RegularExpression(@"^.*\b(\d{4})\b.*\b(\d{4})\b.*\b(\d{4})\b.*\b(\d{4})\b.*$")]
        public string Number { get; set; }
        [Required]
        [StringLength(3)]
        [RegularExpression(@"\d{3}$")]
        [JsonProperty("CVC")]
        public string Cvc { get; set; }
        [Required]
        public string Type { get; set; }
    }
}