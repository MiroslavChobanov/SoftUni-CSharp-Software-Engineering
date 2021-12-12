using System;

namespace Bee
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            int pollinatedFlowers = 0;

            char[,] matrix = new char[n, n];
            int beeRow = 0;
            int beeCol = 0;
            
            for (int row = 0; row < n; row++)
            {
                string currentRow = Console.ReadLine();
                for (int col = 0; col < currentRow.Length; col++)
                {
                    matrix[row, col] = currentRow[col];

                    if (matrix[row, col] == 'B')
                    {
                        beeRow = row;
                        beeCol = col;
                    }
                }
            }

            while (true)
            {
                string command = Console.ReadLine();

                if (command == "End")
                {
                    break;
                }

                matrix[beeRow, beeCol] = '.';

                beeRow = MoveRow(beeRow, command);
                beeCol = MoveCol(beeCol, command);

                if (beeRow < 0 || beeCol < 0 || beeRow >= n || beeCol >= n)
                {
                    Console.WriteLine("The bee got lost!");
                    break;
                }

                if (matrix[beeRow, beeCol] == 'f')
                {
                    pollinatedFlowers++;
                }

                if (matrix[beeRow, beeCol] == 'O')
                {
                    matrix[beeRow, beeCol] = '.';
                    beeRow = MoveRow(beeRow, command);
                    beeCol = MoveCol(beeCol, command);
                    if (beeRow < 0 || beeCol < 0 || beeRow >= n || beeCol >= n)
                    {
                        Console.WriteLine("The bee got lost!");
                        break;
                    }

                    if (matrix[beeRow, beeCol] == 'f')
                    {
                        pollinatedFlowers++;
                    }
                }
                    matrix[beeRow, beeCol] = 'B';
            }
            


            if (pollinatedFlowers >= 5)
            {
                Console.WriteLine($"Great job, the bee managed to pollinate {pollinatedFlowers} flowers!");
            }
            else
            {
                Console.WriteLine($"The bee couldn't pollinate the flowers, she needed {5 - pollinatedFlowers} flowers more");
            }

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write(matrix[row, col]);
                }

                Console.WriteLine();
            }
        }

        public static int MoveRow(int row, string movement)
        {
            if (movement == "up")
            {
                return row - 1;
            }
            if (movement == "down")
            {
                return row + 1;
            }

            return row;
        }

        public static int MoveCol(int col, string movement)
        {
            if (movement == "left")
            {
                return col - 1;
            }
            if (movement == "right")
            {
                return col + 1;
            }

            return col;
        }
    }
}
