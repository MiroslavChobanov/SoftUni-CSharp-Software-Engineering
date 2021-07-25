using System;
using System.Collections.Generic;
using System.Text;

namespace VehiclesExtension
{
    public abstract class Vehicle
    {
        private double fuelQuantity;
        protected Vehicle(double fuelQuantity, double fuelConsumption, double tankCapacity)
        {
            this.TankCapacity = tankCapacity;
            this.FuelQuantity = fuelQuantity;
            this.FuelConsumption = fuelConsumption;
            
        }

        public double FuelQuantity {
            get => this.fuelQuantity;  
            private set 
            {
                if (value > this.TankCapacity)
                {
                    this.fuelQuantity = 0;
                }
                else
                {
                    this.fuelQuantity = value;
                }
            }
        }
        public virtual double FuelConsumption { get; }
        public double TankCapacity { get; }

        public virtual void Refuel(double amount)
        {
            if (amount <= 0)
            {
                throw new InvalidOperationException("Fuel must be a positive number");
            }
            if (amount > this.TankCapacity)
            {
                throw new InvalidOperationException($"Cannot fit {amount} fuel in the tank");
            }
            this.FuelQuantity += amount;
        }

        public bool CanDrive(double distance)
        {
            bool canDrive = this.FuelQuantity - this.FuelConsumption * distance >= 0;

            if (!canDrive)
            {
                return false;
            }

            this.FuelQuantity -= this.FuelConsumption * distance;
            return true;
        }
    }
}
