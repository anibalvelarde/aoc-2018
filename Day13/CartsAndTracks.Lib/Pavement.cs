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
    }
}
