using System.Collections.Generic;

namespace pognale
{
    public class Topology
    {
        public int InputCount { get; }
        public int OutputCount { get; }
        public double LearningRate { get; }
        public List<int> HiddenLayers { get; }
        public Topology (int inpCount, int outCount,double learningRate, params int [] layers)
        {
            InputCount = inpCount;
            OutputCount = outCount;
            LearningRate = learningRate;
            HiddenLayers = new List<int>();
            HiddenLayers.AddRange(layers);
        }
    }
}
