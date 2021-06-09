using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMN
{
    class Lab12
    {
        private static int n; 
        private static int nMinus1;
        private static float val;
        private static float[,] firstArray;
        private static float[] secondArray;
        private static float[] a;
        private static float[] b;
        private static float[] output;

        private static void Input(int n)
        {
            Console.WriteLine("Введите коэффициенты");

            for (int i = 0; i < n; ++i)
            {
                Console.WriteLine($"Строка {i + 1}");

                for (int j = 0; j < n; ++j)
                {
                    firstArray[i, j] = float.Parse(Console.ReadLine());
                }
            }

            Console.WriteLine("Введите вектор-столбец");

            for (int i = 0; i < n; ++i)
            {
                secondArray[i] = float.Parse(Console.ReadLine());
            }
        }
        
        private static void Print()
        {
            for (int i = 0; i < n; ++i)
            {
                Console.WriteLine($"x{i + 1} = {output[i]}");
            }
        }

        private static void Print(params float[] array)
        {
            for (int i = 0; i < array.Length; ++i)
            {
                Console.Write($"{array[i]} ");
            }
        }

        public static void Exec()
        {
            int.TryParse(Console.ReadLine(), out n);

            nMinus1 = n - 1;
            firstArray = new float[n, n];
            secondArray = new float[n];
            a = new float[n];
            b = new float[n];
            output = new float[n];

            Input(n);

            val = firstArray[0, 0];
            a[0] = -firstArray[0, 1] / val;
            b[0] = secondArray[0] / val;

            for (int i = 1; i < nMinus1; i++)
            {
                val = firstArray[i, i] + firstArray[i, i - 1] * a[i - 1];

                a[i] = -firstArray[i, i + 1] / val;
                b[i] = (secondArray[i] - firstArray[i, i - 1] * b[i - 1]) / val;
            }

            Console.WriteLine("A");
            Print(a);
            Console.WriteLine("B");
            Print(b);
            Console.WriteLine();

            output[nMinus1] = (secondArray[nMinus1] - firstArray[nMinus1, nMinus1 - 1] * b[nMinus1 - 1]) /
                (firstArray[nMinus1, nMinus1] + firstArray[nMinus1, nMinus1 - 1] * a[nMinus1 - 1]);

            for (int i = nMinus1 - 1; i >= 0; --i)
            {
                output[i] = a[i] * output[i + 1] + b[i];
            }

            Print();
        }
    }
}
