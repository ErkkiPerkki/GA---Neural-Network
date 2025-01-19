using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace NeuralNetwork
{
    public class Program
    {
        public static void Main()
        {
            // Make floats separate decimals by . instead of ,
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            //Console.SetWindowSize(150, 40);

            string root = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string trainingDataPath = $@"{root}\TrainingData\mnist_train.csv";
            Debug.Assert(File.Exists(trainingDataPath), $"Couldn't find training data in path [{trainingDataPath}]");

            Network testNetwork = new Network(784, 64, 10); //784, 64, 10
            TrainingData data = new TrainingData(trainingDataPath);
            testNetwork.Train(data, 0.01f);

            //for (uint i = 0; i < data.Data.Length; i++) {
            //    Matrix result = testNetwork.FeedForward(data.PackToColumnVector(i));
            //    data.DrawImage(i);

            //    Thread.Sleep(500);
            //}
            
            //Matrix a = new Matrix(new float[][] { new float[] { 1f, 1f}, new float[] { 1f, 1f} });
            //Matrix b = new Matrix(new float[][] { new float[] { 2f, 3f}, new float[] { 2f, 3f} });
            //Matrix c = a + b;

            //Console.WriteLine(a.ToString());
            //Console.WriteLine(b.ToString());
            //Console.WriteLine(c.ToString());

            Console.ReadKey();
        }

        public static void FormatOutput(Matrix output)
        {
            
        }
    }
}