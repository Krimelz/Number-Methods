using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMN {
    class lab10 {
        private static int size;
        private static double[,] extendedMatrix;

        private static void Input(int n) {
            Console.WriteLine("Введите коэффициенты");

            for (int i = 0; i < n; ++i) {
                Console.WriteLine($"Строка {i + 1}");

                for (int j = 0; j < n; ++j) {
                    extendedMatrix[i, j] = float.Parse(Console.ReadLine());
                }
            }
        }

        public static void Exec() {
            size = int.Parse(Console.ReadLine());
            extendedMatrix = new double[size, size + 1];

            Input(size);
            SquareRoot();
        }

        public static void SquareRoot() {
            double[,] s = new double[size, size];
            s[0, 0] = Math.Sqrt(extendedMatrix[0, 0]);

            for (int j = 1; j < size; j++) {
                s[0, j] = extendedMatrix[0, j] / s[0, 0];
            }

            for (int i = 1; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    if (i == j) {
                        double sum = 0f;

                        for (int k = 0; k < i; k++) {
                            sum += s[k, i] * s[k, i];
                        }

                        double c = 0f;
                        Console.WriteLine(extendedMatrix[i, i]);
                        s[i, i] = Math.Sqrt(extendedMatrix[i, i] - sum);
                        c = s[i, i];
                    } else {
                        if (i < j) {
                            double sum = 0f;
                            for (int k = 0; k < i; k++) {
                                sum += s[k, i] * s[k, j];
                            }

                            s[i, j] = (extendedMatrix[i, j] - sum) / s[i, i];
                        }
                    }
                }
            }

            Console.Write("S:  ");

            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    Console.Write($"{s[i, j]} ");
                }

                Console.WriteLine();
            }

            double[,] st = Transpose(s);
            Console.Write("St:  ");

            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    Console.Write($"{st[i, j]} ");
                }

                Console.WriteLine();
            }

            double[] f = new double[size];

            for (int i = 0; i < size; i++) {
                f[i] = extendedMatrix[i, 3];
            }

            double[] xs = new double[size];
            double[] ys = new double[size];

            ys = CalcT(ys, st, f);
            CalcX(xs, s, ys);
        }

        public static double[] CalcT(double[] ys, double[,] matrix, double[] f) {
            ys[0] = f[0] / matrix[0, 0];
            double a = 0;

            for (int i = 1; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    if (j == 0) {
                        ys[i] = f[i];
                    }

                    if (i == j) {
                        a = matrix[i, i];
                    } else {
                        ys[i] -= matrix[i, j] * ys[j];
                    }
                }

                ys[i] = ys[i] / a;
            }

            for (int i = 0; i < size; i++) {
                Console.WriteLine(ys[i]);
            }

            return ys;
        }

        public static double[] CalcX(double[] xs, double[,] matrix, double[] ys) {
            Console.WriteLine();

            xs[size - 1] = ys[size - 1] / matrix[size - 1, size - 1];

            double a = 0;

            for (int i = size - 2; i >= 0; i--) {
                for (int j = size - 1; j >= 0; j--) {
                    if (j == size - 1) {
                        xs[i] = ys[i];
                    }

                    if (i == j) {
                        a = matrix[i, i];
                    } else {
                        xs[i] -= matrix[i, j] * xs[j];
                    }
                }

                xs[i] = xs[i] / a;
            }

            for (int i = 0; i < size; i++) {
                Console.WriteLine(xs[i]);
            }

            return xs;
        }

        public static double[,] Transpose(double[,] matrix) {
            double[,] transposedMatrix = new double[size, size];

            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    transposedMatrix[i, j] = matrix[j, i];
                }
            }

            return transposedMatrix;
        }
    }
}
