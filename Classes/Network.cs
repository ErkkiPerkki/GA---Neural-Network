using System;

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

            Console.Clear();

            for (uint i = 0; i < _Layers.Length; i++) {
                uint size = _Layers[i].Size;
                uint halfSize = size / 2;

                for (uint j = 0; j < size; j++) {
                    float value = _Layers[i].Neurons.Elements[j][0];

                    ConsoleColor color = value > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    ConsoleColor previousColor = Console.ForegroundColor;
                    Console.ForegroundColor = color;

                    Console.SetCursorPosition((int)i * 12, (int)(center - halfSize + j));
                    Console.Write($"[{Math.Round(value, 2).ToString("0.00")}]");

                    Console.ForegroundColor = previousColor;
                }
            }

            Console.SetCursorPosition(0, (int)largestLayer + 1);

            return "";
        }

        public static float Sigmoid(float x) {
            return 1 / (1 + (float)Math.Pow(Math.E, -x));
        }

        public Matrix FeedForward(Matrix inputs) {
            uint outputLayerSize = _Layers[_Layers.Length-1].Size;
            float[] outputs = new float[outputLayerSize];

            _Layers[0].Neurons = inputs;

            for (uint i = 0; i < _Layers.Length-1; i++) {
                Layer currentLayer = _Layers[i];
                Layer nextLayer = _Layers[i + 1];

                nextLayer.Neurons = currentLayer.Weights * currentLayer.Neurons + nextLayer.Biases;
            }

            return new Matrix(_Layers[_Layers.Length-1].Neurons.Elements);
        }
    }
}
