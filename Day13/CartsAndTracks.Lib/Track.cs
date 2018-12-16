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

        public int TotalTicks { get; private set; }
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
            Console.WriteLine("");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine($"--- Ticks Elapsed: [{TotalTicks}]");
            Console.WriteLine("-----------------------------------");
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
            Console.WriteLine("-----------------------------------");
            Console.WriteLine(CrashReport());
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("");
        }

        private string CrashReport()
        {
            if (_radar.CrashDetected)
            {
                var loc = _radar.CrashLocations();
                return $"Crash detected at location [x:{loc[0].X},y:{loc[0].Y}]";
            } else
            {
                return "No crashes were detected.";
            }
        }

        internal bool IsPavedNext(Heading h, Coordinates referencePosition)
        {
            var point = GetPointAt(referencePosition);
            return point.IsPavedWhenHeading(h);
        }

        public GridPoint GetPointAt(Coordinates pos)
        {
            if (IsOffGrid(pos))
            {
                return new NoGoZone(pos);
            }
            else
            {
                return _grid[pos.X, pos.Y];
            }
        }

        private bool IsOffGrid(Coordinates pos)
        {
            if (pos.X < 0 || pos.Y < 0) return true;
            if ((pos.X > (Length - 1)) || (pos.Y > (Width - 1))) return true;
            return false;
        }

        public void Tick()
        {
            TotalTicks++;
            foreach (var cart in _carts)
            {
                cart.Move(this);
                _radar.DetectCrash(_carts);
            }
        }

        public void Simulate(int tickLimit = 100)
        {
            var checkTickLimit = tickLimit > 0;
            while (!_radar.CrashDetected && (checkTickLimit && tickLimit > 0))
            {
                Tick();
                tickLimit--;
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
