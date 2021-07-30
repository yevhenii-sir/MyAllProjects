using System;
using MyGameSnake.GameObjects;

namespace MyGameSnake.AuxiliaryClasses
{
    public static class GameManager
    {
        /// <summary>
        /// Initial inscription.
        /// </summary>
        public static void StartSplashScreen()
        {
            Console.WriteLine($"To exit during the game, press the {MainClass.ExitConsoleKey} button.");
            Console.WriteLine("Press any button other than power off to continue...");
            Console.ReadKey(true);
            Console.Clear();
        }
        
        /// <summary>
        /// Generation of random coordinates taking into
        /// account the current arrangement of the elements.
        /// </summary>
        /// <param name="combineGameArea">An array with the current location of all components.</param>
        /// <returns>Generated coordinates like (int x, int y).</returns>
        public static (int x, int y) GenerateRandomPosition(char[,] combineGameArea)
        {
            Random random = new Random();

            for (int i = 0; i < combineGameArea.GetLength(0) * combineGameArea.GetLength(1); i++)
            {
                var x = random.Next(combineGameArea.GetLength(0));
                var y = random.Next(combineGameArea.GetLength(1));

                if (combineGameArea[x, y] == '\0')
                {
                    return (x, y);
                }
            }
            return (0, 0);
        }
        
        /// <summary>
        /// Checking whether the snake's head is in the place of the apple,
        /// if so, then a segment is added to the snake, and the coordinates
        /// of the apple are randomly generated.
        /// </summary>
        /// <param name="snake, apple">To compare their coordinates.</param>
        /// <param name="combineGameArea">To pass to the GetNewCoordinates method.</param>
        public static bool MeetApple(Apple apple, Snake snake, char[,] combineGameArea)
        {
            if (snake.Coordinates == apple.Coordinates)
            {
                snake.AddSegment();
                apple.GetNewCoordinates(combineGameArea);
                return true;
            }

            return false;
        }
        
        /// <summary>
        /// Connecting all components into one matrix.
        /// </summary>
        /// <param name="gameArea">Obtaining the basis on which will be wall.</param>
        /// <param name="snake, apple">Outputting their coordinates to an matrix.</param>
        public static char[,] CombineObjects(GameArea gameArea, Snake snake, Apple apple)
        {
            var area = gameArea.DrawWall();

            area[snake.SnakeCoordinates[0].x, snake.SnakeCoordinates[0].y] = Snake.HeadSymbol;
            for (int i = 1; i < snake.SnakeCoordinates.Count; i++)
            {
                area[snake.SnakeCoordinates[i].x, snake.SnakeCoordinates[i].y] = Snake.BodySymbol;
            }

            area[apple.Coordinates.x, apple.Coordinates.y] = Apple.AppleSymbol;

            return area;
        }
        
        /// <summary>
        /// Displaying an matrix with all components.
        /// </summary>
        /// <param name="gameArea, snake, apple">To call CombineObjects () method.</param>
        public static char[,] DrawAreaInConsole(GameArea gameArea, Snake snake, Apple apple)
        {
            var tempArea = CombineObjects(gameArea, snake, apple);

            for (int i = 0; i < tempArea.GetLength(0); i++)
            {
                for (int j = 0; j < tempArea.GetLength(1); j++)
                {
                    Console.Write((tempArea[i, j] != '\0') ? tempArea[i, j] : GameArea.EmptinessSymbol);
                }

                Console.WriteLine();
            }

            return tempArea;
        }
        
        /// <summary>
        /// Сhanging the current direction of the snake.
        /// </summary>
        /// <param name="keyInfo">The button that was pressed.</param>
        /// <param name="snake">The snake itself in which to change direction.</param>
        public static void ChangeCurrentDirection(ConsoleKeyInfo keyInfo, Snake snake)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.W:
                    snake.CurrentDirection = SnakeDirections.Down;
                    break;
                case ConsoleKey.S:
                    snake.CurrentDirection = SnakeDirections.Up;
                    break;
                case ConsoleKey.A:
                    snake.CurrentDirection = SnakeDirections.Left;
                    break;
                case ConsoleKey.D:
                    snake.CurrentDirection = SnakeDirections.Right;
                    break;
            }
        }
    }
}