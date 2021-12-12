using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Scheduling
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var taskInput = Console.ReadLine()
                .Split(", ")
                .Select(int.Parse)
                .ToArray();

            var threadInput = Console.ReadLine()
                .Split(" ")
                .Select(int.Parse)
                .ToArray();

            var taskToBeKilled = int.Parse(Console.ReadLine());

            var tasks = new Stack<int>(taskInput);
            var threads = new Queue<int>(threadInput);
            ;
            while (tasks.Count > 0 && threads.Count > 0)
            {
                var task = tasks.Peek();
                var thread = threads.Peek();
                if (thread >= task)
                {
                    tasks.Pop();
                    if (task == taskToBeKilled)
                    {
                        Console.WriteLine($"Thread with value {thread} killed task {taskToBeKilled}");
                        break;
                    }
                    else
                    {
                        threads.Dequeue();
                    }
                    
                }
                else
                {
                    if (task == taskToBeKilled)
                    {
                        Console.WriteLine($"Thread with value {thread} killed task {taskToBeKilled}");
                        break;
                    }
                    else
                    {
                        threads.Dequeue();
                    }
                }
            }

            Console.WriteLine(string.Join(" ", threads));
        }
    }
}
