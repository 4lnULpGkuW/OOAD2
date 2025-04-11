using Lab_4.ExchangeRateGateway;
using Lab_4.GUI;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Lab_4
{    public class Program
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            // app.Run(new AnotherWindow());
            var serv = new List <ICurrencyService>();
            serv.Add(new CurrencyMockService.CurrencyMockGateway());
            serv.Add(new ExchangeRateGateway.ExchangeRateHttpGateway("0b382a9c9e91763dad6b63ae"));
            app.Run(new GUI.MainWindow(serv));
        }
    }
}