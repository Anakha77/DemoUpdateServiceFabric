using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;

namespace MatrixClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly Timer _updateTimer;
        private readonly List<string> _lines;
        private readonly int _updateInterval;
        private int _serviceCalls;
        private int _serviceErrors;

        public int MaxLines => (int)Math.Ceiling(lblMatrix.ActualHeight / 12);
        public int MaxCharacterByLine => (int)Math.Floor(lblMatrix.ActualWidth / 6);

        public MainWindow()
        {
            InitializeComponent();

            _lines = new List<string>();

            _updateInterval = int.Parse(ConfigurationManager.AppSettings["updateInterval"]);

            _updateTimer = new Timer { Interval = _updateInterval };
            _updateTimer.Elapsed += UpdateTimerElapsed;
            _updateTimer.Start();
        }

        private async void UpdateTimerElapsed(object sender, ElapsedEventArgs e)
        {
            await AddMatrixContentAsync();
        }

        public async Task AddMatrixContentAsync()
        {
            try
            {
                var t = await GetMatrixAsync();
                _lines.Add(t.Substring(1, t.Length-2));
                _serviceCalls++;

                if (_lines.Count > MaxLines)
                {
                    _lines.RemoveRange(0, _lines.Count - MaxLines);
                }
                Dispatcher.Invoke(UpdateMatrixContent);
                Dispatcher.Invoke(UpdateTitle);
            }
            catch (Exception)
            {
                _serviceErrors++;
                Dispatcher.Invoke(UpdateTitle);
            }
        }

        private void UpdateTitle()
        {
            Title = $"Lines : {_serviceCalls} / Errors : {_serviceErrors}";
        }

        private void UpdateMatrixContent()
        {
            lblMatrix.Content = string.Join(Environment.NewLine, _lines);
        }

        public async Task<string> GetMatrixAsync()
        {
            var client = new HttpClient();

            var host = ConfigurationManager.AppSettings["serviceHost"];
            var port = ConfigurationManager.AppSettings["servicePort"];
            var uri = $"http://{host}:{port}{ConfigurationManager.AppSettings["serviceUri"]}/{MaxCharacterByLine}";

            client.BaseAddress = new Uri(uri);
            client.Timeout = TimeSpan.FromMilliseconds(_updateInterval);

            return await client.GetStringAsync("");
        }
    }
}
