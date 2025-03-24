using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static Lab_3.MainWindow;

namespace Lab_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class AircraftEntry
        {
            public string Aircraft { get; set; }
            public string Status { get; set; }
        }
        ObservableCollection<AircraftEntry> aircrafts = new();
        public class RunwayEntry
        {
            public string Runway { get; set; }
            public string Status { get; set; }
        }
        ObservableCollection<RunwayEntry> runways = new();
        public class TerminalEntry
        {
            public string Terminal { get; set; }    
            public string Status { get; set; }
        }
        ObservableCollection<TerminalEntry> terminals = new();
        private void UpdateCollections()
        {
            aircrafts.Clear();
            foreach (var aircraft in ct.Aircrafts.Keys)
            {
                string status = "";
                if (ct.Aircrafts[aircraft].Item1 == null)
                {
                    status = $"✈️ in air";
                    if (ct.Aircrafts[aircraft].Item2 == "queued")
                        status += " (awaits landing)";
                }
                else if (ct.Aircrafts[aircraft].Item1 is Runway)
                {
                    if (ct.Aircrafts[aircraft].Item2 == "taking off")
                        status += "🛫 taking off from";
                    else
                        status += "🛬 landing on";
                    status += $" Runway-{ct.Runways.ToList().IndexOf(ct.Aircrafts[aircraft].Item1 as Runway)}";
                }
                else
                {
                    status += $"🅿 in Terminal-{ct.Terminals.ToList().IndexOf(ct.Aircrafts[aircraft].Item1 as Terminal)}";
                }
                aircrafts.Add(new AircraftEntry { Aircraft = aircraft.RegCode, Status = status });
            }

            runways.Clear();
            foreach (var runway in ct.Runways)
            {
                string status = "";
                if (!runway.IsOccupied)
                    status = "✅ Free";
                else
                {
                    var occupyingAircraft = ct.Aircrafts.FirstOrDefault(
                        x => x.Value.Item1 is Runway && x.Value.Item1 == runway
                        ).Key;
                    status = $"⛔ Occupied by {occupyingAircraft.RegCode}";
                }
                runways.Add(new RunwayEntry { Runway = $"Runway-{ct.Runways.ToList().IndexOf(runway)}",
                    Status = status });
            }

            terminals.Clear();
            foreach (var terminal in ct.Terminals)
            {
                string status = "";
                if (!terminal.IsOccupied)
                    status = "✅ Free";
                else
                {
                    var occupyingAircraft = ct.Aircrafts.FirstOrDefault(
                        x => x.Value.Item1 is Terminal && x.Value.Item1 == terminal
                        ).Key;

                    if (occupyingAircraft != null)
                        status = $"⛔ Occupied by {occupyingAircraft.RegCode}";
                    else
                        status = "⏳ Reserved";
                }
                terminals.Add(new TerminalEntry { Terminal = $"Terminal-{ct.Terminals.ToList().IndexOf(terminal)}",
                    Status = status });
            }
        }
        ControlTower ct = new();
        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i < 3; i++)
                ct.AddRunway();
            for (int i = 0; i < 5; i++)
                ct.AddTerminal();

            Console.SetOut(new ListBoxWriter(LogListBox));

            DataGridAircrafts.ItemsSource = aircrafts;
            DataGridRunways.ItemsSource = runways;
            DataGridTerminals.ItemsSource = terminals;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            Timer_Tick(null, EventArgs.Empty);
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateCollections();
        }
        private void ButtonAddAircraft_Click(object sender, RoutedEventArgs e)
        {
            AddAircraftWindow addAircraftWindow = new AddAircraftWindow();
            addAircraftWindow.Show();
            addAircraftWindow.AddAircraftEvent += AddAircraftWindow_AddAircraft;
        }
        private void AddAircraftWindow_AddAircraft(string regCode)
        {
            try
            {
                AircraftControlWindow aircraftControlWindow = new AircraftControlWindow(ct, ct.AddAircraft(regCode));
                aircraftControlWindow.Show();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void DataGridAircrafts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var aircraftEntry = DataGridAircrafts.SelectedItem as AircraftEntry;
            if (aircraftEntry == null) return;
            string regCode = aircraftEntry.Aircraft;
            var aircraft = ct.Aircrafts.Keys.FirstOrDefault(a => a.RegCode == regCode);
            AircraftControlWindow aircraftControlWindow = new AircraftControlWindow(ct, aircraft);
            aircraftControlWindow.Show();
        }
    }
}
