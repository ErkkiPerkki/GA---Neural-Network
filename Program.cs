using System;
using System.Threading;

namespace NeuralNetwork
{
    public class Program
    {
        static void Main()
        {
            // Make floats separate decimals by . instead of ,
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            Network testNetwork = new Network(2, 8, 12, 6, 4);
            testNetwork.ToString();

            while (true) {
                Matrix inputs = new Matrix(2, 1);   
                inputs.PopulateWithRandom();

                Matrix output = testNetwork.FeedForward(inputs);
                testNetwork.ToString();

                //Thread.Sleep(10);
            }
            
            //Matrix a = new Matrix(new float[][] { new float[] { 1f, 1f}, new float[] { 1f, 1f} });
            //Matrix b = new Matrix(new float[][] { new float[] { 2f, 3f}, new float[] { 2f, 3f} });
            //Matrix c = a + b;

            //Console.WriteLine(a.ToString());
            //Console.WriteLine(b.ToString());
            //Console.WriteLine(c.ToString());

            Console.ReadKey();
        }
    }
}