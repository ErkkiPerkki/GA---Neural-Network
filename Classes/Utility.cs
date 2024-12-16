using System;

namespace NeuralNetwork {
    public static class Utility {
        public static Random random = new Random();

        public static float GetRandomFloat() {
            return (float)random.NextDouble();
        }
    }
}
