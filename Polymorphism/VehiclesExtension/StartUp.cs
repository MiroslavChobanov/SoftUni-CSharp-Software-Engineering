using System;
using System.Linq;

namespace VehiclesExtension
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
            string[] busInfo = Console.ReadLine()
                .Split()
                .ToArray();

            Vehicle car = new Car(double.Parse(carInfo[1]), double.Parse(carInfo[2]), double.Parse(carInfo[3]));
            Vehicle truck = new Truck(double.Parse(truckInfo[1]), double.Parse(truckInfo[2]), double.Parse(truckInfo[3]));
            Bus bus = new Bus(double.Parse(busInfo[1]), double.Parse(busInfo[2]), double.Parse(busInfo[3]));

            int n = int.Parse(Console.ReadLine());
            ;
            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine()
                .Split()
                .ToArray();

                string command = input[0];
                string type = input[1];
                double amount = double.Parse(input[2]);

                if (command == "Drive")
                {
                    if (type == "Car")
                    {
                        CanDrive(car, amount);
                    }
                    else if (type == "Truck")
                    {
                        CanDrive(truck, amount);
                    }
                    else
                    {
                        bus.IsEmpty = false;
                        CanDrive(bus, amount);
                    }
                }
                else if(command == "Refuel")
                {
                    try
                    {
                        if (type == "Car")
                        {
                            car.Refuel(amount);
                        }
                        else if (type == "Truck")
                        {
                            truck.Refuel(amount);
                        }
                        else
                        {
                            bus.Refuel(amount);
                        }
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    bus.IsEmpty = true;
                    CanDrive(bus, amount); 
                }

            }

            Console.WriteLine($"Car: {car.FuelQuantity:F2}");
            Console.WriteLine($"Truck: {truck.FuelQuantity:F2}");
            Console.WriteLine($"Bus: {bus.FuelQuantity:F2}");
        }

        public static void CanDrive(Vehicle vehicle, double distance)
        {
            bool canDrive = vehicle.CanDrive(distance);
            string vehicleType = vehicle.GetType().Name;
            string result = canDrive
                ? $"{vehicleType} travelled {distance} km"
                : $"{vehicleType} needs refueling";

            Console.WriteLine(result);
        }
    }
}
