using ExtensibilityAPI;

namespace PythagoreanTheorem {
    public class Pythagorean :IOperation {
        public string Name => "Pythagorean";

        public double Calculate(double a, double b) {
            return (double)System.Math.Sqrt(a * a + b * b);
        }
    }
}
