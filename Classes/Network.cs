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

        public override string ToString()
        {
            for (uint i = 0; i < _Layers.Length; i++) {
                Console.SetCursorPosition((int)i * 6, 0);

                for (uint j = 0; j < _Layers[i].Length; j++)
                {
                    float value = _Layers[i][j];
                    Console.Write(Math.Round(value, 2).ToString());
                    Console.SetCursorPosition(0, (int)j);
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
