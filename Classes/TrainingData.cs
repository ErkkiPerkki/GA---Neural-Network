using System.Threading;
using System;
using System.IO;

namespace NeuralNetwork {
    public class TrainingData {
        private string _Path;
        private string[] _Source;
        private string[][] _Data;
        private (int Rows, int Columns) _Size;

        public string[][] Data {
            get { return _Data; }
        }
        public (int Rows, int Columns) Size {
            get { return _Size; } 
        }

        public TrainingData(string path) {
            _Path = path;
            _Source = File.ReadAllLines(path);

            _Size = (_Source.Length, 0);
            _Data = new string[_Size.Rows][];

            for (int i = 0; i < _Size.Rows; i++) {
                _Data[i] = _Source[i].Split(',');
            }

            _Size.Columns = _Data[0].Length;
        }

        public void DrawImage(uint index) {
            string[] imageData = _Data[index];

            Console.CursorVisible = false;

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

        public Matrix PackToColumnVector(uint index, bool isImage = false) {
            float[] vectorElements = new float[_Size.Columns-1];
            string[] imageData = _Data[index];

            for (uint i = 1; i < _Size.Columns; i++) {
                vectorElements[i-1] = float.Parse(imageData[i]);

                if (isImage)
                    vectorElements[i-1] /= 255;
            }

            return new Matrix(vectorElements);
        }

        public Matrix GetCorrectAnswer(uint index, uint outputLayerSize) {
            string[] data = _Data[index];
            float[] columnVector = new float[outputLayerSize];

            if (outputLayerSize == 1) {
                columnVector[0] = float.Parse(data[0]);
            }
            else {
                columnVector[int.Parse(data[0])] = 1;
            }

            return new Matrix(columnVector);
        }
    }
}
