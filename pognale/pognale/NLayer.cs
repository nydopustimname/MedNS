using System.Collections.Generic;

namespace pognale
{
    public class NLayer
    {
        public List<OneNeuron> Neurons { get; }
        public int Count => Neurons?.Count ?? 0;
        public NLayer(List<OneNeuron> neurons, NeuronType nt=NeuronType.Normal )
        {
            //ebnyt' proverky vhodnih neironov na sootv tipy
            Neurons = neurons;
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
    }
}
