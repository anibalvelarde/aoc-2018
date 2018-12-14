using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartsAndTracks.Lib
{
    public class Track
    {
        private readonly string _trackFile;
        private GridPoint[,] _grid;
        private List<Cart> _carts = new List<Cart>();

        public Track(string trackFile)
        {
            _trackFile = trackFile;
        }

        public int Width { get; private set; }
        public int Length { get; private set; }
        public List<Cart> Carts
        {
            get
            {
                return _carts;
            }
        }
        public GridPoint[,] Grid
        {
            get
            {
                return _grid;
            }
        }

        public void Load()
        {
            string[] trackData = BildTrackGrid();

            for (int row = 0; row < Length; row++)
            {
                ProcessTrackLayout(row, trackData[row]);
            }
        }

        private string[] BildTrackGrid()
        {
            // read data
            var trackData = File.ReadAllLines(_trackFile);
            // determine dimensions
            Width = trackData.Length;
            Length = trackData[0].Length; 

            _grid = new GridPoint[Length, Width];
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    _grid[j, i] = new OpenField(' ', j, i);
                }
            }
            return trackData;
        }

        private void ProcessTrackLayout(int row, string trackLayoutData)
        {
            var layoutElements = trackLayoutData.ToCharArray();

            for (int col = 0; col < layoutElements.Length; col++)
            {
                var elem = layoutElements[col];
                switch (elem)
                {
                    case '>':
                    case '<':
                    case '^':
                    case 'V':
                        HandleCartDetection(col, row, elem);
                        break;

                    case '|':
                    case '-':
                    case '/':
                    case '\\':
                    case '+':
                        HandleTrackDetection(row, col, elem);
                        break;
                }
            }
        }

        private void HandleTrackDetection(int col, int row, char elem)
        {
            _grid[col, row] = new Pavement(elem, col, row); ;
        }

        private void HandleCartDetection(int col, int row, char elem)
        {
            switch (elem)
            {
                case '<':
                    // cart detected heading west
                    HandleTrackDetection(col, row, '-');
                    _carts.Add(new Cart(Heading.West, col, row));
                    break;

                case '>':
                    // cart detected heading east
                    HandleTrackDetection(col, row, '-');
                    _carts.Add(new Cart(Heading.West, col, row));
                    break;

                case '^':
                    // cart detected heading north
                    HandleTrackDetection(col, row, '|');
                    _carts.Add(new Cart(Heading.North, col, row));
                    break;

                case 'V':
                    // cart detected heading south
                    HandleTrackDetection(col, row, '|');
                    break;

                default:
                    throw new InvalidDataException($"Unknown track piece type: [{elem}].");
            }
        }
    }
}
