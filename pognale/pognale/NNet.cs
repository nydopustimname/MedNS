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

        public OneNeuron FeedForward(List<double> inpSignals)
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

        private void SendSignalsToInpNeurons(List<double> inpSignals)
        {
            for (var i = 0; i < inpSignals.Count; i++)
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
                var neuron = new OneNeuron(lastLayer.Count, NeuronType.Output);
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
                    var neuron = new OneNeuron(lastLayer.Count);
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
    }
}
