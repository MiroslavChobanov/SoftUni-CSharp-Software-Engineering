using System;

namespace Snake
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

			char[,] matrix = new char[n, n];

			int snakeRow = 0;
			int snakeCol = 0;

            int firstBurrowRow = 0;
            int firstBurrowCol = 0;
            int secondBurrowRow = 0;
            int secondBurrowCol = 0;

            int foodQuantity = 0;

			for (int row = 0; row < n; row++)
			{
				string currentRow = Console.ReadLine();
				for (int col = 0; col < currentRow.Length; col++)
				{
					matrix[row, col] = currentRow[col];

					if (matrix[row, col] == 'S')
					{
						snakeRow = row;
						snakeCol = col;
					}

                    if (matrix[row, col] == 'B')
                    {
                        if (firstBurrowRow == 0)
                        {
                            firstBurrowRow = row;
                            firstBurrowCol = col;
                        }
                        else
                        {
                            secondBurrowRow = row;
                            secondBurrowCol = col;
                        }
                    }
                }
			}

            while (true)
            {
				string command = Console.ReadLine();

				matrix[snakeRow, snakeCol] = '.';

                snakeRow = MoveRow(snakeRow, command);
                snakeCol = MoveCol(snakeCol, command);

                if (snakeRow < 0 || snakeCol < 0 || snakeRow >= n || snakeCol >= n)
                {
                    Console.WriteLine("Game over!");
                    break;
                }

                if (matrix[snakeRow, snakeCol] == '*')
                {
                    foodQuantity++;
                }

                if (matrix[snakeRow, snakeCol] == 'B')
                {
                    matrix[snakeRow, snakeCol] = '.';
                    if (firstBurrowRow == snakeRow && firstBurrowCol == snakeCol)
                    {
                        snakeRow = secondBurrowRow;
                        snakeCol = secondBurrowCol;
                    }
                    else
                    {
                        snakeRow = firstBurrowRow;
                        snakeCol = firstBurrowCol;
                    }
                }

                matrix[snakeRow, snakeCol] = 'S';

                if (foodQuantity >= 10)
                {
                    Console.WriteLine("You won! You fed the snake.");
                    break;
                }

                
            }

            Console.WriteLine($"Food eaten: {foodQuantity}");

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
