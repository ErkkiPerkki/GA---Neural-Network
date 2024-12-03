using System;

namespace NeuralNetwork
{
    public class Program
    {
        public static Random random = new Random();

        static void Main()
        {
            Network test = new Network(4, 8, 6, 2, 4);

            float[] output = test.FeedForward(new float[4]{1f, 1f, 1f, 1f });
            foreach (float value in output) {
                //Console.WriteLine(value);
            }

            Matrix a = new Matrix(new float[][] { new float[] { 1f, 1f}, new float[] { 1f, 1f} });
            Matrix b = new Matrix(new float[][] { new float[] { 2f, 3f}, new float[] { 2f, 3f} });
            Matrix c = a * b;

            Console.ReadKey();
        }
    }
}