using System.Collections.Generic;

namespace pognale
{
    public class Topology
    {
        public int InputCount { get; }
        public int OutputCount { get; }
        public List<int> HiddenLayers { get; }
        public Topology (int inpCount, int outCount, params int [] layers)
        {
            InputCount = inpCount;
            OutputCount = outCount;
            HiddenLayers = new List<int>();
            HiddenLayers.AddRange(layers);
        }
    }
}
