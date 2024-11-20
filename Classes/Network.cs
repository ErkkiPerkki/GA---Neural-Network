using System;
using System.Collections.Generic;

namespace NeuralNetwork
{
    public class Network
    {
        public Layer[] Layers {
            get {return Layers;}
            private set {Layers = value;}
        }

        public Network(params uint[] layerSizes)
        {
            Layers = new Layer[layerSizes.Length];

            for (uint i = 0; i < layerSizes.Length; i++) {
                uint layerSize = layerSizes[i];
                Layer layer = new Layer(layerSize);

                for (uint j = 0; j < layerSize; j++) {
                    layer.Neurons[j] = GetRandomFloat();
                }

                Layers[i] = layer;
            }
        }

        public uint GetLargestLayer()
        {
            uint largest = 0;

            for (uint i = 0; i < _Layers.Length; i++) {
                if (_Layers[i].Size > largest)
                    largest = (uint)_Layers[i].Size;
            }

            return largest;
        }

        public override string ToString()
        {
            uint largestLayer = GetLargestLayer();
            uint center = largestLayer / 2;

            Console.Clear();

            for (uint i = 0; i < _Layers.Length; i++) {
                uint size = _Layers[i].Size;
                uint halfSize = size / 2;

                for (uint j = 0; j < size; j++) {
                    float value = _Layers[i].Neurons[j];

                    Console.SetCursorPosition((int)i * 12, (int)(center - halfSize + j));
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
            

        }
    }
}
