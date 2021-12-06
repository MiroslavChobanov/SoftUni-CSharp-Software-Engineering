using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using Theatre.Data.Models.Enums;

namespace Theatre.DataProcessor.ExportDto
{
    [XmlType("Play")]
    public class ExportPlayDto
    {
        [XmlAttribute("Title")]
        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string Title { get; set; }
        [XmlAttribute("Duration")]
        [Required]
        [Range(typeof(TimeSpan), "01:00", "23:59")]
        public string Duration { get; set; }
        [XmlAttribute("Rating")]
        public string Rating { get; set; }
        [XmlAttribute("Genre")]
        [Required]
        [EnumDataType(typeof(Genre))]
        public string Genre { get; set; }
        [XmlArray("Actors")]
        public ExportActorDto[] Actors { get; set; }
    }
}
