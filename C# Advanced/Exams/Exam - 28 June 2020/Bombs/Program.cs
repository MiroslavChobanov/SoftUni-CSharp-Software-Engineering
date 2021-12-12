using System;
using System.Collections.Generic;
using System.Linq;

namespace Bombs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var bombEffectsInput = Console.ReadLine()
                .Split(", ")
                .Select(int.Parse)
                .ToArray();
            var bombCasingsInput = Console.ReadLine()
                .Split(", ")
                .Select(int.Parse)
                .ToArray();


            var bombEffects = new Queue<int>(bombEffectsInput);
            var bombCasings = new Stack<int>(bombCasingsInput);

            int sum = 0;

            int daturaBombsCount = 0;
            int cherryBombsCount = 0;
            int smokeDecoyBombsCount = 0;


            while (bombEffects.Count > 0 && bombCasings.Count > 0)
            {
                var bombEffect = bombEffects.Dequeue();
                var bombCasing = bombCasings.Pop();

                sum = bombEffect + bombCasing;
                
                if (sum == 40)
                {
                    daturaBombsCount++;
                }
                else if (sum == 60)
                {
                    cherryBombsCount++;
                }
                else if (sum == 120)
                {
                    smokeDecoyBombsCount++;
                }
                else
                {
                    bombCasing -= 5;
                    while (true)
                    {
                        if (sum == 40)
                        {
                            daturaBombsCount++;
                            break;
                        }
                        else if (sum == 60)
                        {
                            cherryBombsCount++;
                            break;
                        }
                        else if (sum == 120)
                        {
                            smokeDecoyBombsCount++;
                            break;
                        }
                        else
                        {
                            bombCasing -= 5;
                        }
                    }
                }

                if (daturaBombsCount >= 3 && cherryBombsCount >= 3 && smokeDecoyBombsCount >= 3)
                {
                    Console.WriteLine("Bene! You have successfully filled the bomb pouch!");
                    break;
                }
            }

            if (daturaBombsCount < 3 || cherryBombsCount < 3 || smokeDecoyBombsCount < 3)
            {
                Console.WriteLine("You don't have enough materials to fill the bomb pouch.");
            }

            if (bombEffects.Count > 0)
            {
                Console.WriteLine($"Bomb Effects: {string.Join(", ", bombEffects)}");
            }
            else
            {
                Console.WriteLine("Bomb Effects: empty");
            }

            if (bombCasings.Count > 0)
            {
                Console.WriteLine($"Bomb Casings: {string.Join(", ", bombCasings)}");
            }
            else
            {
                Console.WriteLine("Bomb Casings: empty");
            }

            Console.WriteLine($"Cherry Bombs: {cherryBombsCount}");
            Console.WriteLine($"Datura Bombs: {daturaBombsCount}");
            Console.WriteLine($"Smoke Decoy Bombs: {smokeDecoyBombsCount}");
        }
    }
}
