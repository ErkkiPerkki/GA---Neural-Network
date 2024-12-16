using System;

namespace NeuralNetwork
{
    public class Matrix {

        private uint _Rows;
        private uint _Columns;
        private float[][] _Elements;    

        public uint Rows { get { return _Rows; } }
        public uint Columns { get { return _Columns; } }
        public float[][] Elements { get { return _Elements; } }

        public Matrix(float[][] elements) {
            uint rows = (uint)elements.Length;
            uint columns = (uint)elements[0].Length;

            _Rows = rows;
            _Columns = columns;
            _Elements = elements;
        }

        public Matrix(float[] elements) {
            int size = elements.Length;
            float[][] matrixElements = new float[size][];

            for(uint i = 0; i < size; i++) {
                matrixElements[i] = new float[1];
                matrixElements[i][0] = elements[i];
            }

            _Rows = (uint)size;
            _Columns = 1;
            _Elements = matrixElements;
        }

        public Matrix(uint rows, uint columns) {
            float[][] elements = new float[rows][];

            for (uint i = 0; i < rows; i++) {
                elements[i] = new float[columns];

                for (uint j = 0; j < columns; j++) {
                    elements[i][j] = 0;
                }
            }

            _Rows = rows;
            _Columns = columns;
            _Elements = elements;
        }

        public static Matrix operator *(Matrix left, Matrix right) {
            if (left.Columns != right.Rows)
                throw new Exception("Columns of matrix A must match the rows of matrix B");

            float[][] product = new float[left.Rows][];

            for (uint i = 0; i < left.Rows; i++) {
                product[i] = new float[right.Columns];

                for (uint j = 0; j < right.Columns; j++) {
                    float sum = 0;

                    for (uint k = 0; k < left.Columns; k++) {
                        sum += left.Elements[i][k] * right.Elements[k][j];
                    }

                    product[i][j] = sum;
                }
            }

            Matrix matrixProduct = new Matrix(product);
            return matrixProduct;
        }

        public static Matrix operator +(Matrix left, Matrix right) {
            if (left.Columns != right.Columns || right.Rows != left.Rows)
                throw new Exception("Matrix a must be the same size as Matrix b");

            float[][] sum = new float[left.Rows][];

            for (uint i = 0; i < left.Rows; i++) {
                sum[i] = new float[left.Columns];

                for (uint j = 0; j < left.Columns; j++) {
                    sum[i][j] = left.Elements[i][j] + right.Elements[i][j];
                }
            }

            Matrix matrixSum = new Matrix(sum);
            return matrixSum;
        }

        public static Matrix operator -(Matrix left, Matrix right) {
            if (left.Columns != right.Columns || right.Rows != left.Rows)
                throw new Exception("Matrix a must be the same size as Matrix b");

            float[][] sum = new float[left.Rows][];

            for (uint i = 0; i < left.Rows; i++) {
                sum[i] = new float[left.Columns];

                for (uint j = 0; j < left.Columns; j++) {
                    sum[i][j] = left.Elements[i][j] - right.Elements[i][j];
                }
            }

            Matrix matrixSum = new Matrix(sum);
            return matrixSum;
        }

        public override string ToString() {
            string output = "";

            for (uint row = 0; row < Rows; row++) {
                output += "[";

                for (uint column = 0; column < Columns; column++) {
                    float element = Elements[row][column];

                    output += $" {element} ";
                }

                output += "]\n";
            }

            return output;
        }

        public void PopulateWithRandom() {
            for (uint i = 0; i < _Rows; i++) {
                for (uint j = 0; j < _Columns; j++) {
                    _Elements[i][j] = Utility.GetRandomFloat();
                }
            }
        }
    }
}
