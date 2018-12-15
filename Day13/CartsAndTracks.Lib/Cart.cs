namespace CartsAndTracks.Lib
{
    public class Cart
    {
        public Cart(Heading h, int x, int y)
        {
            CurrentPosition = new Coordinates(x, y);
            CurrentHeading = h;
        }

        public string Render()
        {
            char shape = '\0';
            switch (CurrentHeading)
            {
                case Heading.North:
                    shape = '^';
                    break;
                case Heading.South:
                    shape = 'v';
                    break;
                case Heading.East:
                    shape = '>';
                    break;
                case Heading.West:
                    shape = '<';
                    break;
                default:
                    break;
            }
            return shape.ToString();
        }
        public Heading CurrentHeading { get; private set; }
        public Coordinates CurrentPosition { get; private set; }
        public void Move(Heading h, int steps = 1)
        {
            CurrentHeading = h;
            var x = CurrentPosition.X; var y = CurrentPosition.Y;
            switch (h)
            {
                case Heading.North:
                    CurrentPosition = new Coordinates(x--, y);
                    break;
                case Heading.South:
                    CurrentPosition = new Coordinates(x++, y);
                    break;
                case Heading.East:
                    CurrentPosition = new Coordinates(x, y++);
                    break;
                case Heading.West:
                    CurrentPosition = new Coordinates(x, y--);
                    break;
            }
        }
    }
}
