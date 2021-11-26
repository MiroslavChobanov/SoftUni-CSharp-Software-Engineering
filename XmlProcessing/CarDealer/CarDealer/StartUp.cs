using CarDealer.Data;
using CarDealer.Dto;
using CarDealer.Dtos.Export;
using CarDealer.Dtos.Import;
using CarDealer.Models;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {

        }

        //Task 09
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var reader = new StringReader(inputXml);

            var serializer = new XmlSerializer(typeof(ImportSupplierDto[]), new XmlRootAttribute("Suppliers"));
            var supplierDtos = (ImportSupplierDto[])serializer.Deserialize(reader);

            var suppliers = supplierDtos
                .Select(s => new Supplier
                {
                    Name = s.Name,
                    IsImporter = s.IsImporter
                })
                .ToList();

            context.Suppliers.AddRange(suppliers);
            var importedCount = context.SaveChanges();

            return $"Successfully imported {importedCount}";
        }

        //Task 10
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            var reader = new StringReader(inputXml);

            var serializer = new XmlSerializer(typeof(ImportPartDto[]), new XmlRootAttribute("Parts"));
            var partDtos = (ImportPartDto[])serializer.Deserialize(reader);

            var parts = partDtos
                .Where(p => context.Suppliers.Any(s => s.Id == p.SupplierId))
                .Select(p => new Part
                {
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    SupplierId = p.SupplierId
                })
                .ToList();

            context.Parts.AddRange(parts);
            var importedCount = context.SaveChanges();

            return $"Successfully imported {importedCount}";
        }

        //Task 11
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var reader = new StringReader(inputXml);

            var serializer = new XmlSerializer(typeof(ImportCarDto[]), new XmlRootAttribute("Cars"));
            var carDtos = (ImportCarDto[])serializer.Deserialize(reader);

            foreach (var carDto in carDtos)
            {
                var car = new Car
                {
                    Make = carDto.Make,
                    Model = carDto.Model,
                    TravelledDistance = carDto.TravelledDistance
                };

                context.Cars.Add(car);

                var partsId = carDto.Parts
                    .Distinct()
                    .Select(p => p.Id)
                    .ToArray();

                foreach (var partId in partsId)
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

            return $"Successfully imported {carDtos.Length}";
        }

        //Task 12
        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            var reader = new StringReader(inputXml);

            var serializer = new XmlSerializer(typeof(ImportCustomerDto[]), new XmlRootAttribute("Customers"));
            var customerDtos = (ImportCustomerDto[])serializer.Deserialize(reader);

            var customers = customerDtos
                .Select(c => new Customer
                {
                    Name = c.Name,
                    BirthDate = c.BirthDate,
                    IsYoungDriver = c.IsYoungDriver
                })
                .ToList();

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count}";
        }

        //Task 13
        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            var reader = new StringReader(inputXml);

            var serializer = new XmlSerializer(typeof(ImportSaleDto[]), new XmlRootAttribute("Sales"));
            var saleDtos = (ImportSaleDto[])serializer.Deserialize(reader);

            var sales = saleDtos
                .Where(s => context.Cars.Any(c => c.Id == s.CarId))
                .Select(s => new Sale
                {
                    CarId = s.CarId,
                    CustomerId = s.CustomerId,
                    Discount = s.Discount
                })
                .ToList();

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count}";
        }

        //Task 14
        public static string GetCarsWithDistance(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(c => c.TravelledDistance > 2000000)
                .Select(c => new CarDistanceDto
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .Take(10)
                .ToArray();

            using var writer = new StringWriter();

            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            var serializer = new XmlSerializer(typeof(CarDistanceDto[]), new XmlRootAttribute("cars"));
            serializer.Serialize(writer, cars, ns);

            var carsXml = writer.GetStringBuilder();

            return carsXml.ToString().TrimEnd();
        }

        //Task 15
        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(c => c.Make == "BMW")
                .Select(c => new BMWCarDto
                {
                    Id = c.Id,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .ToArray();
                


            var writer = new StringWriter();

            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            var serializer = new XmlSerializer(typeof(BMWCarDto[]), new XmlRootAttribute("cars"));
            serializer.Serialize(writer, cars, ns);

            var carsXml = writer.GetStringBuilder();

            return carsXml.ToString().TrimEnd();
        }

        //Task 16
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(s => !s.IsImporter)
                .Select(s => new SupplierDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count
                })
                .ToArray();

            var writer = new StringWriter();

            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            var serializer = new XmlSerializer(typeof(SupplierDto[]), new XmlRootAttribute("suppliers"));
            serializer.Serialize(writer, suppliers, ns);

            var suppliersXml = writer.GetStringBuilder();

            return suppliersXml.ToString().TrimEnd();
        }

        //Task 17
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                .Select(c => new CarWithPartsDto
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance,
                    Parts = c.PartCars.Select(pc => new PartDto
                    {
                        Name = pc.Part.Name,
                        Price = pc.Part.Price
                    })
                    .OrderByDescending(pc => pc.Price)
                    .ToArray()
                })
                .OrderByDescending(c => c.TravelledDistance)
                .ThenBy(c => c.Model)
                .Take(5)
                .ToArray();

            var writer = new StringWriter();

            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            var serializer = new XmlSerializer(typeof(CarWithPartsDto[]), new XmlRootAttribute("cars"));
            serializer.Serialize(writer, cars, ns);

            var carsXml = writer.GetStringBuilder();

            return carsXml.ToString().TrimEnd();
        }

        //Task 18
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Where(c => c.Sales.Any())
                .Select(c => new CustomerDto
                {
                    FullName = c.Name,
                    BoughtCars = c.Sales.Count,
                    SpentMoney = c.Sales
                    .SelectMany(s => s.Car.PartCars)
                    .Sum(p => p.Part.Price)
                })
                .OrderByDescending(c => c.SpentMoney)
                .ToArray();

            var writer = new StringWriter();

            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            var serializer = new XmlSerializer(typeof(CustomerDto[]), new XmlRootAttribute("customers"));
            serializer.Serialize(writer, customers, ns);

            var customersXml = writer.GetStringBuilder();

            return customersXml.ToString().TrimEnd();
        }

        //Task 19
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales
                .Select(s => new SaleDto
                {
                    Car = new CarDistanceDto
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TravelledDistance = s.Car.TravelledDistance
                    },
                    Discount = s.Discount,
                    CustomerName = s.Customer.Name,
                    Price = s.Car.PartCars.Sum(pc => pc.Part.Price),
                    PriceWithDiscount = s.Car.PartCars.Sum(pc => pc.Part.Price)
                    - (s.Car.PartCars.Sum(pc => pc.Part.Price) * s.Discount / 100)
                })
                .ToArray();

            var writer = new StringWriter();

            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            var serializer = new XmlSerializer(typeof(SaleDto[]), new XmlRootAttribute("sales"));
            serializer.Serialize(writer, sales, ns);

            var salesXml = writer.GetStringBuilder();

            return salesXml.ToString().TrimEnd();
        }
    }
}