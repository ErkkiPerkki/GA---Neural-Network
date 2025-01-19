using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace NeuralNetwork
{
    public class Network {
        private Layer[] _Layers;

        public Layer[] Layers { get { return _Layers; } }

        public Network(params uint[] layerSizes) {
            _Layers = new Layer[layerSizes.Length];

            for (uint i = 0; i < layerSizes.Length; i++) {
                uint layerSize = layerSizes[i];
                Layer layer = new Layer(layerSize);

                for (uint j = 0; j < layerSize; j++) {
                    layer.Neurons.Elements[j][0] = 1f;
                }

                if (i > 0) {
                    _Layers[i - 1].ConnectLayer(layer);
                }

                _Layers[i] = layer;
            }
        }

        public uint GetLargestLayer() {
            uint largest = 0;

            for (uint i = 0; i < _Layers.Length; i++) {
                if (_Layers[i].Size > largest)
                    largest = (uint)_Layers[i].Size;
            }

            return largest;
        }

        public override string ToString() {
            uint largestLayer = GetLargestLayer();
            uint center = largestLayer / 2;

            Console.CursorVisible = false;

            for (uint i = 0; i < _Layers.Length; i++) {
                uint size = _Layers[i].Size;
                uint halfSize = size / 2;
                uint layerStartPosition = center - halfSize;
                uint x = i * 12;

                for (uint j = 0; j < size; j++) {
                    float value = _Layers[i].Neurons.Elements[j][0];
    
                    uint y = layerStartPosition + j;

                    ConsoleColor color = value >= 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    ConsoleColor previousColor = Console.ForegroundColor;
                    Console.ForegroundColor = color;

                    Console.SetCursorPosition((int)x, (int)y);
                    Console.Write($"[{value.ToString("0.00")}]    ");

                    Console.ForegroundColor = previousColor;
                }
            }

            Console.SetCursorPosition(0, (int)largestLayer + 1);
            Console.CursorVisible = true;

            return "";
        }

        public static float Sigmoid(float x) {
            return 1 / (1 + (float)Math.Pow(Math.E, -x));
        }

        public static Matrix Sigmoid(Matrix matrix) {
            float[][] result = new float[matrix.Rows][];

            for (uint row = 0; row < matrix.Rows; row++) {
                result[row] = new float[matrix.Columns];

                for (uint column = 0; column < matrix.Columns; column++) {
                    float value = matrix.Elements[row][column];

                    result[row][column] = Sigmoid(value);
                }
            }

            return new Matrix(result);
        }

        public static Matrix SigmoidDerivative(Matrix matrix) {
            float[][] result = new float[matrix.Rows][];

            for (uint row = 0; row < matrix.Rows; row++) {
                result[row] = new float[matrix.Columns];

                for (uint column = 0; column < matrix.Columns; column++) {
                    float value = matrix.Elements[row][column];

                    result[row][column] = Sigmoid(value) * (1 - value);
                }
            }

            return new Matrix(result);
        }

        public Matrix FeedForward(Matrix inputs) {
            uint outputLayerSize = _Layers[_Layers.Length-1].Size;
            float[] outputs = new float[outputLayerSize];

            _Layers[0].Neurons = inputs;

            for (uint i = 0; i < _Layers.Length-1; i++) {
                Layer currentLayer = _Layers[i];
                Layer nextLayer = _Layers[i + 1];

                Matrix activation = currentLayer.Weights * currentLayer.Neurons + nextLayer.Biases;
                nextLayer.UnactivatedNeurons = activation;
                nextLayer.Neurons = Sigmoid(activation);
            }

            return new Matrix(_Layers[_Layers.Length-1].Neurons.Elements);
        }

        public void Backpropogate(Matrix expectedOutput, float learningRate) {
            Layer outputLayer = _Layers[_Layers.Length - 1];
        }

        public void Train(TrainingData data, float learningRate) {
            for (uint i = 0; i < data.Data.Length; i++) {
                Console.Clear();

                string imageData = data.Data[i];
                string[] splitData = imageData.Split(',');
                Matrix inputMatrix = data.PackToColumnVector(i);

                uint correctAnswer = uint.Parse(splitData[0]);
                float[] expectedOutput = new float[10];
                expectedOutput[correctAnswer] = 1;

                FeedForward(inputMatrix);
                Backpropogate(new Matrix(expectedOutput), learningRate);

                Thread.Sleep(3000);
            }
            
        }
    }
}
