using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMN {
	class Lab3 {

		private delegate double Func(double x, double[] coefs);

		private static List<double> xs = new List<double>();
		private static List<double> ys = new List<double>();

		private static double Derivative1(double x, uint n = 0) {
			if (n == 0) {
				return x;
			} else if (n == 1) {
				return 1;
			} else {
				return 0;
			}
		}

		private static double Derivative2(double x, uint n = 0) {
			if (n == 0) {
				return x * x;
			} else if (n == 1) {
				return x;
			} else if (n == 2) {
				return 1;
			} else {
				return 0;
			}
		}

		private static double Derivative3(double x, uint n = 0) {
			if (n == 0) {
				return x * x * x;
			} else if (n == 1) {
				return x * x;
			} else if (n == 2) {
				return x;
			} else if (n == 3) {
				return 1;
			} else {
				return 0;
			}
		}

		private static double Func1(double x, double[] coefs) {
			return coefs[0] * x + coefs[1];
		}

		private static double Func2(double x, double[] coefs) {
			return coefs[0] * x * x + coefs[1] * x + coefs[2];
		}

		private static double Func3(double x, double[] coefs) {
			return coefs[0] * x * x * x + coefs[1] * x * x + coefs[2] * x + coefs[3];
		}

		private static double Distance(Func func, double[] coefs) {
			double sum = 0f;

			for (int i = 0; i < xs.Count; ++i) {
				sum += Math.Pow(ys[i] - func(xs[i], coefs), 2f);
			}

			return sum;
		}

		private static double[] CalcCoef(int n) {
			double[] coefs = new double[n];
			double[,] a = new double[n, n];
			double[] b = new double[n];

			if (n == 2) {
				for (int i = 0; i < n; i++) {
					a[0, 0] += xs[i] * xs[i];
					a[0, 1] += xs[i];
					a[1, 0] += xs[i];
					a[1, 1] = n;

					b[0] += xs[i] * ys[i];
					b[1] += ys[i];
				}
			} else if (n == 3) {
				for (int i = 0; i < n; i++) {
					a[0, 0] += xs[i] * xs[i] * xs[i] * xs[i];
					a[0, 1] += xs[i] * xs[i] * xs[i];
					a[0, 2] += xs[i] * xs[i];

					a[1, 0] += xs[i] * xs[i] * xs[i];
					a[1, 1] += xs[i] * xs[i];
					a[1, 2] += xs[i];

					a[2, 0] += xs[i] * xs[i];
					a[2, 1] += xs[i];
					a[2, 2] = n;

					b[0] += xs[i] * xs[i] * ys[i];
					b[1] += xs[i] * ys[i];
					b[2] += ys[i];
				}
			} else if (n == 4) {
				for (int i = 0; i < n; i++) {
					a[0, 0] += xs[i] * xs[i] * xs[i] * xs[i] * xs[i] * xs[i];
					a[0, 1] += xs[i] * xs[i] * xs[i] * xs[i] * xs[i];
					a[0, 2] += xs[i] * xs[i] * xs[i] * xs[i];
					a[0, 3] += xs[i] * xs[i] * xs[i];

					a[1, 0] += xs[i] * xs[i] * xs[i] * xs[i] * xs[i];
					a[1, 1] += xs[i] * xs[i] * xs[i] * xs[i];
					a[1, 2] += xs[i] * xs[i] * xs[i];
					a[1, 3] += xs[i] * xs[i];

					a[2, 0] += xs[i] * xs[i] * xs[i] * xs[i];
					a[2, 1] += xs[i] * xs[i] * xs[i];
					a[2, 2] += xs[i] * xs[i];
					a[2, 3] += xs[i];

					a[3, 0] += xs[i] * xs[i] * xs[i];
					a[3, 1] += xs[i] * xs[i];
					a[3, 2] += xs[i];
					a[3, 3] = n;

					b[0] += xs[i] * xs[i] * xs[i] * ys[i];
					b[1] += xs[i] * xs[i] * ys[i];
					b[2] += xs[i] * ys[i];
					b[3] += ys[i];
				}
			}

			coefs = Kramer(a, b, n);

			return coefs;
		}

		private static double[] Kramer(double[,] a, double[] b, int n) {
			double[] result = new double[n];
			double d = Determinant(a);

			for (int i = 0; i < n; i++) {
				result[i] = Determinant(CreateMatrix(a, b, i)) / d;
			}

			return result;
		}

		private static double[,] CreateMatrix(double[,] a, double[] b, int index) {
			double[,] matrix = new double[a.GetLength(0), a.GetLength(1)];

			Array.Copy(a, matrix, a.GetLength(0) * a.GetLength(1));

			for (int i = 0; i < b.Length; i++) {
				matrix[i, index] = b[i];
			}

			return matrix;
		}

		private static double Determinant(double[,] a) {
			double l = 0f;
			double r = 0f;

			for (int i = 0; i < a.GetLength(0) - 1; i++) {
				double p = 1f;

				for (int j = 0; j < a.GetLength(1); j++) {
					int index = (j + i) % a.GetLength(1);
					p *= a[j, index];
				}

				l += p;
			}

			for (int i = 0; i < a.GetLength(0) - 1; i++) {
				double p = 1f;

				for (int j = 0; j < a.GetLength(1); j++) {
					int index1 = a.GetLength(0) - j - 1;
					int index2 = (j + i + a.GetLength(1)) % a.GetLength(1);
					p *= a[index1, index2];
				}

				r += p;
			}

			return l - r;
		}

		private static void PrintTable(List<double> xs, List<double> ys, int precision) {
			Console.WriteLine();
			Console.Write("X");

			foreach (double x in xs) {
				Console.Write($"|{Math.Round(x, precision).ToString().PadLeft(10).PadRight(10)}");
			}

			Console.WriteLine("|");
			Console.Write(" ");

			for (int i = 0; i < xs.Count; ++i) {
				Console.Write("-----------");
			}

			Console.WriteLine();
			Console.Write("Y");

			foreach (double y in ys) {
				Console.Write($"|{Math.Round(y, precision).ToString().PadLeft(10).PadRight(10)}");
			}

			Console.WriteLine("|");
		}

		private void Read() {
			int n = int.Parse(Console.ReadLine());

			for (int i = 0; i < n; ++i) {
				double x, y;

				Console.Write($"Введите x{i}: ");
				double.TryParse(Console.ReadLine(), out x);
				Console.Write($"Введите y{i}: ");
				double.TryParse(Console.ReadLine(), out y);

				xs.Add(x);
				ys.Add(y);
			}
		}

		private static void PrintArray(double[] collection) {
			foreach (var item in collection) {
				Console.Write(item.ToString() + " ");
			}

			Console.WriteLine();
		}

		public static void Exec() {
			// Read();

			xs.Add(0f);
			xs.Add(0.1f);
			xs.Add(0.2f);
			xs.Add(0.3f);
			xs.Add(0.4f);
			xs.Add(0.5f);
			xs.Add(0.6f);

			ys.Add(3.02f);
			ys.Add(2.81f);
			ys.Add(2.57f);
			ys.Add(2.39f);
			ys.Add(2.18f);
			ys.Add(1.99f);
			ys.Add(1.81f);

			PrintTable(xs, ys, 4);

			double[] coefs1 = new double[2];
			double[] coefs2 = new double[3];
			double[] coefs3 = new double[4];

			coefs1 = CalcCoef(2);
			coefs2 = CalcCoef(3);
			coefs3 = CalcCoef(4);

			PrintArray(coefs1);
			PrintArray(coefs2);
			PrintArray(coefs3);

			double result1 = Distance(Func1, coefs1);
			double result2 = Distance(Func2, coefs2);
			double result3 = Distance(Func3, coefs3);

			Console.WriteLine();

			if (result1 < result2 && result1 < result3) {
				Console.WriteLine($"Линейная = {result1}");
			} else if (result2 < result1 && result2 < result3) {
				Console.WriteLine($"Квадратная = {result2}");
			} else {
				Console.WriteLine($"Кубическая = {result3}");
			}
		}
	}
}
