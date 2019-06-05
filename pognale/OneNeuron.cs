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
        public List<double> Inputs { get; }
        public double Delta { get; private set; }
        public NeuronType NType { get; }
        public double Output { get; private set; }
        public OneNeuron(int inpCount, NeuronType nt=NeuronType.Normal)
        {
            NType = nt;
            Inputs = new List<double>();
            Weights = new List<double>();
            InitWeights(inpCount);
        }

        private void InitWeights(int inpCount)
        {
            var rand = new Random();
            for (var i = 0; i < inpCount; i++)
            {
                if (NType == NeuronType.Input)
                    Weights.Add(1);
                else
                Weights.Add(rand.NextDouble());
                Inputs.Add(0);
            }
        }

        public double FeedForward (List<double> inputs)
        {
            //ebnyt' proverky
            for (var i=0; i<inputs.Count; i++)
            {
                Inputs[i] = inputs[i];
            }
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

        private double Dx (double x)
        {
           var sigmx= ActivationFunc(x);
            return sigmx / (1 - sigmx);
        }

        public override string ToString()
        {
            return Output.ToString(); 
        }

        public void Learn(double error, double learnRate)
        {
            if (NType==NeuronType.Input)
            {
                return;
            }
            Delta = error * Dx(Output);
            for (var i=0; i<Weights.Count; i++)
            {
                var weight = Weights[i];
                var inp = Inputs[i];
                var newWeight = weight - inp * Delta * learnRate;
                Weights[i] = newWeight;
            }
        }
    }
}
