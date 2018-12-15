namespace CartsAndTracks.Lib
{
    public class Coordinates
    {
        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; private set; }
        public int Y { get; private set; }

        public string ToKey()
        {
            return $"[x:{X}-y:{Y}]";
        }

        public override bool Equals(object point)
        {
            return ((Coordinates)point).X.Equals(X) && ((Coordinates)point).Y.Equals(Y);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public Coordinates MoveTowards(Heading direction)
        {
            switch (direction)
            {
                case Heading.North:
                    return new Coordinates(X--, Y);
                case Heading.South:
                    return new Coordinates(X++, Y);
                case Heading.East:
                    return new Coordinates(X, Y++);
                case Heading.West:
                    return new Coordinates(X, Y--);
                default:
                    throw new System.Exception($"Connot move in [{direction}] direction.");
            }
        }
    }
}
