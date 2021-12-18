using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Drones
{
    public class Airfield
    {
        public Airfield(string name, int capacity, double landingStrip)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.LandingStrip = landingStrip;
            this.Drones = new List<Drone>();
        }
        public List<Drone> Drones { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public double LandingStrip { get; set; }

        public int Count { get { return Drones.Count; } }

        public string AddDrone(Drone drone)
        {
            if (Drones.Count >= Capacity)
            {
                return "Airfield is full.";
                
            }
            else
            {
                if (string.IsNullOrEmpty(drone.Name) || string.IsNullOrEmpty(drone.Brand) || drone.Range < 5 || drone.Range > 15)
                {
                    return "Invalid drone.";
                }
                else
                {
                    Drones.Add(drone);
                    return $"Successfully added {drone.Name} to the airfield.";
                }
            }
        }

        public bool RemoveDrone(string name)
        {
            var drone = Drones.FirstOrDefault(d => d.Name == name);

            if (drone != null)
            {
                Drones.Remove(drone);
                return true;
            }

            return false;
        }

        public int RemoveDroneByBrand(string brand)
        {
            int removedCount = 0;
            var drones = Drones.Where(d => d.Brand == brand).ToList();
            foreach (var drone in drones)
            {
                if (drone.Brand == brand)
                {
                    Drones.Remove(drone);
                    removedCount++;
                }
            }
            if (drones.Count > 0)
            {
                return removedCount;
            }
            return 0;
        }

        public Drone FlyDrone(string name)
        {
            var drones = Drones.Where(d => d.Name == name).ToList();
            foreach (var drone in drones)
            {
                drone.Available = false;

                if (drone != null)
                {
                    return drone;
                }
            }
            return null;
        }

        public List<Drone> FlyDronesByRange(int range)
        {
            var drones = Drones.Where(d => d.Range >= range).ToList();

            foreach (var drone in drones)
            {
                drone.Available = false;
            }

            return drones;
        }

        public string Report()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Drones available at {this.Name}:");

            foreach (var drone in Drones)
            {
                if (drone.Available == true)
                {
                    sb.AppendLine(drone.ToString());
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}
