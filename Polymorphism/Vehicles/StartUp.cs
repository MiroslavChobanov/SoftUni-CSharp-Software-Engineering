using System;
using System.Linq;

namespace Vehicles
{
    class StartUp
    {
        static void Main(string[] args)
        {
            string[] carInfo = Console.ReadLine()
                .Split()
                .ToArray();
            string[] truckInfo = Console.ReadLine()
                .Split()
                .ToArray();

            Vehicle car = new Car(double.Parse(carInfo[1]), double.Parse(carInfo[2]));
            Vehicle truck = new Truck(double.Parse(truckInfo[1]), double.Parse(truckInfo[2]));

            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine()
                .Split()
                .ToArray();

                string command = input[0];
                string type = input[1];
                double amount = double.Parse(input[2]);

                if (command is "Drive")
                {
                    if (type is "Car")
                    {
                        CanDrive(car, amount);
                    }
                    else
                    {
                        CanDrive(truck, amount);
                    }
                }
                else // refuel
                {
                    if (type is "Car")
                    {
                        car.Refuel(amount);
                    }
                    else
                    {
                        truck.Refuel(amount);
                    }

                }
                    
            }

            Console.WriteLine($"Car: {car.FuelQuantity:F2}");
            Console.WriteLine($"Truck: {truck.FuelQuantity:F2}");
        }

        public static void CanDrive(Vehicle vehicle, double distance)
        {
            bool canDrive = vehicle.CanDrive(distance);
            string vehicleType = vehicle.GetType().Name;
            string result = canDrive ? $"{vehicleType} travelled {distance} km"
                : $"{vehicleType} needs refueling";

            Console.WriteLine(result);
        }
    }
}
