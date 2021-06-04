using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] table = { {100, 5, 0, 2, 0, 3, 1, 2},
                                {80,  3, 1, 5, 0, 2, 0, 1},
                                {120, 1, 0, 3, 1, 2, 0, 6},
                                {0, -4,-1,-5,-6,-3.5,-7,-4}};

            double[] result = new double[7];
            double[,] table_result;
            double[,] table2 = new double[4, 8];
            Simplex S = new Simplex(table);
            table_result = S.Calculate(result);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    table2[i, j] = table_result[i, j];
                    if(j == 0 && i == 3)table2[i, j] = 0;
                } 
            }

            Simplex S2 = new Simplex(table2);
            table_result = S2.Calculate2(result);


            Console.WriteLine("Решенная симплекс-таблица:");
            for (int i = 0; i < table_result.GetLength(0); i++)
            {
                for (int j = 0; j < table_result.GetLength(1); j++)
                    Console.Write(table_result[i, j] + " ");
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Решение:");
            foreach (double res in result)
            {
                Console.WriteLine("X[ ] = " + res);
            }

            Console.ReadLine();
        }
    }
}
