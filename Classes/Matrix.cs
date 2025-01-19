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

        public static Matrix operator *(Matrix left, float right) {
            float[][] result = new float[left.Rows][];

            for (uint i = 0; i < left.Rows; i++) {
                result[i] = new float[left.Columns];

                for (uint j = 0; j < left.Columns; j++) {
                    result[i][j] = left.Elements[i][j] * right;
                }
            }

            return new Matrix(result);
        }

        public Matrix ElementWiseMultiplication(Matrix right) {
            if (_Columns != right.Columns)
                throw new Exception("Matrix A and B must be the same size");

            float[][] result = new float[_Rows][];

            for (uint row = 0; row < _Rows; row++) {
                result[row] = new float[_Columns];

                for (uint column = 0; column < _Columns; column++) {
                    result[row][column] = _Elements[row][column] * right._Elements[row][column];
                }
            }

            return new Matrix(result);
        }

        public Matrix OuterProduct(Matrix right) {
            if (_Columns != 1 || right._Columns != 1)
                throw new Exception("Matrix A and B must be column vectors");

            float[][] result = new float[_Rows][];
                
            for (uint m = 0; m < _Rows; m++) {
                result[m] = new float[right._Rows];

                for (uint n = 0; n < right._Rows; n++) {
                    result[m][n] = _Elements[m][0] * right._Elements[n][0];
                }
            }

            return new Matrix(result);
        }

        public Matrix Transpose() {
            float[][] result = new float[_Columns][];

            for (uint column = 0; column < _Columns; column++) {
                result[column] = new float[_Rows];

                for (uint row = 0; row < _Rows; row++) {
                    result[column][row] = _Elements[row][column];
                }
            }

            return new Matrix(result);
        }

        public override string ToString() {
            string output = "";

            for (uint row = 0; row < _Rows; row++) {
                output += "[";

                for (uint column = 0; column < _Columns; column++) {
                    float element = _Elements[row][column];

                    output += $" {element.ToString("0.00")} ";
                }

                output += "]\n";
            }

            return output;
        }

        public void PopulateWithRandom(float min = 0, float max = 1) {
            float diff = max - min;

            for (uint i = 0; i < _Rows; i++) {
                for (uint j = 0; j < _Columns; j++) {
                    _Elements[i][j] = min + (Utility.GetRandomFloat() * diff);
                }
            }
        }
    }
}
