using System;
using System.Collections.Generic;
using System.Text;

namespace VehiclesExtension
{
    public class Car : Vehicle
    {
        private const double AirConConsumption = 0.9;
        public Car(double fuelQuantity, double fuelConsumption, double tankCapacity) : base(fuelQuantity, fuelConsumption, tankCapacity)
        {

        }

        public override double FuelConsumption => base.FuelConsumption + AirConConsumption;
    }
}
