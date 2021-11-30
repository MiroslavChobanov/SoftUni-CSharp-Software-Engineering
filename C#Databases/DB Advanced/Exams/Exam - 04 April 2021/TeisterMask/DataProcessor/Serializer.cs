namespace TeisterMask.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using TeisterMask.DataProcessor.ExportDto;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            var projects = context.Projects
                .ToArray()
                .Where(p => p.Tasks.Any())
                .Select(p => new ExportProjectDto()
                {
                    TasksCount = p.Tasks.Count,
                    ProjectName = p.Name,
                    HasEndDate = HasEndDate(p.DueDate) ? "Yes" : "No",
                    Tasks = p.Tasks
                        .Select(t => new ExportTaskDto()
                        {
                            Name = t.Name,
                            Label = t.LabelType.ToString()
                        })
                        .OrderBy(t => t.Name)
                        .ToArray()
                })
                .OrderByDescending(p => p.TasksCount)
                .ThenBy(p => p.ProjectName)
                .ToArray();

            var writer = new StringWriter();

            var ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            var xmlSerializer = new XmlSerializer(
                typeof(ExportProjectDto[]),
                new XmlRootAttribute("Projects"));
            xmlSerializer.Serialize(writer, projects, ns);

            var projectsXml = writer.GetStringBuilder();

            return projectsXml.ToString().TrimEnd();
        }

        private static bool HasEndDate(DateTime? dueDate)
        {
            var hasEndDate = false;

            if (dueDate != null)
            {
                hasEndDate = true;
            }

            return hasEndDate;
        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            var employees = context.Employees
                .ToArray()
                .Where(et => et.EmployeesTasks.Any(t => t.Task.OpenDate >= date))
                .Select(et => new
                {
                    Username = et.Username,
                    Tasks = et.EmployeesTasks
                        .Select(et => et.Task)
                        .Where(t => t.OpenDate >= date)
                        .OrderByDescending(t => t.DueDate)
                        .ThenBy(t => t.Name)
                        .Select(t => new
                        {
                            TaskName = t.Name,
                            OpenDate = t.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                            DueDate = t.DueDate.ToString("d", CultureInfo.InvariantCulture),
                            LabelType = t.LabelType.ToString(),
                            ExecutionType = t.ExecutionType.ToString()
                        })
                        .ToArray()
                })
                    .OrderByDescending(e => e.Tasks.Count())
                    .ThenBy(e => e.Username)
                    .Take(10)
                    .ToArray();

            var employeesJson = JsonConvert.SerializeObject(employees, Formatting.Indented);

            return employeesJson;
        }
    }
}