using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles
{
    public class Truck : Vehicle
    {
        private const double AirConConsumption = 1.6;
        public Truck(double fuelConsumption, double fuelQuantity) : base(fuelConsumption, fuelQuantity)
        {

        }

        public override double FuelConsumption => base.FuelConsumption + AirConConsumption;

        public override void Refuel(double amount)
        {
            base.Refuel(amount * 0.95);
        }
    }
}
