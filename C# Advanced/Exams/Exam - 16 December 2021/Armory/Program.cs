using System;

namespace Armory
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            char[,] matrix = new char[n, n];

            int officerRow = 0;
            int officerCol = 0;

            int coins = 0;

            int firstMirrorRow = 0;
            int firstMirrorCol = 0;
            int secondMirrorRow = 0;
            int secondMirrorCol = 0;


            for (int row = 0; row < n; row++)
            {
                string currentRow = Console.ReadLine();
                for (int col = 0; col < currentRow.Length; col++)
                {
                    matrix[row, col] = currentRow[col];

                    if (matrix[row, col] == 'A')
                    {
                        officerRow = row;
                        officerCol = col;
                    }

                    if (matrix[row, col] == 'M')
                    {

                        if (firstMirrorRow == 0)
                        {
                            firstMirrorRow = row;
                            firstMirrorCol = col;
                        }
                        else
                        {
                            secondMirrorRow = row;
                            secondMirrorCol = col;
                        }
                    }
                }
            }


            while (true)
            {
                string input = Console.ReadLine();

                matrix[officerRow, officerCol] = '-';

                officerRow = MoveRow(officerRow, input);
                officerCol = MoveCol(officerCol, input);

                if (officerRow < 0 || officerRow >= n || officerCol < 0 || officerCol >= n)
                {
                    Console.WriteLine("I do not need more swords!");
                    break;
                }

                if (char.IsDigit(matrix[officerRow,officerCol]))
                {
                    int diff = matrix[officerRow, officerCol] - '0';
                    coins += diff;
                }

                if (matrix[officerRow, officerCol] == 'M')
                {
                    matrix[officerRow, officerCol] = '-';
                    if (firstMirrorRow == officerRow && firstMirrorCol == officerCol)
                    {
                        officerRow = secondMirrorRow;
                        officerCol = secondMirrorCol;
                    }
                    else
                    {
                        officerRow = firstMirrorRow;
                        officerCol = firstMirrorCol;
                    }
                }

                matrix[officerRow, officerCol] = 'A';

                if (coins >= 65)
                {
                    Console.WriteLine("Very nice swords, I will come back for more!");
                    break;
                }
            }
            Console.WriteLine($"The king paid {coins} gold coins.");

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
