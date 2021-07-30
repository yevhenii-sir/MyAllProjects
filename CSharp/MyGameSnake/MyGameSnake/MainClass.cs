using System;
using System.Threading;
using MyGameSnake.AuxiliaryClasses;
using MyGameSnake.GameObjects;

namespace MyGameSnake
{
    static class MainClass
    {
        public static ConsoleKey ExitConsoleKey = ConsoleKey.F;
        
        static void Main(string[] args)
        {
            GameManager.StartSplashScreen();

            #region Initialization

            ConsoleKeyInfo keyInfo;
            GameArea gameArea = new GameArea();
            
            Snake snake = new Snake(7, 13);
            snake.OnHookedMyself += Handlers.PlayerHookedMyself;
            snake.OnHookedWall += Handlers.PlayerHookedWall;
            snake.OnPlayerWon += Handlers.PlayerWon;
            
            Apple apple = new Apple(gameArea);

            #endregion
            
            #region ControlsAndVerification

            do
            {
                while (Console.KeyAvailable == false)
                {
                    snake.Move();
                    GameManager.MeetApple(apple, snake, GameManager.DrawAreaInConsole(gameArea, snake, apple));
                    Console.WriteLine("Points: " + snake.Points);

                    Thread.Sleep(150);
                    Console.Clear();
                    
                    if (snake.OnHookedMyselfEvent() || 
                        snake.OnHookedWallEvent(gameArea) || 
                        snake.OnPlayerWonEvent(gameArea)) return;
                }
                
                keyInfo = Console.ReadKey(true);
                GameManager.ChangeCurrentDirection(keyInfo, snake);
            } while (keyInfo.Key != ExitConsoleKey);

            #endregion
        }
    }
}