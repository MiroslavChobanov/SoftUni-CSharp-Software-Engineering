using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassroomProject
{
    public class Classroom
    {
        public Classroom(int capacity)
        {
            this.Capacity = capacity;
            this.Students = new List<Student>();
        }
        public int Capacity { get; set; }
        public int Count { get { return this.Students.Count; } }
        public List<Student> Students { get; set; }


        public string RegisterStudent(Student student)
        {
            if (Students.Count < Capacity)
            {
                Students.Add(student);
                return $"Added student {student.FirstName} {student.LastName}";
            }
            else
            {
                return "No seats in the classroom";
            }
        }

        public string DismissStudent(string firstName, string lastName)
        {
            var student = Students.FirstOrDefault(s => s.FirstName == firstName && s.LastName == lastName);
            if (student == null)
            {
                return "Student not found";
                
            }

            this.Students.Remove(student);
            return $"Dismissed student {student.FirstName} {student.LastName}";
        }

        public string GetSubjectInfo(string subject)
        {
            var students = Students.Where(s => s.Subject == subject).ToList();

            var sb = new StringBuilder();
            if (!students.Any())
            {
                return "No students enrolled for the subject";
            }
            else
            {
                sb.AppendLine($"Subject: {subject}");
                sb.AppendLine("Students:");

                foreach (var student in students)
                {
                    sb.AppendLine($"{student.FirstName} {student.LastName}");
                }

                return sb.ToString().TrimEnd();
            }
        }

        public int GetStudentsCount()
        {
            return Students.Count;
        }

        public Student GetStudent(string firstName, string lastName)
        {
            var student = Students.FirstOrDefault(s => s.FirstName == firstName && s.LastName == lastName);
            return student;
        }
    }
}
