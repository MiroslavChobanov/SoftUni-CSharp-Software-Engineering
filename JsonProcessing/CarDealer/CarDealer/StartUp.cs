using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AutoMapper;
using CarDealer.Data;
using CarDealer.DTO;
using CarDealer.DTO.ExportDtos;
using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
        }

        //Task 09
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var suppliers = JsonConvert.DeserializeObject<Supplier[]>(inputJson);

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Length}.";
        }

        //Task 10
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var parts = JsonConvert.DeserializeObject<Part[]>(inputJson)
                .Where(p => context.Suppliers.Any(s => s.Id == p.SupplierId))
                .ToArray();
                

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Length}.";
        }

        //Task 11
        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var carDtos = JsonConvert.DeserializeObject<ImportCarDto[]>(inputJson);

            foreach (var carDto in carDtos)
            {
                var car = new Car
                {
                    Make = carDto.Make,
                    Model = carDto.Model,
                    TravelledDistance = carDto.TravelledDistance
                };

                context.Cars.Add(car);

                foreach (var partId in carDto.PartsId)
                {
                    var partCar = new PartCar
                    {
                        CarId = car.Id,
                        PartId = partId
                    };

                    if (car.PartCars.FirstOrDefault(pc => pc.PartId == partId) == null)
                    {
                        context.PartCars.Add(partCar);
                    }
                }
            }

            context.SaveChanges();

            return $"Successfully imported {carDtos.Length}.";
        }

        //Task 12
        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var customers = JsonConvert.DeserializeObject<Customer[]>(inputJson);

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Length}.";
        }

        //Task 13
        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var sales = JsonConvert.DeserializeObject<Sale[]>(inputJson);

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Length}.";
        }

        //Task 14
        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var customers = context.Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver)
                .Select(c => new
                {
                    c.Name,
                    BirthDate = c.BirthDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    c.IsYoungDriver
                })
                .ToList();

            var customersJson = JsonConvert.SerializeObject(customers, Formatting.Indented);

            return customersJson;
        }

        //Task 15
        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(x => x.Make == "Toyota")
                .OrderBy(x => x.Model)
                .ThenByDescending(x => x.TravelledDistance)
                .Select(x => new
                {
                    x.Id,
                    x.Make,
                    x.Model,
                    x.TravelledDistance
                })
                .ToList();

            var carsJson = JsonConvert.SerializeObject(cars, Formatting.Indented);

            return carsJson;
        }

        //Task 16
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(x => x.IsImporter == false)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    PartsCount = x.Parts.Count
                })
                .ToList();

            var suppliersJson = JsonConvert.SerializeObject(suppliers, Formatting.Indented);

            return suppliersJson;
        }

        //Task 17
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                .Select(x => new CarPartsDto
                {
                    Car = new ExportCarDto
                    {
                      Make =  x.Make,
                      Model = x.Model,
                      TravelledDistance = x.TravelledDistance
                    },
                    Parts = x.PartCars.Select(pc => new ExportPartDto
                    {
                        Name = pc.Part.Name,
                        Price = $"{pc.Part.Price:F2}"
                    })
                    .ToList()
                })
                .ToList();

            var carsJson = JsonConvert.SerializeObject(cars, Formatting.Indented);

            return carsJson;
        }

        //Task 18
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Where(c => c.Sales.Any(s => s.Car != null))
                .Select(c => new
                {
                    fullName = c.Name,
                    boughtCars = c.Sales.Count(s => s.Car != null),
                    spentMoney = c.Sales
                        .SelectMany(s => s.Car.PartCars)
                        .Sum(p => p.Part.Price)
                })
                .OrderByDescending(c => c.spentMoney)
                .ThenByDescending(c => c.boughtCars)
                .ToList();

            var customersJson = JsonConvert.SerializeObject(customers, Formatting.Indented);

            return customersJson;

        }

        //Task 19
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales
                .Select(s => new ExportSaleDiscountDto
                {
                    Car = new ExportCarDto
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TravelledDistance = s.Car.TravelledDistance
                    },
                    CustomerName = s.Customer.Name,
                    Discount = $"{s.Discount:F2}",
                    Price = $"{s.Car.PartCars.Sum(p => p.Part.Price):F2}",
                    PriceWithDiscount = $@"{s.Car.PartCars.Sum(p => p.Part.Price)
                        - s.Car.PartCars.Sum(p => p.Part.Price) * s.Discount / 100:F2}"
                })
                .Take(10)
                .ToList();

            var salesJson = JsonConvert.SerializeObject(sales, Formatting.Indented);

            return salesJson;
        }
    }
}