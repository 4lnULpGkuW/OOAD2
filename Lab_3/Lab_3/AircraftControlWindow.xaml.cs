using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Lab_3
{
    /// <summary>
    /// Interaction logic for AircraftControlWindow.xaml
    /// </summary>
    public partial class AircraftControlWindow : Window
    {
        private ControlTower _ct;
        private Aircraft _aircraft;
        DispatcherTimer _timer;
        public AircraftControlWindow(ControlTower ct, Aircraft aircraft)
        {
            _ct = ct;
            _aircraft = aircraft;
            InitializeComponent();
            LabelRegCode.Content = aircraft.RegCode;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
            Timer_Tick(null, EventArgs.Empty);
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            string status = "";
            if (_ct.Aircrafts[_aircraft].Item1 == null)
            {
                status = $"in air";
                ButtonRequest.IsEnabled = true;
                ButtonLeave.IsEnabled = true;
                if (_ct.Aircrafts[_aircraft].Item2 == "queued")
                {
                    status += " (awaits landing)";
                    ButtonRequest.IsEnabled = false;
                    ButtonLeave.IsEnabled = false;
                }
            }
            else if (_ct.Aircrafts[_aircraft].Item1 is Runway)
            {
                if (_ct.Aircrafts[_aircraft].Item2 == "taking off")
                    status += "taking off from";
                else
                    status += "landing on";
                status += $" Runway-{_ct.Runways.ToList().IndexOf(_ct.Aircrafts[_aircraft].Item1 as Runway)}";
                ButtonRequest.IsEnabled = false;
                ButtonLeave.IsEnabled = false;
            }
            else
            {
                status += $"in Terminal-{_ct.Terminals.ToList().IndexOf(_ct.Aircrafts[_aircraft].Item1 as Terminal)}";
                ButtonRequest.IsEnabled = true;
                ButtonLeave.IsEnabled = false;
            }
            LabelStatus.Content = status;
        }
        private void ButtonRequest_Click(object sender, RoutedEventArgs e)
        {
            _aircraft.Request();
            Timer_Tick(null, EventArgs.Empty);
        }
        private void ButtonLeave_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            _aircraft.Leave();
            this.Close();
        }
    }
}
