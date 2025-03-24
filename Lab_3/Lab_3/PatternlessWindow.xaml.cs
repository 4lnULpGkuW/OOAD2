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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Lab_3
{
    /// <summary>
    /// Interaction logic for PatternlessWindow.xaml
    /// </summary>
    public partial class PatternlessWindow : Window
    {
        private static bool[] runways = new bool[3];
        private static bool[] terminals = new bool[5];
        public class Aircraft
        {
            public string RegCode { get; }
            public Aircraft(string regCode) => RegCode = regCode;
            public override string ToString()
            {
                return RegCode;
            }
            public async Task Land()
            {
                var freeRunway = Array.IndexOf(PatternlessWindow.runways, false);
                var freeTerminal = Array.IndexOf(PatternlessWindow.terminals, false);

                if (freeRunway != -1 && freeTerminal != -1)
                {
                    PatternlessWindow.runways[freeRunway] = true;
                    PatternlessWindow.terminals[freeTerminal] = true;
                    Console.Write($"{RegCode} started landing");
                    await Task.Delay(10000);
                    PatternlessWindow.runways[freeRunway] = false;
                    Console.Write($"{RegCode} landed");
                }
                Console.Write($"{RegCode} landing not possible");
            }
            public async Task Takeoff()
            {
                var freeRunway = Array.IndexOf(PatternlessWindow.runways, false);

                if (freeRunway != -1)
                {
                    PatternlessWindow.runways[freeRunway] = true;
                    Console.Write($"{RegCode} started taking off");
                    await Task.Delay(10000);
                    PatternlessWindow.runways[freeRunway] = false;
                    Console.Write($"{RegCode} took off");
                }
                Console.Write($"{RegCode} taking off not possible");
            }
        }
        private ObservableCollection<Aircraft> aircrafts = new();
        public PatternlessWindow()
        {
            InitializeComponent();
            Combox1.ItemsSource = aircrafts;
            Console.SetOut(new ListBoxWriter(LogListBox));

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            Timer_Tick(null, EventArgs.Empty);
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            string runwaysString = "Runways  ";
            foreach (var i in runways)
            {
                runwaysString += $"{Convert.ToInt32(i)} ";
            }
            TextBoxRunways.Text = runwaysString;
            string terminalsString = "Terminals ";
            foreach (var i in terminals)
            {
                terminalsString += $"{Convert.ToInt32(i)} ";
            }
            TextBoxTerminals.Text = terminalsString;
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            aircrafts.Add(new Aircraft($"Aircraft-{aircrafts.Count()}"));
        }
        private void ButtonLand_Click(object sender, RoutedEventArgs e)
        {
            var aircraft = Combox1.SelectedItem as Aircraft;
            aircraft.Land();
        }
        private void ButtonTakeoff_Click(object sender, RoutedEventArgs e)
        {
            var aircraft = Combox1.SelectedItem as Aircraft;
            aircraft.Takeoff();
        }
        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            var aircraft = Combox1.SelectedItem as Aircraft;
            aircrafts.Remove(aircraft);
        }
    }
}
