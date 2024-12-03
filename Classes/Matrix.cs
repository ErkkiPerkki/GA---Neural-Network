﻿using System;
using System.Data;

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

        public static Matrix operator *(Matrix left, Matrix right) {
            if (left.Columns != right.Rows)
                throw new Exception("Columns of matrix A must match the rows of matrix B");

            float[][] product = new float[left.Rows][];

            for (int i = 0; i < left.Rows; i++) {
                product[i] = new float[right.Columns];

                for (int j = 0; j < right.Columns; j++) {
                    float sum = 0;

                    for (int k = 0; k < left.Columns; k++) {
                        sum += left.Elements[i][k] * right.Elements[k][j];
                    }

                    product[i][j] = sum;
                }
            }

            Matrix matrixProduct = new Matrix(product);
            return matrixProduct;
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
    }
}
