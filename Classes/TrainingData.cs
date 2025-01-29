using System.Threading;
using System;
using System.IO;

namespace NeuralNetwork {
    public class TrainingData {
        private string _Path;
        private string[] _Source;
        private string[][] _Data;
        private (int, int) _Size;


        public string[][] Data {
            get { return _Data; }
        }

        public TrainingData(string path) {
            _Path = path;
            _Source = File.ReadAllLines(path);

            _Size = (_Source.Length, _Source[0].Length);
            _Data = new string[_Size][];

            for (int i = 0; i < _Size; i++) {
                _Data[i] = new string[];
            }

        }

        public void DrawImage(uint index) {
            string source = _Data[index];
            string[] imageData = source.Split(',');

            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);

            for (uint i = 0; i < imageData.Length; i++) {
                uint brightness = uint.Parse(imageData[i]);

                Console.ForegroundColor = brightness < 191 ? ConsoleColor.DarkGray : ConsoleColor.White;
                Console.Write(brightness < 128 ? ' ' : '#');

                if (i % 28 == 0) {
                    Console.Write("\n");
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorVisible = true;
        }

        public Matrix PackToColumnVector(uint index) {
            float[] elements = new float[784];
            string[] imageData = _Data[index].Split(',');

            for (uint i = 0; i < 783; i++) {
                elements[i] = float.Parse(imageData[i]) / 255;
            }

            return new Matrix(elements);
        }

        public Matrix GetCorrectAnswer(uint index) {
            string[] data = _Data[index].Split(',');
            float[] columnVector = new float[10];
            columnVector[index] = 1;

            return new Matrix(columnVector);
        }
    }
}
