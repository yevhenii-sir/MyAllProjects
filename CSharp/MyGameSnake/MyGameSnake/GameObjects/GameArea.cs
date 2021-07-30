namespace MyGameSnake.GameObjects
{
    public class GameArea
    {
        public static char WallSymbol { get; private set; } = '#';
        public static char EmptinessSymbol { get; private set; } = ' ';

        public int FreePlace { get; private set; }

        public int WallWidth { get; private set; } = 1;
        public int Size { get; private set; } = 15;

        #region Constructors

        /// <summary>
        /// Creating a GameArea object with standard parameters.
        /// </summary>
        public GameArea() { }

        /// <summary>Create GameArea object with given parameters.</summary>
        /// <param name="size">Area size [size, size * 2]</param>
        public GameArea(int size, int wallWidth = 1, char wallSymbol = '#', char emptinessSymbol = ' ')
        {
            Size = size;
            WallWidth = wallWidth;
            WallSymbol = wallSymbol;
            EmptinessSymbol = emptinessSymbol;
        }

        #endregion

        /// <summary>
        /// Creation of a new matrix with placement of walls in it.
        /// </summary>
        public char[,] DrawWall()
        {
            FreePlace = 0;
            char[,] tempArea = new char[Size, Size * 2];

            for (int i = 0; i < tempArea.GetLength(0); i++)
            {
                for (int j = 0; j < tempArea.GetLength(1); j++)
                {
                    //horizontal stripes
                    if (i <= WallWidth - 1 || tempArea.GetLength(0) - WallWidth <= i)
                        tempArea[i, j] = WallSymbol;
                    //vertical stripes
                    else if (j <= WallWidth - 1 || tempArea.GetLength(1) - WallWidth <= j)
                        tempArea[i, j] = WallSymbol;
                    else FreePlace++;
                }
            }

            return tempArea;
        }
    }
}