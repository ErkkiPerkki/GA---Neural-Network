using System.Data;

namespace NeuralNetwork
{
    public class Matrix {

        private uint _Rows;
        private uint _Columns;
        private float[][] _Elements;

        public uint Rows { get; }
        public uint Colums { get; }
        public float[][] Elements { get; }

        public Matrix(float[][] elements) {
            uint rows = (uint)elements.Length;
            uint columns = (uint)elements[0].Length;

            _Rows = rows;
            _Columns = columns;
            _Elements = elements;
        }

        public Matrix Multiply(Matrix A, Matrix B) {
            float[][] product = new float[A.Rows][];

            for (uint i = 0; i < A.Rows; i++) {
                for (uint j = 0; j < A.Rows; j++) {
                    float sum = 0;
                    
                    for (uint k = 0; k < A.Rows; k++) {
                        sum += A.Elements[i][k] * B.Elements[k][j];
                    }

                    product[i][j] = sum;
                }
            }

            Matrix matrixProduct = new Matrix(product);
            return matrixProduct;
        }
    }
}
