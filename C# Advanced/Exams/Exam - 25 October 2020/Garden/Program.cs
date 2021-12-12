using System;
using System.Linq;

namespace Garden
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var dimensions = Console.ReadLine()
                .Split(" ")
                .Select(int.Parse)
                .ToArray();

            var n = dimensions[0];
            var m = dimensions[1];

            var flowerRow = 0;
            var flowerCol = 0;

            var matrix = new int[n, m];
            ;
            for (int row = 0; row < n; row++)
            {
                for (int col = 0; col < m; col++)
                {
                    matrix[row, col] = 0;
                }
            }
            var input = Console.ReadLine();
            while (input != "Bloom Bloom Plow")
            {
                var flowerPositions = input.Split(" ").Select(int.Parse).ToArray();

                var currentRow = flowerPositions[0];
                var currentCol = flowerPositions[1];


                if (currentRow < 0 || currentRow >= n || currentCol < 0 || currentCol >= m)
                {
                    Console.WriteLine("Invalid coordinates.");
                }

                flowerRow = currentRow;
                flowerCol = currentCol;


                    for (int row = 0; row < matrix.GetLength(0); row++)
                    {
                        for (int col = 0; col < matrix.GetLength(1); col++)
                        {
                            if (row == flowerRow || col == flowerCol)
                            {
                                matrix[row, col] += 1;
                            }
                        }
                    }

                input = Console.ReadLine();
            }


                //printing the matrix
                for (int row = 0; row < matrix.GetLength(0); row++)
                {
                    for (int col = 0; col < matrix.GetLength(1); col++)
                    {
                        Console.Write(matrix[row, col] + " ");
                    }

                    Console.WriteLine();
                }
        }
    }
}

