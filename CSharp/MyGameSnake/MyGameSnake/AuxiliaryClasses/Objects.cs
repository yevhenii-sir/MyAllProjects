namespace MyGameSnake.AuxiliaryClasses
{
    public abstract class Objects
    {
        private (int x, int y) _coordinates;
        
        public (int x, int y) Coordinates
        {
            get => _coordinates;
            set => _coordinates = value;
        }

        protected Objects(int x, int y)
        {
            _coordinates.x = x;
            _coordinates.y = y;
        }
    }
}