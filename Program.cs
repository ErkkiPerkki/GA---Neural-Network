﻿using System;

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

            Network test = new Network(4, 8, 6, 2, 4);
            test.ToString();

            Matrix output = test.FeedForward(new float[4]{1f, 1f, 1f, 1f });
            Console.WriteLine(output);
            test.ToString();

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