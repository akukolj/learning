using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<int, int> f = s => s * s;

            Func<int, int, int> add = (a, b) => a + b;

            Console.WriteLine(f(add(3,5)));

            var developers = new[]
            {
                new Employee{Id = 1,Name = "Scott"},
                new Employee{Id = 2,Name = "Chris"}
            };

            var sales = new List<Employee>()
            {
                new Employee{Id = 3,Name = "Alex"}
            };

            foreach (var developer in developers.Where(s=>s.Name.StartsWith("S")))
            {
               Console.WriteLine(developer.Name); 
            }
           Console.WriteLine(sales.Count());
        }

     
    }
}
