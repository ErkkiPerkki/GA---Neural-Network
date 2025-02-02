using System.Text.Json;
using System.Text.Json.Serialization;

namespace NeuralNetwork {
    public class NetworkData {
        public float[][][] Weights { get; set; }
        public float[][][] Biases { get; set; }

        public NetworkData(Network network) {
            Weights = new float[network.NetworkSize][][];
            Biases = new float[network.NetworkSize][][];

            // Loop through all hidden layers and save their data to the corresponding data array
            for (int layerIndex = 0; layerIndex < network.NetworkSize; layerIndex++) {
                Layer layer = network.Layers[layerIndex];

                Biases[layerIndex] = layer.Biases.Elements;

                if (layerIndex < network.NetworkSize - 1)
                    Weights[layerIndex] = layer.Weights.Elements;
            }
        }

        [JsonConstructor]
        public NetworkData(float[][][] weights, float[][][] biases) {
            Weights = weights;
            Biases = biases;
        }

        public string ToJSON() {
            JsonSerializerOptions options = new JsonSerializerOptions {
                WriteIndented = true,
                IncludeFields = true
            };
            return JsonSerializer.Serialize(this, options);
        }
    }
}