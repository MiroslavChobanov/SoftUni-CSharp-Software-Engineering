using System;
using System.Collections.Generic;
using System.Linq;

namespace StackAndQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            var steelInput = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
            var carbonInput = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

            var steel = new Queue<int>(steelInput);
            var carbon = new Stack<int>(carbonInput);

            int sum = 0;

            int gladiusCount = 0;
            int shamshirCount = 0;
            int katanaCount = 0;
            int sabreCount = 0;
            int broadswordCount = 0;

            int totalSwordCount = 0;
            var dict = new Dictionary<string, int>();
            dict.Add("Gladius", 0);
            dict.Add("Shamshir", 0);
            dict.Add("Katana", 0);
            dict.Add("Sabre", 0);
            dict.Add("Broadsword", 0);
            ;
            while (steel.Count > 0 && carbon.Count > 0)
            {
                var pieceOfSteel = steel.Peek();
                var pieceOfCarbon = carbon.Peek();

                sum = pieceOfSteel + pieceOfCarbon;

                if (sum == 70)
                {
                    gladiusCount++;
                    dict["Gladius"]++;
                    steel.Dequeue();
                    carbon.Pop();
                }
                else if (sum == 80)
                {
                    shamshirCount++;
                    dict["Shamshir"]++;

                    steel.Dequeue();
                    carbon.Pop();
                }
                else if (sum == 90)
                {
                    katanaCount++;
                    dict["Katana"]++;

                    steel.Dequeue();
                    carbon.Pop();
                }
                else if (sum == 110)
                {
                    sabreCount++;
                    dict["Sabre"]++;

                    steel.Dequeue();
                    carbon.Pop();
                }
                else if (sum == 150)
                {
                    broadswordCount++;
                    dict["Broadsword"]++;

                    steel.Dequeue();
                    carbon.Pop();
                }
                else
                {
                    pieceOfCarbon += 5;
                    steel.Dequeue();
                    carbon.Pop();
                    carbon.Push(pieceOfCarbon);
                }

                totalSwordCount = shamshirCount + gladiusCount + katanaCount + sabreCount + broadswordCount;
            }

            if (totalSwordCount > 0)
            {
                Console.WriteLine($"You have forged {totalSwordCount} swords.");
            }
            else
            {
                Console.WriteLine("You did not have enough resources to forge a sword.");
            }

            if (steel.Count == 0)
            {
                Console.WriteLine("Steel left: none");
            }
            else
            {
                Console.WriteLine($"Steel left: {string.Join(", ", steel)}");
            }

            if (carbon.Count == 0)
            {
                Console.WriteLine("Carbon left: none");
            }
            else
            {
                Console.WriteLine($"Carbon left: {string.Join(", ", carbon)}");
            }
            
            foreach (var item in dict.Where(x => x.Value > 0).OrderBy(x => x.Key))
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
        }
    }
}