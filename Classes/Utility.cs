using System;

namespace NeuralNetwork {
    public static class Utility {
        public static Random random = new Random();

        public static float GetRandomFloat() {
            return (float)random.NextDouble();
        }

        public static void ColoredPrint(string text, ConsoleColor color) {
            ConsoleColor previousColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = previousColor;
        }

        public static void ColoredWrite(string text, ConsoleColor color) {
            ConsoleColor previousColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = previousColor;
        }
    }
}
