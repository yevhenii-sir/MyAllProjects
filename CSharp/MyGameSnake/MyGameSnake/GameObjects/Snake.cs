using System;
using System.Collections.Generic;
using MyGameSnake.AuxiliaryClasses;

namespace MyGameSnake.GameObjects
{
    public class Snake : Objects
    {
        public int Points => SnakeCoordinates.Count - 1;

        public SnakeDirections CurrentDirection { get; set; } = SnakeDirections.Up;
        
        public static char HeadSymbol { get; private set; } = '@';
        public static char BodySymbol { get; private set; } = '*';

        public List<(int x, int y)> SnakeCoordinates = new List<(int x, int y)>();
        
        public event EventHandler OnHookedMyself;
        public event EventHandler OnHookedWall;
        public event EventHandler OnPlayerWon;

        #region Constructors
        
        /// <summary>
        /// Creating a Snake object in the middle of the area.
        /// </summary>
        /// <param name="gameArea">initialized play area.</param>
        public Snake(GameArea gameArea) : this((gameArea.Size / 2) - 2, gameArea.Size)
        {
            SnakeCoordinates.Add((gameArea.Size / 2, gameArea.Size));
        }
        
        /// <summary>
        /// Creation of a Snake object with the specified coordinates and other parameters.
        /// </summary>
        /// <param name="x">Coordinate X</param>
        /// <param name="y">Coordinate Y</param>
        public Snake(int x, int y, char headSymbol = '@', char bodySymbol = '*') : base(x, y)
        {
            SnakeCoordinates.Add((x, y));
            HeadSymbol = headSymbol;
            BodySymbol = bodySymbol;
        }
        #endregion

        #region Events
        
        /// <summary>
        /// Method for triggering an event at the moment when
        /// the coordinates of the head and body are the same.
        /// </summary>
        /// <returns>Did the coordinates match.</returns>
        public bool OnHookedMyselfEvent()
        {
            for (int i = 2; i < SnakeCoordinates.Count; i++)
                if (Coordinates == SnakeCoordinates[i])
                {
                    OnHookedMyself?.Invoke(this, EventArgs.Empty);
                    return true;
                }

            return false;
        }

        /// <summary>
        /// Method for triggering an event at the moment when the coordinates
        /// of the head are included in the coordinates of the wall.
        /// </summary>
        /// <param name="gameArea">To get the dimensions of the area.</param>
        /// <returns>Did the coordinates match.</returns>
        public bool OnHookedWallEvent(GameArea gameArea)
        {
            if (!(Coordinates.x > gameArea.WallWidth-1 && Coordinates.x < gameArea.Size - gameArea.WallWidth) ||
                !(Coordinates.y > gameArea.WallWidth-1 && Coordinates.y < (gameArea.Size * 2) - gameArea.WallWidth))
            {
                OnHookedWall?.Invoke(this, EventArgs.Empty);
                return true;
            }

            return false;
        }
        
        /// <summary>
        /// The method of calling an event, at a time when there is as much
        /// free space on the area as the number of segments in the snake.
        /// </summary>
        /// <param name="gameArea">For information about free space.</param>
        /// <returns>Did the value match.</returns>
        public bool OnPlayerWonEvent(GameArea gameArea)
        {
            if (SnakeCoordinates.Count == gameArea.FreePlace)
            {
                OnPlayerWon?.Invoke(this, EventArgs.Empty);
                return true;
            }
            
            return false;
        }
        
        #endregion
        
        /// <summary>
        /// Adding a segment for the snake. The first
        /// element from the end is taken to add.
        /// </summary>
        public void AddSegment()
        {
            SnakeCoordinates.Add(SnakeCoordinates[^1]);
        }

        /// <summary>
        /// Shift all coordinates to the right.
        /// </summary>
        private void CoordinatesShift()
        {
            for (int i = SnakeCoordinates.Count - 1; i > 0; i--)
            {
                SnakeCoordinates[i] = SnakeCoordinates[i - 1];
            }
        }
        
        /// <summary>
        /// Behind the standard direction of the snake gives out
        /// how we need to change the coordinates of its head.
        /// </summary>
        private (int x, int y) StandardOffsets()
        {
            switch (CurrentDirection)
            {
                case SnakeDirections.Up:
                    return (1, 0);
                case SnakeDirections.Down:
                    return (-1, 0);
                case SnakeDirections.Right:
                    return (0, 1);
                case SnakeDirections.Left:
                    return (0, -1);
            }
            return (0, 0); //never used
        }

        /// <summary>
        /// Moving the snake in the current direction,
        /// and save the coordinates of the head.
        /// </summary>
        public void Move()
        {
            CoordinatesShift();
            
            var (x, y) = StandardOffsets();
            SnakeCoordinates[0] = (SnakeCoordinates[0].x + x, SnakeCoordinates[0].y + y);
            
            Coordinates = SnakeCoordinates[0];
        }
    }
}