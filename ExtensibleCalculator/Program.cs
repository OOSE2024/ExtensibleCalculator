

using System.Reflection;
using ExtensibilityAPI;

namespace ExtensibleCalculator {

    class Addition : IOperation {
        public double Calculate(double a, double b) {
            return a + b;
        }
        public string Name => "Addition";
    }
    class Subtraction : IOperation {
        public double Calculate(double a, double b) {
            return a - b;
        }
        
        public string Name => "Subtraction";
    }
    class Multiplication : IOperation {
        public double Calculate(double a, double b) {
            return a * b;
        }
        
        public string Name => "Multiplication";
    }
    class Division : IOperation {
        public double Calculate(double a, double b) {
            return a / b;
        }
        
        public string Name => "Division";
    }
    internal class Program {
        static void Main(string[] args) {

            var operations = GetOperations().ToArray();

            while (true) {
                int selectedOperation;
                do {
                    Console.Clear();
                    Console.WriteLine("*** Extensible Calculator v1.0 ***");
                    Console.WriteLine();
                    for (int i = 0; i < operations.Length; i++) {
                        Console.WriteLine($"{i + 1}. {operations[i].Name}");
                    }
                    Console.WriteLine();
                    selectedOperation = ReadInt("Select an operation:");
                } while (selectedOperation < 0 || selectedOperation > operations.Length);

                Console.WriteLine();
                if (selectedOperation == 0) {
                    break;
                }
                IOperation operation = operations[selectedOperation - 1];
                int a = ReadInt("Enter first number: ");
                int b = ReadInt("Enter second number: ");
                Console.WriteLine($"Result: {operation.Calculate(a, b)}");
                Console.WriteLine("Hit any key to proceed");
                Console.ReadKey();
                Console.WriteLine();
            }
        }

        private static int ReadInt(string message) {
            bool valid = false;
            int result = 0;
            while (!valid) {
                Console.Write(message);
                valid = int.TryParse(Console.ReadLine(), out result);
            }
            return result;
        }

        static IEnumerable<IOperation> GetBuildInOperations() {
            yield return new Addition();
            yield return new Subtraction();
            yield return new Multiplication();
            yield return new Division();
            // More build-in operations can be added here
        }

        static IEnumerable<IOperation> GetExternalOperations() {
            if (!Directory.Exists("Extensions")) {
                Type operationType = typeof(IOperation);
            }

            foreach (string file in Directory.EnumerateFiles("Extensions", "Extension*.dll")) {
                Assembly assembly = Assembly.LoadFrom(file);
                foreach (Type type in assembly.GetTypes()) {
                    if (typeof(IOperation).IsAssignableFrom(type)) {
                        yield return (IOperation)Activator.CreateInstance(type);
                    }
                }
            }
            yield break;
        }
        static IEnumerable<IOperation> GetOperations() {
            return GetBuildInOperations().Concat(GetExternalOperations());
        }
    }
}
