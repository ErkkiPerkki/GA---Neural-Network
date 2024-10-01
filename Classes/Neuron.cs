namespace NeuralNetwork
{
    public class Neuron
    {
        private float _Value;

        public float Value
        {
            get { return _Value; }
        }

        public Neuron()
        {
            _Value = (float)Program.random.NextDouble();
        }
    }
}
