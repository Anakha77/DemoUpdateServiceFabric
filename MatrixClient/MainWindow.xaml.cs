using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;

namespace Matrix.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly List<string> _lines;
        private readonly List<string> _seeds;

        private int _serviceTimeout;
        private readonly int _updateInterval;

        private int _serviceCalls;
        private int _serviceErrors;
        private bool _loading;

        public int MaxLines => (int)Math.Ceiling(lblMatrix.ActualHeight / 12);
        public int MaxColumns => (int)Math.Floor(lblMatrix.ActualWidth / 6);

        public bool Loading
        {
            get { return _loading; }
            set
            {
                if (_loading == value) return;
                _loading = value;
                if (value)
                {
                    Dispatcher.Invoke(() => { Title = "Loading..."; });
                }
            }
        }

        public MainWindow()
        {
            _lines = new List<string>();
            _seeds = new List<string>();

            InitializeComponent();

            _updateInterval = int.Parse(ConfigurationManager.AppSettings["updateSeedInterval"]);
            var scrollInterval = int.Parse(ConfigurationManager.AppSettings["scrollInterval"]);

            var scrollTimer = new Timer { Interval = scrollInterval };
            scrollTimer.Elapsed += (sender, args) => { ScrollLines(); };
            scrollTimer.Start();

            var updateSeedsTimer = new Timer { Interval = _updateInterval };
            updateSeedsTimer.Elapsed += async (sender, args) => { await UpdateSeedsAsync(); };
            updateSeedsTimer.Start();
        }

        private void ScrollLines()
        {
            CreateNewLine();

            RemoveOldLines();

            Dispatcher.InvokeAsync(UpdateMatrixContent);
            Dispatcher.InvokeAsync(UpdateTitle);
        }

        private void RemoveOldLines()
        {
            if (_lines.Count > MaxLines)
            {
                _lines.RemoveRange(0, _lines.Count - MaxLines);
            }
        }

        private async Task<string> GetMatrixAsync()
        {
            var client = new HttpClient();

            var host = ConfigurationManager.AppSettings["serviceHost"];
            var port = ConfigurationManager.AppSettings["servicePort"];
            var uri = $"http://{host}:{port}{ConfigurationManager.AppSettings["serviceUri"]}/{MaxLines}";

            client.BaseAddress = new Uri(uri);
            _serviceTimeout = MaxColumns > 0 ? _updateInterval / MaxColumns : _updateInterval;
            client.Timeout = TimeSpan.FromMilliseconds(_serviceTimeout);

            _serviceCalls++;

            try
            {
                return await client.GetStringAsync(string.Empty);
            }
            catch (Exception ex)
            {
                _serviceErrors++;
                return " ";
            }
        }
        private async Task<string> GetSeedAsync()
        {
            var seed = await GetMatrixAsync();
            return string.IsNullOrEmpty(seed) ? " " : seed;
        }
        private async Task AddSeedAsync()
        {
            var content = await GetSeedAsync();
            _seeds.Add(content);
        }

        private async Task UpdateSeedsAsync()
        {
            Loading = true;

            for (var i = 0; i < _seeds.Count; i++)
            {
                _seeds[i] = await GetSeedAsync();
            }

            Loading = false;
        }
        private async Task CompleteSeedsAsync()
        {
            if (_seeds.Count < MaxColumns)
            {
                for (var i = 0; i < MaxColumns - _seeds.Count; i++)
                {
                    await AddSeedAsync();
                }
            }
        }

        private void CreateNewLine()
        {
            var line = string.Empty;

            Task.Run(() => CompleteSeedsAsync());

            var r = new Random();

            for (var i = 0; i < _seeds.Count; i++)
            {
                var seed = _seeds[i];
                line += seed[r.Next(seed.Length)];
            }
            _lines.Add(line);
        }

        private void UpdateTitle()
        {
            if (Loading) return;

            Title = $"Lines : {_serviceCalls} / Errors : {_serviceErrors}";
        }

        private void UpdateMatrixContent()
        {
            lblMatrix.Content = string.Join(Environment.NewLine, _lines);
        }
    }
}
