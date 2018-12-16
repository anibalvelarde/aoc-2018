namespace CartsAndTracks.Lib
{
    public class Coordinates
    {
        public Coordinates(int col, int row)
        {
            Col = col;
            Row = row;
        }

        public int Col { get; private set; }
        public int Row { get; private set; }

        public string ToKey()
        {
            return $"[x:{Col}-y:{Row}]";
        }

        public override bool Equals(object point)
        {
            return ((Coordinates)point).Col.Equals(Col) && ((Coordinates)point).Row.Equals(Row);
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
                    return new Coordinates(Col--, Row);
                case Heading.South:
                    return new Coordinates(Col++, Row);
                case Heading.East:
                    return new Coordinates(Col, Row++);
                case Heading.West:
                    return new Coordinates(Col, Row--);
                default:
                    throw new System.Exception($"Connot move in [{direction}] direction.");
            }
        }
    }
}
