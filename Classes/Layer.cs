namespace NeuralNetwork
{
    public class Layer
    {
        private uint _Size;
        private Matrix _Neurons;
        private Matrix _Weights;
        private Matrix _Biases;

        public uint Size {
            get { return _Size; }
        }

        public Matrix Neurons {
            get { return _Neurons; }
            set { _Neurons = value; }
        }

        public Matrix Weights {
            get { return _Weights; }
            set { _Weights = value; }
        }

        public Matrix Biases {
            get { return _Biases; }
            set { _Biases = value; }
        }

        public Layer(uint size)
        {
            _Size = size;

            _Neurons = new Matrix(new float[size][]);
            _Biases = new Matrix(new float[size][]);
        }

        public void ConnectLayer(Layer nextLayer) {
            uint amountOfWeights = Size * nextLayer.Size;
            
            _Weights = new Matrix(new float[amountOfWeights][]);
        }
    }
}
