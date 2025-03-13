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

namespace Lab_2
{
    /// <summary>
    /// Interaction logic for SubWindow.xaml
    /// </summary>
    public partial class SubWindow : Window
    {
        MainWindow _mainWindow;
        public event Action<string> StringSelected;
        public SubWindow(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();

            DataGrid1.ItemsSource = mainWindow.HistoryStrings;
        }

        private void DataGrid1_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var selectedItem = DataGrid1.SelectedItem as string;

            if (selectedItem != null && StringSelected != null)
            {
                StringSelected(selectedItem);
            }
            Close();
        }
    }
}
