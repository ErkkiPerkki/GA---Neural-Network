using System;

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

            string root = @"C:\Users\TE22ERILUN\Desktop\Gymanise Arbete\\GA - Neural Network";

            Network testNetwork = new Network(784, 64, 10);
            TrainingData data = new TrainingData($@"{root}\TrainingData\mnist_train.csv");
            testNetwork.FeedForward(data.PackToColumnVector(1));
            testNetwork.ToString();
            
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