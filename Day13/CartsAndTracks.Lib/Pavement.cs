using System;

namespace CartsAndTracks.Lib
{
    public class Pavement : GridPoint
    {
        private readonly char _pavementShape;

        public Pavement(char trackPiece, int x, int y)
            : base(x, y)
        {
            if (trackPiece.Equals(' ')) throw new ArgumentOutOfRangeException($"Pavement type cannot hold the value of empty space [{trackPiece}].");

            _pavementShape = trackPiece;
        }

        public bool IsStraight
        {
            get
            {
                switch (_pavementShape)
                {
                    case '-':
                    case '|':
                    case '+':
                        return true;
                    default:
                        return false;
                }
            }
        }

        public bool IsIntersection
        {
            get
            {
                return _pavementShape.Equals('+');
            }
        }

        public override bool IsPavedWhenHeading(Heading direction)
        {
            var isPaved = false;
            switch (direction)
            {
                case Heading.North:
                case Heading.South:
                    if (_pavementShape.Equals('|') || _pavementShape.Equals('+'))
                    {
                        isPaved = true;
                    }
                    if (_pavementShape.Equals('\\') || _pavementShape.Equals('/'))
                    {
                        isPaved = true;
                    }
                    break;
                case Heading.East:
                case Heading.West:
                    if (_pavementShape.Equals('-') || _pavementShape.Equals('+'))
                    {
                        isPaved = true;
                    }
                    if (_pavementShape.Equals('\\') || _pavementShape.Equals('/'))
                    {
                        isPaved = true;
                    }
                    break;
                default:
                    break;
            }
            return isPaved;
        }

        public override string Render()
        {
            return _pavementShape.ToString();
        }

        internal bool CurvesRight(Heading h)
        {
            switch (_pavementShape)
            {
                case '/':
                    if (h.Equals(Heading.North) || h.Equals(Heading.South))
                        return true;
                    break;
                case '\\':
                    if (h.Equals(Heading.West) || h.Equals(Heading.East))
                        return true;
                    break;
            }
            return false;
        }

        internal bool CurvesLeft(Heading h)
        {
            return !CurvesRight(h);
        }
    }
}
