using System;

namespace NeuralNetwork
{
    public class Network
    {
        private Neuron[] _Neurons;

        public Neuron[] Neurons
        {
            get { return _Neurons; }
        }

        public Network(params uint[] layers)
        {
            for (int i = 0; i < layers.Length; i++) {
                Console.WriteLine(layers[i]);
            }
        }
    }
}
