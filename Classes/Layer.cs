﻿namespace NeuralNetwork
{
    public class Layer
    {
        private uint _Size;
        private Matrix _Neurons;
        private Matrix _UnactivatedNeurons;
        private Matrix _Weights;
        private Matrix _Biases;

        public uint Size {
            get { return _Size; }
        }

        public Matrix Neurons {
            get { return _Neurons; }
            set { _Neurons = value; }
        }

        public Matrix UnactivatedNeurons {
            get { return _UnactivatedNeurons; }
            set { _UnactivatedNeurons = value; }
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

            Matrix biases = new Matrix(size, 1);
            biases.PopulateWithRandom(-1, 1);

            _Neurons = new Matrix(size, 1);
            _UnactivatedNeurons = new Matrix(size, 1);
            _Biases = biases;
        }

        public Layer(Matrix weights) {
            _Size = weights.Rows;

            _Neurons = new Matrix(_Size, 1);
            _UnactivatedNeurons = new Matrix(_Size, 1);
            _Weights = weights;
            
            _Biases = new Matrix(_Size, 1);
            _Biases.PopulateWithRandom(-1, 1);
        }

        public void ConnectLayer(Layer nextLayer) {    
            _Weights = new Matrix(nextLayer._Size, _Size);
            _Weights.PopulateWithRandom(-0.1f, 0.1f);
        }
    }
}
