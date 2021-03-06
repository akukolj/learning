﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            var cars = ProcessCars("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");

            var query = from car in cars
                group car by car.Manufacturer
                into carGroup
                select new
                {
                    name = carGroup.Key,
                    Max = carGroup.Max(c => c.Combined),
                    Min = carGroup.Min(c => c.Combined),
                    Average = carGroup.Average(c => c.Combined)
                } ;


        var query2 = cars.GroupBy(c=> c.Manufacturer)
                .Select(g =>
                {
                    var resuls = g.Aggregate(new CarStatistic(), (acc, car) => acc.Accumulate(car),
                        acc => acc.Compute());
                    return new
                    {
                        name = g.Key,
                        avg = resuls.Average
                    }

                }
            ) 

            foreach (var group in query)
            {
                Console.WriteLine(group.name + "\n");
               Console.WriteLine($"\t {group.Min}");
                Console.WriteLine($"\t {group.Max}");
                Console.WriteLine($"\t {group.Average}");
            }
        }

        private static List<Car> ProcessCars(string path)
        {
            var query =

                File.ReadAllLines(path)
                    .Skip(1)
                    .Where(l => l.Length > 1)
                    .ToCar();

            return query.ToList();
        }

        public class CarStatistic
        {
            public CarStatistic()
            {
                Max = Int32.MinValue;
                Min = Int32.MaxValue;
            }
            internal CarStatistic Accumulate(Car c)
            {

                Total += c.Combined;
                Max = Math.Max(Max, c.Combined);
                Min = Math.Min(Min, c.Combined);
                return this;
            }

            public int Max { get; set; }
            public int Min { get; set; }
            public int Total { get; set; }
            public  int Count { get; set; }
            public double Average { get; set; }

            public CarStatistic Compute()
            {
                Average = Total / Count;
                return this;
            }
        }
        private static List<Manufacturer> ProcessManufacturers(string path)
        {
            var query =
                   File.ReadAllLines(path)
                       .Where(l => l.Length > 1)
                       .Select(l =>
                       {
                           var columns = l.Split(',');
                           return new Manufacturer
                           {
                               Name = columns[0],
                               Headquarters = columns[1],
                               Year = int.Parse(columns[2])
                           };
                       });
            return query.ToList();
        }
    }

    public class CarStatistics
    {
        public CarStatistics()
        {
            Max = Int32.MinValue;
            Min = Int32.MaxValue;
        }
        
        public CarStatistics Accumulate(Car car)
        {
            Count += 1;
            Total += car.Combined;
            Max = Math.Max(Max, car.Combined);
            Min = Math.Min(Min, car.Combined);
            return this;
        }

        public CarStatistics Compute()
        {
            Average = Total / Count;
            return this;
        }

        public int Max { get; set; }
        public int Min { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }
        public double Average { get; set; }

    }

    public static class CarExtensions
    {
        public static IEnumerable<Car> ToCar(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(',');

                yield return new Car
                {
                    Year = int.Parse(columns[0]),
                    Manufacturer = columns[1],
                    Name = columns[2],
                    Displacement = double.Parse(columns[3]),
                    Cylinders = int.Parse(columns[4]),
                    City = int.Parse(columns[5]),
                    Highway = int.Parse(columns[6]),
                    Combined = int.Parse(columns[7])
                };
            }
        }
    }
}
