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
            if (!IsCrashed)
            {
                char shape = '\0';
                shape = DrawCart(shape);
                return shape.ToString(); 
            }
            return "X";
        }

        private char DrawCart(char shape)
        {
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

            return shape;
        }

        public Heading CurrentHeading { get; private set; }
        public Coordinates CurrentPosition { get; private set; }
        public bool IsCrashed { get; private set; }

        public void Move(Track t)
        {
            Move(CurrentHeading);
            if (!IsPaved(t)) throw new System.Exception($"Bad location [x: {CurrentPosition.X}  y: {CurrentPosition.Y}]");
        }
        public void Crash()
        {
            IsCrashed = true;
        }
        private bool IsPaved(Track t)
        {
            return ! (t.Grid[CurrentPosition.X, CurrentPosition.Y].Render().Equals(" "));
        }
        private void Move(Heading h, int steps = 1)
        {
            if (!IsCrashed)
            {
                CurrentHeading = h;
                var x = CurrentPosition.X; var y = CurrentPosition.Y;
                switch (h)
                {
                    case Heading.North:
                        x--;
                        CurrentPosition = new Coordinates(x, y);
                        break;
                    case Heading.South:
                        x++;
                        CurrentPosition = new Coordinates(x, y);
                        break;
                    case Heading.East:
                        y++;
                        CurrentPosition = new Coordinates(x, y);
                        break;
                    case Heading.West:
                        y--;
                        CurrentPosition = new Coordinates(x, y);
                        break;
                } 
            }
        }
    }
}
