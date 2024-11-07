using System;

namespace NeuralNetwork
{
    public class Network
    {
        private float[][] _Layers;

        public float[][] Layers
        {
            get { return _Layers; }
        }

        public Network(params uint[] layerSizes)
        {
            _Layers = new float[layerSizes.Length][];

            for (uint i = 0; i < layerSizes.Length; i++) {
                uint layerSize = layerSizes[i];
                float[] layer = new float[layerSize];

                for (uint j = 0; j < layerSize; j++) {
                    layer[j] = GetRandomFloat();
                }

                _Layers[i] = layer;
            }
        }

        public uint GetLargestLayer()
        {
            uint largest = 0;

            for (uint i = 0; i < _Layers.Length; i++) {
                if (_Layers[i].Length > largest)
                    largest = (uint)_Layers[i].Length;
            }

            return largest;
        }

        public override string ToString()
        {
            uint largestLayer = GetLargestLayer();
            uint center = largestLayer / 2;

            Console.Clear();

            for (uint i = 0; i < _Layers.Length; i++) {
                int size = _Layers[i].Length;
                int halfSize = size / 2;

                for (int j = 0; j < size; j++) {
                    float value = _Layers[i][j];

                    Console.SetCursorPosition((int)i * 12, (int)center - halfSize + j);
                    Console.Write($"[{Math.Round(value, 2).ToString()}]");
                } 
            }

            return "";
        }

        public static float GetRandomFloat()
        {
            return (float)Program.random.NextDouble();
        }
            
        public void FeedForward(float[] inputs)
        {
            _Layers[1] = inputs;


        }
    }
}
