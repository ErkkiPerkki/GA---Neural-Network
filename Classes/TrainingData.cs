using System.Threading;
using System;
using System.IO;

namespace NeuralNetwork {
    public class TrainingData {
        private string _Path;
        private string[] _Data;

        public TrainingData(string path) {
            _Path = path;
            _Data = File.ReadAllLines(path);
        }

        public void DrawImage(uint index) {
            string source = _Data[index];
            string[] imageData = source.Split(',');

            Console.Clear();

            for (uint i = 0; i < imageData.Length; i++) {
                uint brightness = uint.Parse(imageData[i]);

                Console.Write(brightness < 128 ? ' ' : '#');

                if (i % 28 == 0) {
                    Console.Write("\n");
                }
            }
        }

        public Matrix PackToColumnVector(uint index) {
            float[] elements = new float[784];

            string[] imageData = _Data[index].Split(',');

            for (uint i = 1; i < 783; i++) {
                elements[i] = float.Parse(imageData[i]) / 255;
            }

            return new Matrix(elements);
        }
    }
}
