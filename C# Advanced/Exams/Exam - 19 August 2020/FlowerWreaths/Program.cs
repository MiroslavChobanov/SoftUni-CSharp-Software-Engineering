using System;
using System.Collections.Generic;
using System.Linq;

namespace FlowerWreaths
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var rosesInput = Console.ReadLine()
                .Split(", ")
                .Select(int.Parse)
                .ToArray();

            var liliesInput = Console.ReadLine()
                .Split(", ")
                .Select(int.Parse)
                .ToArray();

            var roses = new Stack<int>(rosesInput);
            var lilies = new Queue<int>(liliesInput);

            var wreaths = 0;
            var flowersLeft = 0;
            
            while (roses.Count > 0 && lilies.Count > 0)
            {
                var rose = roses.Pop();
                var lily = lilies.Dequeue();
                var sum = rose + lily;

                if (sum == 15)
                {
                    wreaths++;
                }
                else if(sum > 15)
                {
                    lily -= 2;
                    while (true)
                    {
                        
                        if (rose + lily == 15)
                        {
                            wreaths++;
                            break;
                        }
                        else if (rose + lily > 15)
                        {
                            lily -= 2;
                        }
                        else
                        {
                            flowersLeft += rose + lily;
                            break;
                        }
                    }
                    
                }
                else
                {
                    flowersLeft += sum;
                }
            }

            wreaths += flowersLeft / 15;

            if (wreaths >= 5)
            {
                Console.WriteLine($"You made it, you are going to the competition with {wreaths} wreaths!");
            }
            else
            {
                Console.WriteLine($"You didn't make it, you need {5 - wreaths} wreaths more!");
            }
        }
    }
}
