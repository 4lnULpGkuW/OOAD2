using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab_1
{
    public class Report
    {
        public string Name { get; set; }
        public string Goal { get; set; }
        public string Task { get; set; }
        public string Theory { get; set; }
        public string Setup { get; set; }
        public string Results { get; set; }
        public string Analysis { get; set; }
        public string Conclusion { get; set; }
        public string Schema { get; set; }
        public string Diagram { get; set; }
        public string Code { get; set; }
        public Report() { }
        public Report(string name, string goal, string task, string theory, string setup,
                      string results, string analysis, string conclusion,
                      string schema, string diagram, string code)
        {
            Name = name;
            Goal = goal;
            Task = task;
            Theory = theory;
            Setup = setup;
            Results = results;
            Analysis = analysis;
            Conclusion = conclusion;
            Schema = schema;
            Diagram = diagram;
            Code = code;
        }
        public void SaveToHTML(string fileName)
        {
            string htmlContent = $@"
                <html>
                <head>
                    <title>Отчёт по теме {Name}</title>
                </head>
                <body>
                    <h1>Отчёт по теме {Name}</h1>
                    <h2>Цель работы</h2><p>{Goal}</p>
                    <h2>Задание</h2><p>{Task}</p>
                    <h2>Теоретические сведения</h2><p>{Theory}</p>
                    <h2>Описание экспериментальной установки</h2><p>{Setup}</p>
                    <h2>Результаты работы</h2><p>{Results}</p>
                    <h2>Анализ результатов работы</h2><p>{Analysis}</p>
                    <h2>Выводы</h2><p>{Conclusion}</p>";
            if (!string.IsNullOrEmpty(Diagram))
            {
                htmlContent += $"<h2>ER диаграмма</h2><img src={Diagram} alt=\"ER диаграмма\">";
            }
            if (!string.IsNullOrEmpty(Schema))
            {
                htmlContent += $"<h2>Схема сети</h2><img src={Schema} alt=\"Схема сети\">";
            }
            if (!string.IsNullOrEmpty(Code))
            {
                htmlContent += $"<h2>Код</h2><pre>{Code}</pre>";
            }
            htmlContent += @"
                </body>
                </html>";
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\Results\\" + fileName, htmlContent);
        }
    }
}