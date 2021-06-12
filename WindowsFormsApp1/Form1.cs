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
        public Form1()
        {
            InitializeComponent();
            double[,] table = { {100, 5, 0, 2, 0, 3, 1, 2},
                                {80,  3, 1, 5, 0, 2, 0, 1},
                                {120, 1, 0, 3, 1, 2, 0, 6},
                                {0, -4,-1,-5,-6,-3.5,-7,-4}};

            double[] result = new double[table.GetLength(1)-1];
            double[,] table_result; 
            Simplex S = new Simplex(table);
            S.Calculate(result);
            table_result = S.Calculate2(result);


            richTextBox1.Text += "Решенная симплекс-таблица:\n"; 
            for (int i = 0; i < table_result.GetLength(0); i++)
            {
                for (int j = 0; j < table_result.GetLength(1); j++)
                    richTextBox1.Text += table_result[i, j] + " ";
                richTextBox1.Text += "\n";
            }


            richTextBox1.Text += "Решение:\n";
            foreach (double res in result)
            {
                richTextBox1.Text += "X[ ] = " + res + "\n";
            } 
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
