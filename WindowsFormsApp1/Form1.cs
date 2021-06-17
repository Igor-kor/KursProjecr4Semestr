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
        double[,] table;/*= { {100, 5, 0, 2, 0, 3, 1, 2},
                                {80,  3, 1, 5, 0, 2, 0, 1},
                                {120, 1, 0, 3, 1, 2, 0, 6},
                                {0, -4,-1,-5,-6,-3.5,-7,-4}};*/
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            dataGridView1.Columns.Add("col1", "Название витамин/\nвитаминный комплекс");
            dataGridView1.Columns.Add("colp1", "P1");
            dataGridView1.Columns.Add("colp2", "P2");
            dataGridView1.Columns.Add("colp3", "P3");
            dataGridView1.Columns.Add("colp4", "P4");
            dataGridView1.Columns.Add("colp5", "P5");
            dataGridView1.Columns.Add("colp6", "P6");
            dataGridView1.Columns.Add("colp7", "P7");
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            } 
            dataGridView1.Columns.Add("col3", "Всего необходимо");
            dataGridView1.Rows.Add("Цена за 1 г. тыс. руб", "4", "1", "5", "6", "3,5", "7", "4");
            dataGridView1.Rows.Add("A", "5", "0", "2", "0", "3", "1", "2", "100");
            dataGridView1.Rows.Add("C", "3", "1", "5", "0", "2", "0", "1", "80");
            dataGridView1.Rows.Add("B6", "1", "0", "3", "1", "2", "0", "6", "120");
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            table = new double[dataGridView1.Rows.Count - 1, dataGridView1.Columns.Count - 1];
            int counttable = 0;
            for (int i = 1; i < dataGridView1.Rows.Count - 1; i++)
            {
                // сначало вносим количество всего
                table[counttable, 0] = Convert.ToDouble(dataGridView1.Rows[i].Cells[dataGridView1.Rows[i].Cells.Count - 1].Value);
                // затем содержимое каждого поливитамина
                for (int k = 1; k < dataGridView1.Rows[i].Cells.Count - 1; k++)
                {
                    table[counttable, k] = Convert.ToDouble(dataGridView1.Rows[i].Cells[k].Value);
                }
                counttable++;
            }
            // затем вносим целевую функцию

            // сначало вносим количество всего
            table[counttable, 0] = 0;
            for (int k = 1; k < dataGridView1.Rows[0].Cells.Count - 1; k++)
            {
                table[counttable, k] = Convert.ToDouble(dataGridView1.Rows[0].Cells[k].Value) * -1;
            }

            //richTextBox2.Text = "";
            richTextBox3.Text = "";
            // string[] lines = richTextBox1.Text.Trim('\n').Split('\n');
            //double[,] num = new double[lines.Length, lines[0].Trim(' ').Split(' ').Length];
            /* try
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
             }*/
            /*catch (System.IndexOutOfRangeException)
            {
                richTextBox2.Text += "Ошибка в начальной таблице, проверте правильность ввода!";
                return;
            }

            table = num;*/
            richTextBox3.Text += "Начальная таблица:\n";
            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                    richTextBox3.Text += Math.Round(table[i, j], 2) + " ";
                richTextBox3.Text += "\n";
            }
            double[] result = new double[table.GetLength(1) - 1]; 
            Simplex S = new Simplex(table);
            S.Calculate(result);
            S.Calculate2(result);

            richTextBox3.Text += "Ответ:\n";
            int count = 1;
            foreach (double res in result)
            {
                richTextBox3.Text += dataGridView1.Columns[ count++].HeaderText + " = " + res + "\n";
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            if (form2.ShowDialog() == DialogResult.OK)
            {
                dataGridView1.Rows.Add(form2.textBox1.Text);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            if (form2.ShowDialog() == DialogResult.OK)
            {
                dataGridView1.Columns.Add(form2.textBox1.Text, form2.textBox1.Text);
                dataGridView1.Columns[form2.textBox1.Text].DisplayIndex = dataGridView1.Columns.Count - 2;
            }
        }
    }
}
