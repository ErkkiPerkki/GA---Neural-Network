namespace NeuralNetwork
{
    public class Layer
    {
        private uint _Size;
        private float[] _Neurons;
        private float[] _Weights;
        private float[] _Biases;

        public uint Size {
            get { return _Size; }
            private set { _Size = value; }
        }

        public float[] Neurons {
            get { return _Neurons; }
            private set { _Neurons = value; }
        }

        public float[] Weights {
            get { return _Weights; }
            private set { _Weights = value; }
        }

        public float[] Biases {
            get { return _Biases; }
            private set { _Biases = value; }
        }

        public Layer(uint size)
        {
            Size = size;
            Neurons = new float[size];
            Biases = new float[size];
        }

        public void ConnectLayer(Layer nextLayer) {
            uint amountOfWeights = Size * nextLayer.Size;

            Weights = new float[amountOfWeights];
        }
    }
}
