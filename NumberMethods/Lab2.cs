using System;
using System.Collections.Generic;
using System.Globalization;

namespace PMN {
    class Lab2 {
        private const double a = 0;
        private const double b = Math.PI;
        private const double h = Math.PI / 5;

        private const int n = (int)((b - a) / h);

        private static List<double> xs = new List<double>(n + 1);
        private static List<double> ys = new List<double>(n + 1);
        private static List<double> ds = new List<double>(n);
        private static List<double> bs = new List<double>(n);

        private static double Func(double x) {
            return x + Math.Cos(x) - 1;
        }

        private static void Print(List<double> array) {
            for (int i = 0; i < array.Count; ++i) {
                Console.Write($"{array[i].ToString("0.00")}  ");
            }
        }

        private static void Print(double[] array) {
            for (int i = 0; i < array.Length; ++i) {
                Console.Write($"{array[i].ToString("0.00")}  ");
            }
        }

        private static double[] GaussianMethod(double[,] matrix, double[] f) {
            double[] x = new double[n + 1];

            for (int i = 0; i < n; i++) {
                f[i] /= matrix[i, i];

                for (int j = n - 1; j >= i; j--) {
                    matrix[i, j] /= matrix[i, i];
                }

                for (int j = i + 1; j < n; j++) {
                    f[j] -= f[i] * matrix[j, i];

                    for (int k = n - 1; k >= i; k--) {
                        matrix[j, k] -= matrix[j, i] * matrix[i, k];
                    }
                }
            }

            for (int i = n - 1; i >= 0; i--) {
                x[i] = f[i];

                for (int j = i + 1; j < n; j++) {
                    x[i] -= x[j] * matrix[i, j];
                }
            }

            return x;
        }

        public static void Exec() {
            for (int i = 0; i < n; ++i) {
                xs.Add(h * i);
                ys.Add(Func(xs[i]));
            }

            Console.WriteLine("X: ");
            Print(xs);

            Console.WriteLine();

            Console.WriteLine("Y: ");
            Print(ys);

            double[] A = new double[n + 1];
            A = ys.ToArray();

            double[,] matrix = new double[n + 1, n + 1];
            matrix[0, 0] = 1;
            matrix[n - 1, n - 1] = 1;

            for (int i = 1; i < n - 1; i++) {
                matrix[i, i - 1] += h;
                matrix[i, i] += 4 * h;
                matrix[i, i + 1] += h;
            }

            double[] matrixR = new double[n + 1];
            matrixR[0] = 0;
            matrixR[xs.Count - 1] = 0;

            for (int i = 1; i <= xs.Count - 2; i++) {
                matrixR[i] = 6 * ((ys[i + 1] - ys[i]) / h - (ys[i] - ys[i - 1]) / h);
            }

            Console.WriteLine();

            double[] C = new double[n];
            C = GaussianMethod(matrix, matrixR);
            Print(C);

            List<double> d = new List<double>();
            List<double> b = new List<double>();

            d.Add(0);
            b.Add(0);

            for (int i = 1; i < n + 1; i++) {
                d.Add((C[i] - C[i - 1]) / h);
            }

            for (int i = 1; i < n + 1; i++) {
                b.Add(C[i] * h / 2 - d[i] * h * h / 6 + (ys[i] - ys[i - 1]) / h);
            }

            double x1 = xs[0] + 0.3;
            double x2 = xs[0] + 0.5 * h;
            double x3 = xs[n] - 0.5 * h;

            int p1 = (int)Math.Floor((x1 - xs[0]) / h);
            int p2 = (int)Math.Floor((x2 - xs[0]) / h);
            int p3 = (int)Math.Floor((x3 - xs[0]) / h);

            Console.WriteLine(p3);

            double result1 =
                A[p1 + 1] + b[p1 + 1] * (x1 - xs[p1 + 1]) + C[p1 + 1] /
                2 * (x1 - xs[p1 + 1]) * (x1 - xs[p1 + 1]) + d[p1 + 1] /
                6 * (x1 - xs[p1 + 1]) * (x1 - xs[p1 + 1]) * (x1 - xs[p1 + 1]);

            double result2 =
                A[p2 + 1] + b[p2 + 1] * (x2 - xs[p2 + 1]) + C[p2 + 1] /
                2 * (x2 - xs[p2 + 1]) * (x2 - xs[p2 + 1]) + d[p2 + 1] /
                6 * (x2 - xs[p2 + 1]) * (x2 - xs[p2 + 1]) * (x2 - xs[p2 + 1]);

            double result3 =
                A[p3 + 1] + b[p3 + 1] * (x3 - xs[p3 + 1]) + C[p3 + 1]
                / 2 * (x3 - xs[p3 + 1]) * (x3 - xs[p3 + 1]) + d[p3 + 1]
                / 6 * (x3 - xs[p3 + 1]) * (x3 - xs[p3 + 1]) * (x3 - xs[p3 + 1]);

            Console.WriteLine();
            Console.WriteLine($"{result1} ~ { 2.2 * x1 - Math.Pow(2, x1)}");
            Console.WriteLine($"{result2} ~ { 2.2 * x2 - Math.Pow(2, x2)}");
            Console.WriteLine($"{result3} ~ { 2.2 * x3 - Math.Pow(2, x3)}");

            Console.WriteLine();

            for (int i = 1; i < n + 1; i++) {
                Console.WriteLine(
                    $"уравнение {i}:  {A[i].ToString("0.00", CultureInfo.InvariantCulture)}" +
                    $" + ({b[i].ToString("0.00", CultureInfo.InvariantCulture)})" +
                    $" * (x- {xs[i].ToString("0.00", CultureInfo.InvariantCulture)})" +
                    $" + ({C[i].ToString("0.00", CultureInfo.InvariantCulture)})" +
                    $" / 2 * (x - {xs[i].ToString("0.00", CultureInfo.InvariantCulture)})" +
                    $" * (x - {xs[i].ToString("0.00", CultureInfo.InvariantCulture)})" +
                    $" + ({d[i].ToString("0.00", CultureInfo.InvariantCulture)})" +
                    $" / 6 * (x - {xs[i].ToString("0.00", CultureInfo.InvariantCulture)})" +
                    $" * (x - {xs[i].ToString("0.00", CultureInfo.InvariantCulture)})" +
                    $" * (x - {xs[i].ToString("0.00", CultureInfo.InvariantCulture)})"
                );
            }
        }
    }
}
