using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace NeuralNetwork
{
    public class Network {
        private Layer[] _Layers;
        private int _NetworkSize;

        public Layer[] Layers { get { return _Layers; } }
        public int NetworkSize { get { return _NetworkSize; } }

        public Network(params uint[] layerSizes) {
            _NetworkSize = layerSizes.Length;
            _Layers = new Layer[_NetworkSize];

            for (uint i = 0; i < _NetworkSize; i++) {
                uint layerSize = layerSizes[i];
                Layer layer = new Layer(layerSize);

                if (i > 0) {
                    _Layers[i - 1].ConnectLayer(layer);
                }

                _Layers[i] = layer;
            }
        }

        public Network(string path) {
            string jsonData = File.ReadAllText(path);
            NetworkData networkData = JsonSerializer.Deserialize<NetworkData>(jsonData, new JsonSerializerOptions {
                WriteIndented = true,
                IncludeFields = true
            });

            _NetworkSize = networkData.Weights.Length;
            _Layers = new Layer[_NetworkSize];

            for (int i = 0; i < _NetworkSize; i++) {
                int layerSize = networkData.Biases[i].Length;
                Layer layer = new Layer((uint)layerSize);

                layer.Biases = new Matrix(networkData.Biases[i]);

                if (i < _NetworkSize - 1)
                    layer.Weights = new Matrix(networkData.Weights[i]);

                _Layers[i] = layer;
            }
        }

        public uint GetLargestLayer() {
            uint largest = 0;

            for (uint i = 0; i < _NetworkSize; i++) {
                if (_Layers[i].Size > largest)
                    largest = _Layers[i].Size;
            }

            return largest;
        }

        public override string ToString() {
            uint largestLayer = GetLargestLayer();
            uint center = largestLayer / 2;

            Console.CursorVisible = false;

            for (uint i = 0; i < _NetworkSize; i++) {
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

                    float sigmoidValue = Sigmoid(value);
                    result[row][column] = sigmoidValue * (1 - sigmoidValue);
                }
            }

            return new Matrix(result);
        }

        public Matrix FeedForward(Matrix inputs) {
            if (inputs.Rows != _Layers[0].Neurons.Rows)
                throw new Exception("Input matrix must match the size of the input layer");

            _Layers[0].Neurons = inputs;

            uint outputLayerSize = _Layers[_NetworkSize - 1].Size;
            float[] outputs = new float[outputLayerSize];

            for (uint i = 0; i < _NetworkSize - 1; i++) {
                Layer currentLayer = _Layers[i];
                Layer nextLayer = _Layers[i + 1];

                Matrix layer = currentLayer.Weights * currentLayer.Neurons + nextLayer.Biases;
                nextLayer.UnactivatedNeurons = layer;
                nextLayer.Neurons = Sigmoid(layer);
            }

            return new Matrix(_Layers[_Layers.Length-1].Neurons.Elements);
        }

        public float Backpropogate(Matrix data, Matrix expectedOutput, float learningRate) {
            int layersAmount = _NetworkSize;
            Layer outputLayer = _Layers[layersAmount - 1];
            Layer lastHiddenLayer = _Layers[layersAmount - 2];

            Matrix error = expectedOutput - outputLayer.Neurons;
            Matrix outputDerivative = error.ElementWiseMultiplication(SigmoidDerivative(outputLayer.UnactivatedNeurons));

            lastHiddenLayer.Weights += learningRate * (outputDerivative * lastHiddenLayer.Neurons.Transpose());
            outputLayer.Biases += learningRate * outputDerivative;

            // Backpropagation
            for (int i = layersAmount - 2; i > 0; i--) {
                Layer currentLayer = _Layers[i];
                Layer previousLayer = _Layers[i - 1];

                Matrix hiddenError = currentLayer.Weights.Transpose() * outputDerivative;
                Matrix hiddenDerivative = hiddenError.ElementWiseMultiplication(SigmoidDerivative(currentLayer.UnactivatedNeurons));

                previousLayer.Weights += learningRate * (hiddenDerivative * previousLayer.Neurons.Transpose());
                currentLayer.Biases += learningRate * hiddenDerivative;

                outputDerivative = hiddenDerivative;
            }

            return error.GetLargestValue().Value;
        }

        public void SaveToFile() {
            string rootPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string networkDataPath = $@"{rootPath}\NetworkData\NetworkData.json";

            NetworkData networkData = new NetworkData(this);
            string networkDataJSON = networkData.ToJSON();

            File.WriteAllText(networkDataPath, networkDataJSON);
        }

        public void Train(TrainingData data, float learningRate, int epochs) {
            for (int epoch = 0; epoch < epochs; epoch++) {
                
                for (uint i = 0; i < data.Size.Rows; i++) {
                    Matrix inputData = data.PackToColumnVector(i);
                    Matrix correctAnswer = data.GetCorrectAnswer(i, _Layers[_Layers.Length - 1].Size);

                    FeedForward(inputData);
                    float networkError = Backpropogate(inputData, correctAnswer, learningRate);

                    Console.SetCursorPosition(0, 0);
                    Utility.ColoredPrint($"Epoch: {epoch + 1} / {epochs}    ", ConsoleColor.DarkCyan);

                    Console.SetCursorPosition(0, 1);
                    Utility.ColoredPrint($"Progress: {100 * i / data.Size.Rows}%   ", ConsoleColor.Green);

                    Console.SetCursorPosition(0, 2);
                    Utility.ColoredPrint($"Network Error: [{networkError}]                    ", ConsoleColor.Red);
                    //Console.Write("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                }
            }

            SaveToFile();
            Utility.ColoredPrint("Finished Training!", ConsoleColor.Green);
        }
    }
}
