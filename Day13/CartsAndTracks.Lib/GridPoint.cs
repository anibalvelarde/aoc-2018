namespace CartsAndTracks.Lib
{
    public abstract class GridPoint
    {
        public GridPoint(int x, int y)
        {
            Point = new Coordinates(x, y);
        }

        public Coordinates Point { get; private set; }
        public abstract string Render();
        public abstract bool IsPavedWhenHeading(Heading direction);
    }
}
