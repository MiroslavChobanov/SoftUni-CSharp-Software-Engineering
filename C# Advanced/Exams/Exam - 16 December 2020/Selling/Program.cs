using System;

namespace Selling
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            char[,] matrix = new char[n, n];

            int currRow = 0;
            int currCol = 0;

            int firstPillarRow = 0;
            int firstPillarCol = 0;
            int secondPillarRow = 0;
            int secondPillarCol = 0;

            int money = 0;
            
            for (int row = 0; row < n; row++)
            {
                string currentRow = Console.ReadLine();
                for (int col = 0; col < currentRow.Length; col++)
                {
                    matrix[row, col] = currentRow[col];

                    if (matrix[row, col] == 'S')
                    {
                        currRow = row;
                        currCol = col;
                    }

                    if (matrix[row, col] == 'O')
                    {
                        
                        if (firstPillarRow == 0)
                        {
                            firstPillarRow = row;
                            firstPillarCol = col;
                        }
                        else
                        {
                            secondPillarRow = row;
                            secondPillarCol = col;
                        }
                    }

                    
                }
            }
            ;
            while (true)
            {
                string input = Console.ReadLine();

                matrix[currRow, currCol] = '-';

                currRow = MoveRow(currRow, input);
                currCol = MoveCol(currCol, input);

                if (currRow < 0 || currRow >= n || currCol < 0 || currCol >= n)
                {
                    Console.WriteLine("Bad news, you are out of the bakery.");
                    break;
                }

                if (char.IsDigit(matrix[currRow, currCol]))
                {
                    int diff = matrix[currRow, currCol] - '0';
                    money += diff;
                }

                if (matrix[currRow, currCol] == 'O')
                {
                    matrix[currRow, currCol] = '-';
                    if (firstPillarRow == currRow && firstPillarCol == currCol)
                    {
                        currRow = secondPillarRow;
                        currCol = secondPillarCol;
                    }
                    else
                    {
                        currRow = firstPillarRow;
                        currCol = firstPillarCol;
                    }
                }

                matrix[currRow, currCol] = 'S';

                if (money >= 50)
                {
                    Console.WriteLine("Good news! You succeeded in collecting enough money!");
                    break;
                }
            }

            Console.WriteLine($"Money: {money}");

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
