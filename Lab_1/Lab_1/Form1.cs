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
using System.Xml.Linq;

namespace Lab_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Изображения (*.png)|*.png";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                pictureBoxER.Image = Image.FromFile(filePath);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Изображения (*.png)|*.png";
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
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(275, 750);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Report report;
            if (tabControl1.SelectedIndex == 0)
            {
                pictureBoxER.Image.Save(AppDomain.CurrentDomain.BaseDirectory + "\\Results\\ER.png");
                report = new Report(
                    $"«{textBoxName.Text}» + по дисциплине Базы данных",
                    textBoxName.Text,
                    textBoxTask.Text,
                    textBoxTheory.Text,
                    textBoxSetup.Text,
                    textBoxResult.Text,
                    textBoxAnalysis.Text,
                    textBoxConclusion.Text,
                    "ER.png",
                    null,
                    null
                );
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                pictureBoxSchema.Image.Save(AppDomain.CurrentDomain.BaseDirectory + "\\Results\\Schema.png");
                report = new Report(
                    $"«{textBoxName.Text}» + по дисциплине Компьютерные сети",
                    textBoxName.Text,
                    textBoxTask.Text,
                    textBoxTheory.Text,
                    textBoxSetup.Text,
                    textBoxResult.Text,
                    textBoxAnalysis.Text,
                    textBoxConclusion.Text,
                    null,
                    "Schema.png",
                    null
                );
            }
            else
            {
                report = new Report(
                    $"«{textBoxName.Text}» + по дисциплине Основы программирования",
                    textBoxName.Text,
                    textBoxTask.Text,
                    textBoxTheory.Text,
                    textBoxSetup.Text,
                    textBoxResult.Text,
                    textBoxAnalysis.Text,
                    textBoxConclusion.Text,
                    null,
                    null,
                    textBoxCode.Text
                );
            }
            report.SaveToHTML("report.html");
            MessageBox.Show("Отчёт успешно создан и сохранён.");
        }
    }
}