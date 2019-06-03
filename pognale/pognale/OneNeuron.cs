using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pognale
{
    public class OneNeuron
    {
        public List<double> Weights { get;  }
        public NeuronType NType { get; }
        public double Output { get; private set; }
        public OneNeuron(int inpCount, NeuronType nt=NeuronType.Normal)
        {
            NType = nt;
            Weights = new List<double>();
            for (var i=0; i<inpCount; i++)
            {
                Weights.Add(1);
            }
        }

        public double FeedForward (List<double> inputs)
        {
            //ebnyt' proverky
            var sum = 0.0;
            for (var i=0; i<inputs.Count; i++)
            {
                sum += inputs[i] * Weights[i];
            }
          
            return Output = NType != NeuronType.Input ? ActivationFunc(sum) : sum; ;
        }

        private double ActivationFunc (double x)
        {
            return 1.0 / (1.0 + Math.Pow(Math.E, -x));
        }

        public override string ToString()
        {
            return Output.ToString(); 
        }

        public void SetWeights (params double [] weights)
        {
            //delete after add study
            for (var i=0; i<weights.Length; i++)
            {
                Weights[i] = weights[i];
            }
        }


    }
}
