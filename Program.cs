using System;

namespace NeuralNetwork
{
    public class Program
    {
        public static Random random = new Random();

        static void Main()
        {
            Network test = new Network(4, 8, 6, 2);

            Console.ReadKey();
        }
    }
}