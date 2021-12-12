using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VetClinic
{
    public class Clinic
    {
        public Clinic(int capacity)
        {
            this.Capacity = capacity;
            this.data = new List<Pet>();
        }
        public List<Pet> data { get; set; }
        public int Capacity { get; set; }

        public void Add(Pet pet)
        {
            if (data.Count < Capacity)
            {
                data.Add(pet);
            }
        }
        public bool Remove(string name)
        {
            var petToRemove = data.FirstOrDefault(p => p.Name == name);
            return data.Remove(petToRemove);
        }

        public Pet GetPet(string name, string owner)
        {
            var pet = data.FirstOrDefault(p => p.Name == name && p.Owner == owner);
            return pet;
        }

        public Pet GetOldestPet()
        {
            var oldestPet = data.OrderByDescending(p => p.Age).FirstOrDefault();
            return oldestPet;
        }

        public int Count { get { return data.Count; } }

        public string GetStatistics()
        {
            var sb = new StringBuilder();

            sb.AppendLine("The clinic has the following patients:");

            foreach (var pet in data)
            {
                sb.AppendLine($"Pet {pet.Name} with owner: {pet.Owner}");
            }

            return sb.ToString();
        }
    }


}
