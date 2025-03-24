using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Controls;

namespace Lab_3
{
    public class ListBoxWriter : TextWriter
    {
        private ListBox listBox;
        public ListBoxWriter(ListBox listBox)
        {
            this.listBox = listBox;
        }
        public override void Write(char value)
        {
        }
        public override void Write(string value)
        {
            listBox.Dispatcher.Invoke(() =>
            {
                string logEntry = $"{DateTime.Now:HH:mm:ss} - {value}";
                listBox.Items.Add(logEntry);
                listBox.ScrollIntoView(listBox.Items[listBox.Items.Count - 1]);
            });
        }
        public override Encoding Encoding => Encoding.UTF8;
    }
}
