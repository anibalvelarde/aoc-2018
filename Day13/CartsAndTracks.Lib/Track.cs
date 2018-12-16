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
        private List<Cart> _crashedCarts = new List<Cart>();
        private CrashDetector _radar = new CrashDetector();

        public Track(string trackFile, bool cleanUpCrashSetting = true, int ticksLimit = -1)
        {
            _trackFile = trackFile;
            NeedsToCleanUpCartPileup = cleanUpCrashSetting;
            TicksLimit = ticksLimit;
        }

        public int TicksLimit { get; private set; }
        public bool NeedsToCleanUpCartPileup { get; private set; }
        public int TotalTicks { get; private set; }
        public int Rows { get; private set; }
        public int Columns { get; private set; }
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

            for (int row = 0; row < Rows; row++)
            {
                ProcessTrackLayout(row, trackData[row]);
            }
        }

        public string Render()
        {
            var sb = new StringBuilder();
            sb.Append(""); sb.AppendLine();
            sb.Append("-----------------------------------");sb.AppendLine();
            sb.Append($"--- Ticks Elapsed: [{TotalTicks}]"); sb.AppendLine();
            sb.Append("-----------------------------------"); sb.AppendLine();
            for (int col = 0; col < Columns; col++)
            {
                for (int row = 0; row < Rows; row++)
                {
                    var k = GetCartAtPoint(_grid[col, row].Point);
                    if (k is null)
                    {
                        sb.Append(_grid[col, row].Render()); 
                    } else
                    {
                        sb.Append(k.Render());
                    }
                }
                sb.Append(""); sb.AppendLine();
            }
            sb.Append("-----------------------------------"); sb.AppendLine();
            sb.Append(CrashReport()); sb.AppendLine();
            sb.Append("-----------------------------------"); sb.AppendLine();
            sb.Append(""); sb.AppendLine();
            return sb.ToString();
        }

        public string CrashReport()
        {
            if (_radar.CrashDetected)
            {
                var sb = new StringBuilder();
                foreach (var crashedCart in _crashedCarts)
                {
                    sb.Append($"Crash detected at location [x:{crashedCart.CurrentPosition.Col},y:{crashedCart.CurrentPosition.Row}]");
                    sb.Append('\n');
                }
                return sb.ToString();
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
                return _grid[pos.Col, pos.Row];
            }
        }

        private bool IsOffGrid(Coordinates pos)
        {
            if (pos.Col < 0 || pos.Row < 0) return true;
            if ((pos.Col > (Columns - 1)) || (pos.Row > (Rows - 1))) return true;
            return false;
        }

        public void Tick()
        {
            TotalTicks++;
            var sortedCartList = GetSortedCarts();
            foreach (var cart in sortedCartList)
            {
                cart.Move(this);
                _radar.DetectCrash(_carts);
            }
        }

        public void Simulate()
        {
            var isLastTick = false;
            while (true)
            {
                Tick();
                if (_radar.CrashDetected)
                {
                    if (NeedsToCleanUpCartPileup)
                    {
                        isLastTick = CleanUpCrashSite();
                        if (isLastTick) break;
                    }
                }
                if (NeedsToExit()) break;
            }
        }

        private bool NeedsToExit()
        {
            var needsToExit = false;
            TicksLimit--;
            if (TicksLimit.Equals(0)) needsToExit = true;
            return needsToExit;
        }

        private bool CleanUpCrashSite()
        {
            var crashedCarts = _radar.CrashedCarts();
            foreach (var cart in crashedCarts)
            {
                HandleCrashDetection(cart);
            }
            // last cart left on the track?
            if (_carts.Count.Equals(1))
            {
                return true;
            }
            return false;
        }
        private void HandleCrashDetection(Cart cart)
        {
            _carts.Remove(cart);
            _crashedCarts.Add(cart);
            _radar.ClearCrashSite();
        }

        private List<Cart> GetSortedCarts()
        {
            return _carts
                .OrderBy(r => r.CurrentPosition.Row)
                .ThenByDescending(c => c.CurrentPosition.Col)
                .ToList();
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
            Rows = trackData.Length;
            Columns = trackData[0].Length; 

            _grid = new GridPoint[Columns, Rows];
            for (int col = 0; col < Columns; col++)
            {
                for (int row = 0; row < Rows; row++)
                {
                    _grid[col, row] = new OpenField(' ', col, row);
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
                        HandleCartDetection(col, row, elem);
                        break;

                    case '|':
                    case '-':
                    case '/':
                    case '\\':
                    case '+':
                        HandleTrackDetection(col, row, elem);
                        break;
                }
            }
        }

        private void HandleTrackDetection(int col, int row, char elem)
        {
            _grid[col, row] = new Pavement(elem, col, row);
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
                    _carts.Add(new Cart(Heading.East, col, row));
                    break;

                case '^':
                    // cart detected heading north
                    HandleTrackDetection(col, row, '|');
                    _carts.Add(new Cart(Heading.North, col, row));
                    break;

                case 'v':
                    // cart detected heading south
                    HandleTrackDetection(col, row, '|');
                    _carts.Add(new Cart(Heading.South, col, row));
                    break;

                default:
                    throw new InvalidDataException($"Unknown track piece type: [{elem}].");
            }
        }
    }
}
