using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace TeisterMask.DataProcessor.ExportDto
{
    [XmlType("Project")]
    public class ExportProjectDto
    {
        [XmlAttribute("TasksCount")]
        public int TasksCount { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string ProjectName { get; set; }
        public string HasEndDate { get; set; }

        [XmlArray("Tasks")]
        public ExportTaskDto[] Tasks { get; set; }
    }
}
