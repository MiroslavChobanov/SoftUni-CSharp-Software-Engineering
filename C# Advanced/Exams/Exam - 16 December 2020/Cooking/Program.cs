using System;
using System.Collections.Generic;
using System.Linq;

namespace Cooking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var liquidsInput = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

            var ingredientsInput = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

            var liquids = new Queue<int>(liquidsInput);
            var ingredients = new Stack<int>(ingredientsInput);

            int breadCount = 0;
            int cakeCount = 0;
            int pastryCount = 0;
            int fruitPieCount = 0;
            ;
            while (liquids.Count > 0 && ingredients.Count > 0)
            {
                var liquid = liquids.Peek();
                var ingredient = ingredients.Peek();

                int sum = liquid + ingredient;

                if (sum == 25)
                {
                    breadCount++;
                    liquids.Dequeue();
                    ingredients.Pop();
                }
                else if (sum == 50)
                {
                    cakeCount++;
                    liquids.Dequeue();
                    ingredients.Pop();
                }
                else if (sum == 75)
                {
                    pastryCount++;
                    liquids.Dequeue();
                    ingredients.Pop();
                }
                else if (sum == 100)
                {
                    fruitPieCount++;
                    liquids.Dequeue();
                    ingredients.Pop();
                }
                else
                {
                    ingredient += 3;
                    liquids.Dequeue();
                    ingredients.Pop();
                    ingredients.Push(ingredient);
                }
            }

            if (breadCount >= 1 && cakeCount >= 1 && pastryCount >= 1 && fruitPieCount >= 1)
            {
                Console.WriteLine("Wohoo! You succeeded in cooking all the food!");
            }
            else
            {
                Console.WriteLine("Ugh, what a pity! You didn't have enough materials to cook everything.");
            }

            if (liquids.Count == 0)
            {
                Console.WriteLine("Liquids left: none");
            }
            else
            {
                Console.WriteLine($"Liquids left: {string.Join(", ", liquids)}");
            }

            if (ingredients.Count == 0)
            {
                Console.WriteLine("Ingredients left: none");
            }
            else
            {
                Console.WriteLine($"Ingredients left: {string.Join(", ", ingredients)}");
            }

            Console.WriteLine($"Bread: {breadCount}");
            Console.WriteLine($"Cake: {cakeCount}");
            Console.WriteLine($"Fruit Pie: {fruitPieCount}");
            Console.WriteLine($"Pastry: {pastryCount}");
        }
    }
}
