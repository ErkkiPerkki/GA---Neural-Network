using System;

namespace NeuralNetwork
{
    public class Network
    {
        public Layer[] Layers {
            get;
            private set;
        }

        public Network(params uint[] layerSizes)
        {
            Layers = new Layer[layerSizes.Length];

            for (uint i = 0; i < layerSizes.Length; i++) {
                uint layerSize = layerSizes[i];
                Layer layer = new Layer(layerSize);

                for (uint j = 0; j < layerSize; j++) {
                    layer.Neurons[j] = 1f;
                }

                Layers[i] = layer;
            }
        }

        public uint GetLargestLayer()
        {
            uint largest = 0;

            for (uint i = 0; i < Layers.Length; i++) {
                if (Layers[i].Size > largest)
                    largest = (uint)Layers[i].Size;
            }

            return largest;
        }

        public override string ToString()
        {
            uint largestLayer = GetLargestLayer();
            uint center = largestLayer / 2;

            Console.Clear();

            for (uint i = 0; i < Layers.Length; i++) {
                uint size = Layers[i].Size;
                uint halfSize = size / 2;

                for (uint j = 0; j < size; j++) {
                    float value = Layers[i].Neurons[j];

                    Console.SetCursorPosition((int)i * 12, (int)(center - halfSize + j));
                    Console.Write($"[{Math.Round(value, 2).ToString()}]");
                } 
            }

            return "";
        }

        public static float GetRandomFloat() {
            return (float)Program.random.NextDouble();
        }

        public static float Sigmoid(float x) {
            return 1 / (1 + (float)Math.Pow(Math.E, -x));
        }
            
        public float[] FeedForward(float[] inputs) {
            uint outputLayerSize = Layers[Layers.Length-1].Size;
            float[] outputs = new float[outputLayerSize];

            for (uint i = 1; i < Layers.Length-1; i++) {
                Layer currentLayer = Layers[i];
                Layer nextLayer = Layers[i + 1];


                Console.WriteLine($"a: {currentLayer.Size} b: {nextLayer.Size}");
            }

            return outputs;
        }
    }
}
