using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        double[,] table = { {100, 5, 0, 2, 0, 3, 1, 2},
                                {80,  3, 1, 5, 0, 2, 0, 1},
                                {120, 1, 0, 3, 1, 2, 0, 6},
                                {0, -4,-1,-5,-6,-3.5,-7,-4}};
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                    richTextBox1.Text += table[i, j] + " ";
                richTextBox1.Text += "\n";
            }
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            richTextBox3.Text = "";
            string[] lines = richTextBox1.Text.Trim('\n').Split('\n');
            double[,] num = new double[lines.Length, lines[0].Trim(' ').Split(' ').Length];
            try
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] temp = lines[i].Trim(' ').Split(' ');
                    if (temp.Length != lines[0].Trim(' ').Split(' ').Length)
                    {
                        throw new IndexOutOfRangeException();
                    }
                    for (int j = 0; j < temp.Length; j++)
                    {
                        if (temp[j].Length > 0) num[i, j] = Convert.ToDouble(temp[j]);

                    }

                }
            }
            catch (System.IndexOutOfRangeException)
            {
                richTextBox2.Text += "Ошибка в начальной таблице, проверте правильность ввода!";
                return;
            }

            table = num;
            richTextBox2.Text += "Начальная таблица:\n";
            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                    richTextBox2.Text += Math.Round(table[i, j], 2) + " ";
                richTextBox2.Text += "\n";
            }
            double[] result = new double[table.GetLength(1) - 1];
            double[,] table_result;
            Simplex S = new Simplex(table, richTextBox2);
            S.Calculate(result);
            table_result = S.Calculate2(result);

            richTextBox3.Text += "Ответ:\n";
            int count = 1;
            foreach (double res in result)
            {
                richTextBox3.Text += "X[" + count++ + "] = " + res + "\n";
            }
        }
    }
}
