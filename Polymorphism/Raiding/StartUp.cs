using System;
using System.Collections.Generic;
using System.Linq;

namespace Raiding
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int numberOfHeroes = int.Parse(Console.ReadLine());
            List<BaseHero> heroes = new List<BaseHero>();

            ;
            while(heroes.Count != numberOfHeroes)
            {
                try
                {
                    string name = Console.ReadLine();
                    string heroType = Console.ReadLine();

                    if (heroType == "Paladin")
                    {
                        heroes.Add(new Paladin(name));
                    }
                    else if (heroType == "Druid")
                    {
                        heroes.Add(new Druid(name));
                    }
                    else if (heroType == "Warrior")
                    {
                        heroes.Add(new Warrior(name));
                    }
                    else if (heroType == "Rogue")
                    {
                        heroes.Add(new Rogue(name));
                    }
                    else
                    {
                        throw new ArgumentException("Ïnvalid hero!");
                    }

                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            int bossPower = int.Parse(Console.ReadLine());

            foreach (var hero in heroes)
            {
                Console.WriteLine(hero.CastAbility());
            }

            int heroesPower = heroes.Sum(x => x.Power);

            if (heroesPower >= bossPower)
            {
                Console.WriteLine("Victory!");
            }
            else
            {
                Console.WriteLine("Defeat...");
            }
        }
    }
}
