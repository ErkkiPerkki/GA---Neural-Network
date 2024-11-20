namespace NeuralNetwork
{
    public class Layer
    {
        public uint Size {
            get {return Size;}
            private set {Size = value;}
        }

        public float[] Neurons {
            get {return Neurons;}
            private set {Neurons = value;}
        }

        public float[] Weights {
            get {return Weights;}
            private set {Weights = value;}
        }

        public float[] Biases {
            get {return Biases;}
            private set {Biases = value;}
        }

        public Layer(uint size)
        {
            Size = size;
            Weights = new float[size];
        }

        public void ConnectLayer(Layer nextLayer) {
            uint amountOfWeights = Size * nextLayer.Size;

            Weights = new float[amountOfWeights];
            Biases = new float[amountOfWeights];
        }
    }
}
