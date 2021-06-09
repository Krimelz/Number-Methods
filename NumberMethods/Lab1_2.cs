using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMN
{
    class Lab1_2
    {
        public static double Diff(double x, int n, params double[] xs)
        {
            double result = 1f;

            for (int i = 0; i < n; i++)
            {
                result *= x - xs[i];
            }

            return result;
        }

        public static double Calc(params double[] xs)
        {
            double result = 0f;

            if (xs.Length > 1)
            {
                List<double> l = new List<double>();
                List<double> r = new List<double>();

                for (int i = 0; i < xs.Length - 1; ++i)
                {
                    l.Add(xs[i]);
                }

                for (int i = 1; i < xs.Length; ++i)
                {
                    r.Add(xs[i]);
                }

                result = (Calc(l.ToArray()) - Calc(r.ToArray())) / (xs[xs.Length - 1] - xs[0]);
            }
            else
            {
                return Func(xs[0]);
            }

            return result;
        }

        public static double Newton(double x, params double[] xs)
        {
            double result = 0f;

            for (int i = 0; i < xs.Length; ++i)
            {
                List<double> tmp = new List<double>();

                for (int j = 0; j <= i; ++j)
                {
                    tmp.Add(xs[j]);
                }

                result += Calc(tmp.ToArray()) * Diff(x, i, xs);
            }

            return result;
            /*
            (cos(0) - 1) 
                + (x * ( (0.628 + cos(0.628) - 1) - (cos(0) - 1) ) / 0.628) 
                + (x * (х - 0.628) * 
                    ( ( (0.628 + cos(0.628) - 1) - (cos(0) - 1) ) / 0.628 ) - (( (1.256 + cos(1.256) - 1) - (0.628 + cos(0.628) - 1) ) / (1.256 - 0.628) ) ) / (1.256 - 0.628)
            */
            //y(0) + (x - 0)(y(0.628) - y(0)) / (0.628 - 0) + (x - 0)(х - 0.628)((y(0.628) - y(0)) / (0.628 - 0) - (y(1.256) - y(0.628)) / (1.256 - 0.628)) / (1.256 - 0.628)

            //(x+cos(0)-1) + (x-0)(((x+cos(3.14/5*1)-1) - (x+cos(0)-1)) / (3.14/5*1 - 0)) + (x-0)(х-3.14/5*1)(((((x+cos(3.14/5*1)-1) - (x+cos(0)-1)) / (3.14/5*1 - 0)) - (((x+cos(3.14/5*2)-1) - (x+cos(3.14/5*1)-1)) / (3.14/5*2 - 3.14/5*1))) / (3.14/5*2 - 3.14/5*1))
        
            //(x+cos(3.14 / 5*0) -1)+((((x+cos(3.14 / 5 * 1)-1)-(x+cos(3.14 / 5 * 0)-1))/(3.14 / 5 * 1 - 3.14 / 5 * 0))*(x- 3.14 / 5 * 0))+((((((x+cos(3.14 / 5 * 2)-1)-(x+cos(3.14 / 5 * 1)-1))/(3.14 / 5 * 2 - 3.14 / 5 * 1))-(((x+cos(3.14 / 5 * 1)-1)-(x+cos(3.14 / 5 * 0)-1))/(3.14 / 5 * 1 - 3.14 / 5 * 0)))/(3.14 / 5 * 2 - 3.14 / 5 * 1))*(x- 3.14 / 5 * 0)*(x- 3.14 / 5 * 1))
        }

        public static double Func(double x)
        {
            return x + Math.Cos(x) - 1;
        }

        public static double Derivative(double x, int n)
        {
            if (n == 1)
            {
                return -Math.Sin(x) - 1;
            }
            else if (n == 2)
            {
                return -Math.Cos(x);
            }
            else if (n == 3)
            {
                return Math.Sin(x);
            }
            else if (n % 4 == 0)
            {
                return Math.Cos(x);
            }
            else if (n % 4 == 1)
            {
                return -Math.Sin(x);
            }
            else if (n % 4 == 2)
            {
                return -Math.Cos(x);
            }
            else if (n % 4 == 3)
            {
                return Math.Sin(x);
            }

            return x + Math.Cos(x) - 1;
        }

        public static double Factorial(int n)
        {
            double result = 1f;

            for (int i = 2; i <= n; ++i)
            {
                result *= i;
            }

            return result;
        }

        public static double Omega(double x, params double[] xs)
        {
            double result = 1f;

            for (int i = 0; i < xs.Length; ++i)
            {
                result *= x - xs[i];
            }

            return result;
        }

        // TODO
        public static double M(double a, double b, int n)
        {
            double d = b - a;
            double p;
            double max;

            if (n % 2 == 0)
            {
                p = Math.PI;

                if (d > Math.PI)
                {
                    max = Math.Abs(Derivative(p, n));
                }
                else
                {
                    int perA = ((int)(a / Math.PI));
                    int perB = ((int)(b / Math.PI));

                    if (perA != perB)
                    {
                        max = Math.Abs(Derivative(p, n));
                    }
                    else
                    {
                        max = Math.Max(Derivative(a, n), Derivative(b, n));
                    }
                }
            }
            else
            {
                p = Math.PI / 2f;

                if (d > Math.PI)
                {
                    max = Math.Abs(Derivative(p, n));
                }
                else
                {
                    int perA = ((int)(a / (Math.PI / 2f)));
                    int perB = ((int)(b / (Math.PI / 2f)));

                    if (perA != perB)
                    {
                        max = Math.Abs(Derivative(p, n));
                    }
                    else
                    {
                        max = Math.Max(Derivative(a, n), Derivative(b, n));
                    }
                }
            }

            return max;
        }

        public static double AbsoluteError(double a, double b, double x, int n, params double[] xs)
        {
            return M(a, b, n + 1) / Factorial(n + 1) * Math.Abs(Omega(x, xs));
        }

        public static void Exec()
        {
            List<double> xs = new List<double>();

            Console.Write("Введите a: ");
            double a = double.Parse(Console.ReadLine());
            Console.Write("Введите b: ");
            double b = double.Parse(Console.ReadLine());
            Console.Write("Введите h: ");
            double h = double.Parse(Console.ReadLine());

            int c = 0;
            for (double i = a; i <= b; i += h)
            {
                xs.Add(i);

                Console.WriteLine($"{c++}. f({i}) = {Func(i)}");
            }

            Console.Write("Введите x: ");
            double x = double.Parse(Console.ReadLine());
            Console.Write("Введите n: ");
            int n = int.Parse(Console.ReadLine());

            Console.WriteLine(Func(x));

            List<double> args = new List<double>();
            for (int i = 0; i <= n; i++)
            {
                Console.Write("Введите номер узла: ");
                args.Add(xs[int.Parse(Console.ReadLine()) - 1]);
            }

            double result = Newton(x, args.ToArray());
            Console.WriteLine(-result);

            Console.WriteLine($"Относительная погрешность = {Func(x) - result}");
            Console.WriteLine($"Абсолютная погрешность = {AbsoluteError(a, b, x, n, args.ToArray())}");

            Console.ReadLine();
        }
    }
}
