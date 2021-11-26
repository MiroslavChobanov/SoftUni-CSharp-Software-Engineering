using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.DTO.ExportDtos
{
    public class ExportCarDto
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public long TravelledDistance { get; set; }
    }
}
