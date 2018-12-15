using System;
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
        private CrashDetector _radar = new CrashDetector();

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

        public void Render()
        {
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    var k = GetCartAtPoint(_grid[i,j].Point);
                    if (k is null)
                    {
                        Console.Write(_grid[i, j].Render()); 
                    } else
                    {
                        Console.Write(k.Render());
                    }
                }
                Console.WriteLine("");
            }
        }

        public void Tick()
        {
            foreach (var cart in _carts)
            {
                cart.Move(this);
                _radar.DetectCrash(_carts);
            }
        }

        public void Simulate()
        {
            while (!_radar.CrashDetected)
            {
                Tick();
                //Render();
            }
        }

        private Cart GetCartAtPoint(Coordinates p)
        {
            foreach (var c in _carts)
            {
                if (c.CurrentPosition.Equals(p))
                {
                    return c;
                }
            }
            return null;
        }

        private string[] BildTrackGrid()
        {
            // read data
            var trackData = File.ReadAllLines(_trackFile);
            // determine dimensions
            Width = trackData[0].Length;
            Length = trackData.Length;

            _grid = new GridPoint[Length, Width];
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    _grid[i, j] = new OpenField(' ', j, i);
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
                    case 'v':
                        HandleCartDetection(row, col, elem);
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

        private void HandleTrackDetection(int row, int col, char elem)
        {
            _grid[row, col] = new Pavement(elem, row, col);
        }

        private void HandleCartDetection(int row, int col, char elem)
        {
            switch (elem)
            {
                case '<':
                    // cart detected heading west
                    HandleTrackDetection(row, col, '-');
                    _carts.Add(new Cart(Heading.West, row, col));
                    break;

                case '>':
                    // cart detected heading east
                    HandleTrackDetection(row, col, '-');
                    _carts.Add(new Cart(Heading.East, row, col));
                    break;

                case '^':
                    // cart detected heading north
                    HandleTrackDetection(row, col, '|');
                    _carts.Add(new Cart(Heading.North, row, col));
                    break;

                case 'v':
                    // cart detected heading south
                    HandleTrackDetection(row, col, '|');
                    _carts.Add(new Cart(Heading.South, row, col));
                    break;

                default:
                    throw new InvalidDataException($"Unknown track piece type: [{elem}].");
            }
        }
    }
}
