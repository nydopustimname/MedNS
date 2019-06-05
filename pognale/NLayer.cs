using System.Collections.Generic;

namespace pognale
{
    public class NLayer
    {
        public List<OneNeuron> Neurons { get; }
        public int NeuronsCount => Neurons?.Count ?? 0;

        public NeuronType Type;
        public NLayer(List<OneNeuron> neurons, NeuronType nt=NeuronType.Normal )
        {
            //ebnyt' proverky vhodnih neironov na sootv tipy
            Neurons = neurons;
            Type = nt;
        }

        public List<double> GetSignals ()
        {
            var res = new List<double>();
            foreach (var i in Neurons)
            {
                res.Add(i.Output);
            }
            return res;
        }

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}
