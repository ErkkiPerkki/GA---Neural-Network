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

            string root = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            // Train
            string trainingDataPath = $@"{root}\TrainingData\mnist_train.csv";
            Debug.Assert(File.Exists(trainingDataPath), $"Couldn't find training data in path [{trainingDataPath}]");

            Network testNetwork = new Network(784, 100, 10);
            TrainingData data = new TrainingData(trainingDataPath);
            testNetwork.Train(data, 0.05f, 3);

            // Test
            //string networkDataPath = $@"{root}\NetworkData\NetworkData.json";
            //Network loadedNetwork = new Network(networkDataPath);
            //TrainingData testData = new TrainingData($@"{root}\TrainingData\mnist_test.csv");

            //float accuracySum = 0;
            //for (uint i = 0; i < testData.Size.Rows; i++) {
            //    Console.Clear();

            //    Matrix result = loadedNetwork.FeedForward(testData.PackToColumnVector(i));
            //    accuracySum += FormatNetworkOutput(result);
            //    Console.WriteLine(accuracySum / i);

            //    testData.DrawImage(i);

            //    Thread.Sleep(25);
            //}

            Console.ReadKey();
        }

        public static float FormatNetworkOutput(Matrix networkOutput) {
            Console.WriteLine(networkOutput);

            float largestOutput = 0;
            int largestOutputIndex = 0;

            for (int row = 0; row < networkOutput.Rows; row++) {
                float value = networkOutput.Elements[row][0];

                if (value > largestOutput) {
                    largestOutput = value;
                    largestOutputIndex = row;
                }
            }

            Utility.ColoredWrite(largestOutputIndex.ToString(), ConsoleColor.DarkCyan);
            Utility.ColoredWrite(": ", ConsoleColor.White);
            Utility.ColoredWrite(largestOutput.ToString(), ConsoleColor.Green);
            Console.WriteLine();

            return largestOutput;
        }
    }
}