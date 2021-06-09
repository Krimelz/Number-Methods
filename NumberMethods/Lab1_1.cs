using System;
using System.Collections.Generic;

namespace PMN
{
    class Lab1_1
    {
        public static double Func(double x)
        {
            return x + Math.Cos(x) - 1;
            //return 0.25f * (Math.Pow(Math.E, 0.5f * x) - x);
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
						max = Math.Max(Math.Abs(Derivative(a, n)), Math.Abs(Derivative(b, n)));
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
					int perA = ((int)(a / Math.PI));
					int perB = ((int)(b / Math.PI));
					int chA = ((int)(a / (Math.PI / 2f)));
					int chB = ((int)(b / (Math.PI / 2f)));


					if (perA != perB && chA != chB)
					{
						max = Math.Abs(Derivative(p, n));
					}
					else
					{
						max = Math.Max(Math.Abs(Derivative(a, n)), Math.Abs(Derivative(b, n)));
					}
				}
			}

			Console.WriteLine($"M = {max}");
			return max;
		}

		public static double AbsoluteError(double a, double b, double x, int n, params double[] xs)
        {
			return M(a, b, n + 1) / Factorial(n + 1) * Math.Abs(Omega(x, xs));
		}

		public static double Lagrange(double x, params double[] xs)
        {
			double top = 1f;
			double bot = 1f;
			double result = 0f;

			string t = "", b = "", r = "";

            for (int k = 0; k < xs.Length; ++k)
            {
				for (int i = 0; i < xs.Length; ++i)
                {
					if (i != k)
                    {
						top *= x - xs[i];
						t += $"(x -{xs[i]})";
						bot *= xs[k] - xs[i];
						b += $"({xs[k]} - {xs[i]})";
                    }
                }

				result += (top / bot) * Func(xs[k]);
				r += t + "/" + b + $"{Func(xs[k])}";
				top = 1f;
				bot = 1f;
            }

			//Console.WriteLine(r);

			/*
				double tt = ((x-(3.14/5)*1)*(x-(3.14/5)*2)*(x-(3.14/5)*3)) / (((3.14/5)*0-(3.14/5)*1)*((3.14/5)*0-(3.14/5)*2)*((3.14/5)*0-(3.14/5)*3)) * (((3.14/5)*0+cos((3.14/5)*0))-1)+ ((x-(3.14/5)*0)*(x-(3.14/5)*2)*(x-(3.14/5)*3)) / (((3.14/5)*1-(3.14/5)*0)*((3.14/5)*1-(3.14/5)*2)*((3.14/5)*1-(3.14/5)*3)) * (((3.14/5)*1+cos((3.14/5)*1))-1)+ ((x-(3.14/5)*1)*(x-(3.14/5)*0)*(x-(3.14/5)*3)) / (((3.14/5)*2-(3.14/5)*1)*((3.14/5)*2-(3.14/5)*0)*((3.14/5)*2-(3.14/5)*3)) * (((3.14/5)*2+cos((3.14/5)*2))-1)+ ((x-(3.14/5)*1)*(x-(3.14/5)*2)*(x-(3.14/5)*0)) / (((3.14/5)*3-(3.14/5)*1)*((3.14/5)*3-(3.14/5)*2)*((3.14/5)*3-(3.14/5)*0)) * (((3.14/5)*3+cos((3.14/5)*3))-1);
				*/
			return result;
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

			// выбор узлов
			int first = 0, last = n;

			while (last < xs.Count - 1)
            {
				if (x >= xs[first] && x <= xs[last])
				{
					for (int i = first; i <= last; i++)
                    {
						args.Add(xs[i]);	
                    }

					break;
				}
				else
                {
					first++;
					last++;
                }
            }

			double result = Lagrange(x, args.ToArray());
			Console.WriteLine($"result = {result}");

			Console.WriteLine($"Относительная погрешность = {Func(x) - result}");
			Console.WriteLine($"Абсолютная погрешность = {AbsoluteError(args[0], args[args.Count - 1], x, n, args.ToArray())}");

			Console.ReadLine();
		}
    }
}
