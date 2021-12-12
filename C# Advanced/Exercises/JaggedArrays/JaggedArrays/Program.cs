using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_Moves
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] numbers = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(double.Parse)
                .ToArray();

            Dictionary<double, int> counter = new Dictionary<double, int>();

            foreach (var number in numbers)
            {
                if (counter.ContainsKey(number))
                {
                    counter[number]++;
                }
                else
                {
                    counter.Add(number, 1);
                }
            }

            foreach (var kvp in counter)
            {
                Console.WriteLine($"{kvp.Key} - {kvp.Value} times");
            }

        }
    }
}