using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Lab_1
{
    public partial class Form2 : Form
    {
        Director _director;
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Изображение (*.png)|*.png";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                pictureBoxER.Image = Image.FromFile(filePath);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Изображение (*.png)|*.png";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                pictureBoxSchema.Image = Image.FromFile(filePath);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Код (*.c; *.cpp)|*.c; *.cpp";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                textBoxCode.Text = File.ReadAllText(filePath);
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            this.Size = new Size(275, 750);
            _director = new Director();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ReportBuilder reportBuilder;
            string domainSpesific;
            if (tabControl1.SelectedIndex == 0)
            {
                reportBuilder = new BDReportBuilder();
                pictureBoxER.Image.Save(AppDomain.CurrentDomain.BaseDirectory + "\\Results\\ER.png");
                domainSpesific = "ER.png";
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                reportBuilder = new NetworkReportBuilder();
                pictureBoxER.Image.Save(AppDomain.CurrentDomain.BaseDirectory + "\\Results\\Schema.png");
                domainSpesific = "Schema.png";
            }
            else
            {
                reportBuilder = new ProgReportBuilder();
                domainSpesific = textBoxCode.Text;
            }
            _director.MakeReport(
                    reportBuilder,
                    textBoxName.Text,
                    textBoxGoal.Text,
                    textBoxTask.Text,
                    textBoxTheory.Text,
                    textBoxSetup.Text,
                    textBoxResult.Text,
                    textBoxAnalysis.Text,
                    textBoxConclusion.Text,
                    domainSpesific
            );
            reportBuilder.GetResult().SaveToHTML("report.html");
            MessageBox.Show("Отчёт успешно создан и сохранён.");
        }
    }
}