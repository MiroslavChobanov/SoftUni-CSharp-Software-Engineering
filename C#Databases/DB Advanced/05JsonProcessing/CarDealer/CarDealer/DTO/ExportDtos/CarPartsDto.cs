using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.DTO.ExportDtos
{
    public class CarPartsDto
    {
        [JsonProperty("car")]
        public ExportCarDto Car { get; set; }

        [JsonProperty("parts")]
        public ICollection<ExportPartDto> Parts { get; set; }
    }
}
