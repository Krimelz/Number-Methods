﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMN {
    class Lab3 {
        private delegate double Func(double x, uint n = 0);

        private static List<double> xs = new List<double>();
        private static List<double> ys = new List<double>();

        private static double Func1(double x, uint n = 0) {
            if (n == 1) {
                return 1;
            } else if (n >= 2) {
                return 0;
            }

            return x;
        }

        private static double Func2(double x, uint n = 0) {
            if (n == 1) {
                return x;
            } else if (n == 2) {
                return 1;
            } else if (n >= 3) {
                return 0;
            }

            return x * x;
        }

        private static double Func3(double x, uint n = 0) {
            if (n == 1) {
                return x * x;
            } else if (n == 2) {
                return x;
            } else if (n == 3) {
                return 1;
            } else if (n >= 4) {
                return 0;
            }

            return x * x * x;
        }

        private static double Distance(Func func) {
            double sum = 0f;

            for (int i = 0; i < xs.Count; ++i) {
                sum += Math.Pow(ys[i] - func(xs[i]), 2f);
            }

            return sum;
        }

        private static double[] CalcCoef(Func f, int n) {
            double[] coefs = new double[n];
            double[,] a = new double[n, n];
            double[] b = new double[n];

            if (n == 2)  {
                for (int i = 0; i < n; i++)  {
                    a[0, 0] += xs[i] * f(xs[i]);
                    a[0, 1] += f(xs[i]);
                    a[1, 0] += xs[i];
                    a[1, 1] += f(xs[i], 1);

                    b[0] += xs[i] * f(xs[i]);
                    b[1] += xs[i] * f(xs[i], 1);
                }
            }  else if (n == 3) {
                // TODO
            }  else if (n == 4) {
                // TODO
            }

            coefs = Kramer(a, b, n);

            return coefs;
        }

        private static double[] Kramer(double[,] a, double[] b, int n) {
            double[] result = new double[n];
            double d = Determinant(a);

            for (int i = 0; i < n; i++) {
                result[i] = Determinant(CreateMatrix(a, b, i));
            }

            for (int i = 0; i < result.Length; i++) {
                result[i] = d / result[i];
            }

            return result;
        }

        private static double[,] CreateMatrix(double[,] a, double[] b, int index) {
            double[,] matrix = new double[a.GetLength(0), a.GetLength(1)];

            matrix = a;

            for (int i = 0; i < b.Length; i++) {
                matrix[i, index] = b[i];
            }

            return matrix;
        }

        private static double Determinant(double[,] a) {
            double l = 0f;
            double r = 0f;

            for (int i = 0; i < a.GetLength(0); i++)  {
                double p = 1f;

                for (int j = 0; j < a.GetLength(1); j++) {
                    int index = (j + i) % a.GetLength(1);
                    p *= a[j, index];
                }

                l += p;
            }

            for (int i = 0; i < a.GetLength(0); i++) {
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

        public static void Exec() {
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

            PrintTable(xs, ys, 4);

            double[] c = new double[2];
            c = CalcCoef(Func1, 2);

            Console.ReadLine();
        }
    }
}
