using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using VaporStore.Data.Models;

namespace VaporStore.DataProcessor.Dto.Import
{
    [XmlType("Purchase")]
    public class ImportPurchaseDto
    {
        [Required]
        [XmlAttribute("title")]
        public string Title { get; set; }
        [XmlElement("Type")]
        [Required]
        
        public string Type { get; set; }
        [XmlElement("Key")]
        [Required]
        [RegularExpression(@"^[A-Z0-9]{4}\-[A-Z0-9]{4}\-[A-Z0-9]{4}$")]
        public string Key { get; set; }
        [XmlElement("Card")]
        [Required]
        [RegularExpression(@"^.*\b(\d{4})\b.*\b(\d{4})\b.*\b(\d{4})\b.*\b(\d{4})\b.*$")]
        public string Card { get; set; }
        [XmlElement("Date")]
        [Required]
        public string Date { get; set; }
    }
}
