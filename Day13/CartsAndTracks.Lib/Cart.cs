using System;
using System.Collections.Generic;

namespace CartsAndTracks.Lib
{
    public class Cart
    {
        public Cart(Heading h, int x, int y)
        {
            CurrentPosition = new Coordinates(x, y);
            CurrentHeading = h;
            foreach (var elem in _turnSequence)
            {
                _turnQueue.Enqueue(elem);
            }
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
        private char[] _turnSequence = { 'L', 'S', 'R' };
        private Queue<char> _turnQueue = new Queue<char>();

        public void Move(Track t)
        {
            var p = t.GetPointAt(CurrentPosition);
            Steer((Pavement)p);
            if (p.IsPavedWhenHeading(CurrentHeading))
            {
                Move(CurrentHeading);
            } else
            {
                throw new System.Exception($"Tried moving to non-paved location [{CurrentHeading.ToString()}] of [x: {CurrentPosition.X}  y: {CurrentPosition.Y}]");
            }
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
        private void Steer(Pavement track)
        {
            if (!track.IsStraight)
            {
                switch (CurrentHeading)
                {
                    case Heading.North:
                        if (track.CurvesRight(CurrentHeading))
                        {
                            CurrentHeading = Heading.East;
                        } else if (track.CurvesLeft(CurrentHeading))
                        {
                            CurrentHeading = Heading.West;
                        }
                        break;
                    case Heading.South:
                        if (track.CurvesRight(CurrentHeading))
                        {
                            CurrentHeading = Heading.West;
                        } else if (track.CurvesLeft(CurrentHeading))
                        {
                            CurrentHeading = Heading.East;
                        }
                        break;
                    case Heading.East:
                        if (track.CurvesRight(CurrentHeading))
                        {
                            CurrentHeading = Heading.South;
                        } else if (track.CurvesLeft(CurrentHeading))
                        {
                            CurrentHeading = Heading.North;
                        }
                        break;
                    case Heading.West:
                        if (track.CurvesRight(CurrentHeading))
                        {
                            CurrentHeading = Heading.North;
                        } else if (track.CurvesLeft(CurrentHeading))
                        {
                            CurrentHeading = Heading.South;
                        }
                        break;
                    default:
                        break;
                }
            } else if (track.IsIntersection)
            {
                CurrentHeading = Trun(CurrentHeading);
            }
        }

        private Heading Trun(Heading currentHeading)
        {
            var nextTrun = _turnQueue.Dequeue();
            var result = Steer(currentHeading, nextTrun);
            _turnQueue.Enqueue(nextTrun);
            return result;
        }

        private Heading Steer(Heading currentHeading, char nextTrun)
        {
            Heading newHeading = currentHeading; 
            switch (currentHeading)
            {
                case Heading.North:
                    if (nextTrun.Equals('L')) return Heading.West;
                    if (nextTrun.Equals('R')) return Heading.East;
                    break;
                case Heading.South:
                    if (nextTrun.Equals('L')) return Heading.East;
                    if (nextTrun.Equals('R')) return Heading.West;
                    break;
                case Heading.East:
                    if (nextTrun.Equals('L')) return Heading.North;
                    if (nextTrun.Equals('R')) return Heading.South;
                    break;
                case Heading.West:
                    if (nextTrun.Equals('L')) return Heading.South;
                    if (nextTrun.Equals('R')) return Heading.North;
                    break;
            }
            return newHeading;
        }
    }
}
