using MyGameSnake.AuxiliaryClasses;

namespace MyGameSnake.GameObjects
{
    public class Apple : Objects
    {
        public static char AppleSymbol { get; private set; } = '+';

        #region Constructors

        /// <summary>
        /// Creating an apple object in the middle of the area.
        /// </summary>
        /// <param name="gameArea">Area object to take coordinates.</param>
        public Apple(GameArea gameArea) : this(gameArea.Size / 2, gameArea.Size) { }

        /// <summary>
        /// Creating an Apple object at the specified coordinates.
        /// </summary>
        /// <param name="x">Coordinate X</param>
        /// <param name="y">Coordinate Y</param>
        public Apple(int x, int y) : base(x, y) { }
        
        #endregion
        
        
        /// <summary>
        /// Generating new coordinates for the apple.
        /// </summary>
        /// <param name="combineGameArea">Matrix of current locations of all objects.</param>
        public void GetNewCoordinates(char[,] combineGameArea)
        {
            Coordinates = GameManager.GenerateRandomPosition(combineGameArea);
        }

    }
}