using System;

namespace CartsAndTracks.Lib
{
    public class OpenField : GridPoint
    {
        private readonly char _pavementShape;
        public OpenField(char trackPiece, int x, int y)
            : base(x, y)
        {
            if (!trackPiece.Equals(' ')) throw new ArgumentOutOfRangeException($"Open field type cannot hold pavement shape [{trackPiece}].");

            _pavementShape = trackPiece;
        }

        public override string Render()
        {
            return " ";
        }
    }
}
