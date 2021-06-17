using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class Simplex
    {
        //source - симплекс таблица без базисных переменных
        double[,] table; //симплекс таблица

        int m, n, tempn;  

        List<int> basis; //список базисных переменных

        public Simplex(double[,] source)
        {
            m = source.GetLength(0);
            n = source.GetLength(1);
            
            tempn = source.GetLength(1);
            table = new double[m, n + m - 1];
            basis = new List<int>();

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    if (j < n)
                        table[i, j] = source[i, j];
                    else
                        table[i, j] = 0;
                }
                //выставляем коэффициент 1 перед базисной переменной в строке
                if ((n + i) < table.GetLength(1))
                {
                    table[i, n + i] = 1;
                    basis.Add(n + i);
                }
            }

            n = table.GetLength(1);
        }

        //result - в этот массив будут записаны полученные значения X
        public double[,] Calculate(double[] result)
        {
            int mainCol, mainRow; //ведущие столбец и строка

            while (!IsItEnd())
            {
                mainCol = findMainCol();
                mainRow = findMainRow(mainCol);
                basis[mainRow] = mainCol;

                double[,] new_table = new double[m, n];

                for (int j = 0; j < n; j++)
                    new_table[mainRow, j] = table[mainRow, j] / table[mainRow, mainCol];

                for (int i = 0; i < m; i++)
                {
                    if (i == mainRow)
                        continue;

                    for (int j = 0; j < n; j++)
                        new_table[i, j] = table[i, j] - table[i, mainCol] * new_table[mainRow, j];
                }
                table = new_table;

            }


            //заносим в result найденные значения X
            for (int i = 0; i < result.Length; i++)
            {
                int k = basis.IndexOf(i + 1);
                if (k != -1)
                    result[i] = table[k, 0];
                else
                    result[i] = 0;
            }

            double[,] table2 = new double[m, tempn];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < tempn; j++)
                {
                    table2[i, j] = table[i, j];
                    if (j == 0 && i == 3) table2[i, j] = 0;
                }
            }
            table = table2;
            n = table.GetLength(1);
            return table;
        }

        //result - в этот массив будут записаны полученные значения X
        // для минимизации
        public double[,] Calculate2(double[] result)
        {
            int mainCol, mainRow; //ведущие столбец и строка

            while (!IsItEnd2())
            {
                mainCol = findMainCol2();
                mainRow = findMainRow2(mainCol);
                basis[mainRow] = mainCol; 

                double[,] new_table = new double[m, n];


                /*НЭ = СЭ - (А*В)/РЭ
СТЭ - элемент старого плана, РЭ - разрешающий элемент (6), А и В - элементы старого плана, образующие прямоугольник с элементами СТЭ и РЭ.
                */

                double re = table[mainRow, mainCol];
                for (int j = 0; j < n; j++)
                    new_table[mainRow, j] = table[mainRow, j] / re;

                for (int i = 0; i < m; i++)
                {
                    if (i == mainRow) continue;
                    for (int j = 0; j < n; j++)
                        new_table[i, j] = table[i, j] - table[i, mainCol] * new_table[mainRow, j];
                }
                table = new_table;
            }


            //заносим в result найденные значения X
            for (int i = 0; i < result.Length; i++)
            {
                int k = basis.IndexOf(i + 1);
                if (k != -1)
                    result[i] = table[k, 0];
                else
                    result[i] = 0;
            }

            return table;
        }
        private bool IsItEnd()
        {
            bool flag = true;

            for (int j = 1; j < n; j++)
            {
                if (table[m - 1, j] < 0)
                {
                    flag = false;
                    break;
                }
            }

            return flag;
        }

        private bool IsItEnd2()
        {
            bool flag = true;

            for (int j = 1; j < n; j++)
            {
                if (table[m - 1, j] > 0)
                {
                    flag = false;
                    break;
                }
            }

            return flag;
        }

        private int findMainCol()
        {
            int mainCol = 1;

            for (int j = 2; j < n; j++)
                if (table[m - 1, j] < table[m - 1, mainCol])
                    mainCol = j;

            return mainCol;
        }

        private int findMainRow(int mainCol)
        {
            int mainRow = 0;

            for (int i = 0; i < m - 1; i++)
                if (table[i, mainCol] > 0)
                {
                    mainRow = i;
                    break;
                }

            for (int i = mainRow + 1; i < m - 1; i++)
                if ((table[i, mainCol] > 0) && ((table[i, 0] / table[i, mainCol]) < (table[mainRow, 0] / table[mainRow, mainCol])))
                    mainRow = i;

            return mainRow;
        }

        //находим наибольший 
        private int findMainCol2()
        {
            int mainCol = 1;

            for (int j = 2; j < n; j++)
                if (table[m - 1, j] > table[m - 1, mainCol])
                    mainCol = j;

            return mainCol;
        }

        private int findMainRow2(int mainCol)
        {
            int mainRow = 0;

            for (int i = 0; i < m - 1; i++)
                if (table[i, mainCol] > table[mainRow, mainCol])
                {
                    mainRow = i;
                }

            return mainRow;
        }
    }
}
