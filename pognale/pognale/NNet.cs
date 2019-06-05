using System;
using System.Collections.Generic;
using System.Linq;

namespace pognale
{
    public class NNet
    {
        public Topology Topology { get; }
        public List<NLayer> Layers { get; }
        public NNet(Topology top)
        {
            Topology = top;
            Layers = new List<NLayer>();
            CreateInpLayer();
            CreateHidLayers();
            CreateOutLayer();
        }

        public OneNeuron FeedForward(params double[] inpSignals)
        {
            //proverka
            SendSignalsToInpNeurons(inpSignals);
            FeedForwardAllLayers();
            return Topology.OutputCount == 1 ? Layers.Last().Neurons[0] : Layers.Last().Neurons.OrderByDescending(n => n.Output).First();
        }

        private void FeedForwardAllLayers()
        {
            for (var i = 1; i < Layers.Count; i++)
            {
                var layer = Layers[i];
                var previousLayerSign = Layers[i - 1].GetSignals();
                foreach (var n in layer.Neurons)
                {
                    n.FeedForward(previousLayerSign);
                }
            }
        }

        private void SendSignalsToInpNeurons(params double[] inpSignals)
        {
            for (var i = 0; i < inpSignals.Length; i++)
            {
                var signal = new List<double>() { inpSignals[i] };
                var neuron = Layers[0].Neurons[i];
                neuron.FeedForward(signal);
            }
        }

        private void CreateOutLayer()
        {
            var outputNeurons = new List<OneNeuron>();
            var lastLayer = Layers.Last();
            for (var i = 0; i < Topology.OutputCount; i++)
            {
                var neuron = new OneNeuron(lastLayer.NeuronsCount, NeuronType.Output);
                outputNeurons.Add(neuron);
            }
            var outpLayer = new NLayer(outputNeurons, NeuronType.Output);
            Layers.Add(outpLayer);
        }

        private void CreateHidLayers()
        {
            for (var j = 0; j < Topology.HiddenLayers.Count; j++)
            {
                var hiddenNeurons = new List<OneNeuron>();
                var lastLayer = Layers.Last();
                for (var i = 0; i < Topology.HiddenLayers[j]; i++)
                {
                    var neuron = new OneNeuron(lastLayer.NeuronsCount);
                    hiddenNeurons.Add(neuron);
                }
                var hiddenLayer = new NLayer(hiddenNeurons);
                Layers.Add(hiddenLayer);
            }
        }

        private void CreateInpLayer()
        {
            var inputNeurons = new List<OneNeuron>();
            for (var i=0; i<Topology.InputCount; i++)
            {
                var neuron = new OneNeuron(1, NeuronType.Input);
                inputNeurons.Add(neuron);
            }
            var inpLayer = new NLayer(inputNeurons, NeuronType.Input);
            Layers.Add(inpLayer);
        }

        private double BackPropagation (double expected, params double [] inputs)
        {
            var actual = FeedForward(inputs).Output;
            var dif = actual - expected;
            foreach (var i in Layers.Last().Neurons)
            {
                i.Learn(dif, Topology.LearningRate);
            }
            for (var i=Layers.Count-2; i>=0; i--)
            {
                var layer = Layers[i];
                var prevLayer = Layers[i + 1];
                for (var j=0; j<layer.NeuronsCount; j++)
                {
                    var neuron = layer.Neurons[j];
                    for (var k=0; k<prevLayer.NeuronsCount; k++)
                    {
                        var prevNeuron = prevLayer.Neurons[k];
                        var error = prevNeuron.Weights[j] * prevNeuron.Delta;
                        neuron.Learn(error, Topology.LearningRate);
                    }
                }
            }
            return dif * dif;
        }

        public double Learn (List<Tuple<double, double[]>> dataset, int epoch)
        {
            var error = 0.0;
            for (var i=0; i<epoch; i++)
            {
                foreach (var j in dataset)
                {
                    error+= BackPropagation(j.Item1, j.Item2);
                }
            }
            var res = error / epoch;
            return res;
        }
    }
}
