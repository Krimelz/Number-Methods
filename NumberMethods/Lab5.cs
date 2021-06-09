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
        private static List<double> ys = new List<double>();

        private static double Func(double x) {
            return x / (2 * x + 3);
        }

        private static double PreFunc(double x) {
            return -x / (3 * (1 / Math.Tan(3 * x))) + (1 / 9) * Math.Log(Math.Sin(3 * x)); 
        }

        private static void CalcValues(int n) {

        }

        public static void Exec() {
            Console.Write("Введите x0: ");
            double.TryParse(Console.ReadLine(), out x0);
            Console.Write("Введите xn: ");
            double.TryParse(Console.ReadLine(), out xn);
            Console.Write("Введите n: ");
            int.TryParse(Console.ReadLine(), out n);
        }
    }
}
