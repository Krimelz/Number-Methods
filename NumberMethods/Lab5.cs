using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMN {
    class Lab5 {
        private static int n;
        private static double x0;
        private static double xn;

        private static List<double> xs = new List<double>();
        private static double len;
        
        private static double Func(double x) {
            return x / Math.Pow(Math.Sin(3 * x), 2);
        }

        private static double PreFunc(double x) {
            return ((-x / 3) * (1 / Math.Tan(3 * x))) + ((1 / 9) * Math.Log(Math.Sin(3 * x))); 
        }

        private static void CalcValues() {
            len = (xn - x0) / n;
            
            for (int i = 0; i <= n; i++) {
                xs.Add(x0 + len * i);
            }
        }

        private static double LeftRect() {
            double result = 0f;
            
            for (int i = 0; i < xs.Count - 1; i++) {
                result += Func(xs[i]) * (xs[i + 1] - xs[i]);
			}

            return result;
		}

        private static double RightRect() {
            double result = 0f;
            
            for (int i = 1; i < xs.Count; i++) {
                result += Func(xs[i]) * (xs[i] - xs[i - 1]);
            }

            return result;
        }

        private static double CenterRect() {
            double result = 0f;
            
            for (int i = 0; i < xs.Count - 1; i++) {
                result += Func((xs[i] + xs[i + 1]) / 2) * (xs[i + 1] - xs[i]);
			}

            return result;
		}

        private static double Trapeze() {
            double result = 0f;
            
            for (int i = 0; i < xs.Count - 1; i++) {
                result += 0.5f * (xs[i + 1] - xs[i]) * (Func(xs[i]) + Func(xs[i + 1]));
            }

            return result;
        }

        private static double Parabolic() {
            double result = 0f;

			for (int i = 0; i < xs.Count - 1; i++) {
                result += ((xs[i + 1] - xs[i]) / 6) * (Func(xs[i]) + 4 * Func((xs[i] + xs[i + 1]) / 2) + Func(xs[i + 1]));
			}

            return result;
		}

        private static double Gauss() {
            double result = 0f;

            result = (xn - x0 / 2) *
                    (Func(((x0 + xn) / 2) - ((xn - x0) / (2 * Math.Sqrt(3)))) +
                    Func(((x0 + xn) / 2) + ((xn - x0) / (2 * Math.Sqrt(3)))));

            return result;
        }

        private static double Newton() {
            double result = 0f;

            result = PreFunc(xn) - PreFunc(x0);

            return result;
		}

        public static void Exec() {
            Console.Write("Введите x0: ");
            double.TryParse(Console.ReadLine(), out x0);
            Console.Write("Введите xn: ");
            double.TryParse(Console.ReadLine(), out xn);
            Console.Write("Введите n: ");
            int.TryParse(Console.ReadLine(), out n);

            CalcValues();

            Console.WriteLine($"Левые квадраты = {LeftRect()}");
            Console.WriteLine($"Правые квадраты = {RightRect()}");
            Console.WriteLine($"Центральные квадраты = {CenterRect()}");
            Console.WriteLine($"Трапеции = {Trapeze()}");
            Console.WriteLine($"Параболы = {Parabolic()}");
            Console.WriteLine($"Гаусс = {Gauss()}");
            Console.WriteLine($"Ньютон-Лейбниц = {Newton()}");
        }
    }
}
