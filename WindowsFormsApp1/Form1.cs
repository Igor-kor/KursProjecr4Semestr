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
        double[,] table; 
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
                dataGridView1.Columns[i].Selected = true;
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
             
            richTextBox3.Text = "";
     
            double[] result = new double[table.GetLength(1) - 1]; 
            Simplex S = new Simplex(table);
            S.Calculate(result);
            S.Calculate2(result);

            richTextBox3.Text += "Ответ:\n";
            int count = 1;
            double total = 0;
            foreach (double res in result)
            {
                richTextBox3.Text += dataGridView1.Columns[ count].HeaderText + " = " + res + "\n";
                total += Convert.ToDouble(dataGridView1.Rows[0].Cells[count].Value) * res; 
                   count++;
            }
            richTextBox3.Text += "\nВсего: " + total ;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
