using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BakeryOpenning
{
    public class Bakery
    {
        public Bakery(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.data = new List<Employee>();
        }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public List<Employee> data { get; set; }

        public int Count { get { return data.Count; } }

        public void Add(Employee employee)
        {
            if (data.Count < Capacity)
            {
                data.Add(employee);
            }
        }

        public bool Remove(string name)
        {
            var employee = data.FirstOrDefault(e => e.Name == name);
            if (employee != null)
            {
                return true;
            }

            return false;
        }

        public Employee GetOldestEmployee()
        {
            var oldestEmployee = data.OrderByDescending(e => e.Age).FirstOrDefault();

            return oldestEmployee;
        }

        public Employee GetEmployee(string name)
        {
            var employee = data.FirstOrDefault(e => e.Name == name);

            return employee;
        }

        public string Report()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Employees working at Bakery {this.Name}:");

            foreach (var employee in data)
            {
                sb.AppendLine(employee.ToString());
            }

            return sb.ToString().TrimEnd();
        }


    }

    
}
