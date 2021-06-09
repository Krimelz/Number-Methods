using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMN
{
    class Lab4
    {
        private delegate double Function1(double x);
        private delegate double Function2(double x, double dx);

        private static double x0;
        private static double xn;
        private static double h;

        private static List<double> xs = new List<double>();
        private static List<double> ys = new List<double>();

        private static double Func(double x)
        {
            //return Math.Log(x) - 1 / x;
            return x / (3 * x - 5);
        }

        private static double Func1(double x)
        {
            return 1 / x + 1 / (x * x);
        }

        private static double Func2(double x)
        {
            return -1 / (x * x) - 2 / (x * x * x);
        }

        private static void CalcValues(Function1 func)
        {
            xs.Clear();
            ys.Clear();

            for (double x = x0; x <= xn; x += h)
            {
                xs.Add(x);
                ys.Add(func(x));
            }
        }

        private static void CalcValues(Function2 func, double dx)
        {
            xs.Clear();
            ys.Clear();

            for (double x = x0; x <= xn; x += h)
            {
                xs.Add(x);
                ys.Add(func(x, dx));
            }
        }

        private static void PrintTable(List<double> xs, List<double> ys, int precision)
        {
            Console.WriteLine();
            Console.Write("X");

            foreach (double x in xs)
            {
                Console.Write($"|{Math.Round(x, precision).ToString().PadLeft(10).PadRight(10)}");
            }

            Console.WriteLine("|");
            Console.Write(" ");

            for (int i = 0; i < xs.Count; ++i)
            {
                Console.Write("-----------");
            }

            Console.WriteLine();
            Console.Write("Y");
            
            foreach (double y in ys)
            {
                Console.Write($"|{Math.Round(y, precision).ToString().PadLeft(10).PadRight(10)}");
            }

            Console.WriteLine("|");
        }

        private static double CalcLeft(double x, double dx)
        {
            return (Func(x) - Func(x - dx)) / dx;
        }

        private static double CalcRight(double x, double dx)
        {
            return (Func(x + dx) - Func(x)) / dx;
        }

        private static double CalcCenter(double x, double dx)
        {
            return (Func(x + dx) - Func(x - dx)) / (2 * dx);
        }

        private static double CalcSecond(double x, double dx)
        {
            return (Func(x + dx) - (2 * Func(x)) + Func(x - dx)) / (dx * dx);
        }

        public static void Exec()
        {
            int p = 5;
            double dx = 0.5f;

            Console.Write("Введите x0: ");
            double.TryParse(Console.ReadLine(), out x0);
            Console.Write("Введите xn: ");
            double.TryParse(Console.ReadLine(), out xn);
            Console.Write("Введите h: ");
            double.TryParse(Console.ReadLine(), out h);

            CalcValues(Func);
            PrintTable(xs, ys, p);

            Console.WriteLine();
            Console.WriteLine(" --- Производные первого порядка --- ");

            CalcValues(Func1);
            PrintTable(xs, ys, p);

            Console.WriteLine();
            Console.WriteLine("Левая производная");

            CalcValues(CalcLeft, dx);
            PrintTable(xs, ys, p);

            Console.WriteLine();
            Console.WriteLine("Правая производная");
            
            CalcValues(CalcRight, dx);
            PrintTable(xs, ys, p);

            Console.WriteLine();
            Console.WriteLine("Центральная производная");

            CalcValues(CalcCenter, dx);
            PrintTable(xs, ys, p);

            Console.WriteLine();
            Console.WriteLine(" --- Производные второго порядка --- ");

            CalcValues(Func2);
            PrintTable(xs, ys, p);

            Console.WriteLine();
            Console.WriteLine("Производная второго порядка");

            CalcValues(CalcSecond, dx);
            PrintTable(xs, ys, p);

            Console.ReadLine();
        }
    }
}
