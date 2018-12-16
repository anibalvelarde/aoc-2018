using System;
using System.Collections.Generic;
using System.Text;

namespace CartsAndTracks.Lib
{
    public class Cart
    {
        public Cart(Heading h, int col, int row)
        {
            CurrentPosition = new Coordinates(col, row);
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
                throw new System.Exception($"Tried moving to non-paved location [{CurrentHeading}] of [x: {CurrentPosition.Col}  y: {CurrentPosition.Row}]");
            }
        }
        public void Crash()
        {
            IsCrashed = true;
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"Cart:\n");
            sb.Append($"  Location: [x:{CurrentPosition.Col} y:{CurrentPosition.Row}]");
            sb.Append($"  Heading:  {CurrentHeading}");
            return sb.ToString();
        }
        
        private bool IsPaved(Track t)
        {
            return ! (t.Grid[CurrentPosition.Col, CurrentPosition.Row].Render().Equals(" "));
        }
        private void Move(Heading h, int steps = 1)
        {
            if (!IsCrashed)
            {
                CurrentHeading = h;
                var col = CurrentPosition.Col; var row = CurrentPosition.Row;
                switch (h)
                {
                    case Heading.North:
                        row--;
                        CurrentPosition = new Coordinates(col, row);
                        break;
                    case Heading.South:
                        row++;
                        CurrentPosition = new Coordinates(col, row);
                        break;
                    case Heading.East:
                        col++;
                        CurrentPosition = new Coordinates(col, row);
                        break;
                    case Heading.West:
                        col--;
                        CurrentPosition = new Coordinates(col, row);
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
