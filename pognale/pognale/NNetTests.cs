using Microsoft.VisualStudio.TestTools.UnitTesting;
using pognale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pognale.Tests
{
    [TestClass()]
    public class NNetTests
    {
        /// <summary>
        /// 
        /// 1=sick
        /// 0=healthy
        /// 
        /// T=wrong temperatyre;
        /// A=young age;
        /// S=smoke;
        /// F=healthy food
        /// 
        /// </summary>

        [TestMethod()]
        public void FeedForwardTest()
        {
            var dataset=new List<Tuple<double, double[]>>
            { 
               //                                         T  A  S  F      
            new Tuple<double, double[]>(0, new double[] { 0, 0, 0, 0 }),
            new Tuple<double, double[]>(0, new double[] { 0, 0, 0, 1 }),
            new Tuple<double, double[]>(1, new double[] { 0, 0, 1, 0 }),
            new Tuple<double, double[]>(0, new double[] { 0, 0, 1, 1 }),
            new Tuple<double, double[]>(0, new double[] { 0, 1, 0, 0 }),
            new Tuple<double, double[]>(0, new double[] { 0, 1, 0, 1 }),
            new Tuple<double, double[]>(1, new double[] { 0, 1, 1, 0 }),
            new Tuple<double, double[]>(0, new double[] { 0, 1, 1, 1 }),
            new Tuple<double, double[]>(1, new double[] { 1, 0, 0, 0 }),
            new Tuple<double, double[]>(1, new double[] { 1, 0, 0, 1 }),
            new Tuple<double, double[]>(1, new double[] { 1, 0, 1, 0 }),
            new Tuple<double, double[]>(1, new double[] { 1, 0, 1, 1 }),
            new Tuple<double, double[]>(1, new double[] { 1, 1, 0, 0 }),
            new Tuple<double, double[]>(0, new double[] { 1, 1, 0, 1 }),
            new Tuple<double, double[]>(1, new double[] { 1, 1, 1, 0 }),
            new Tuple<double, double[]>(1, new double[] { 1, 1, 1, 1 }),
            };


            var topology = new Topology(4, 1, 0.1, 2);
            var neuralNet = new NNet(topology);
            var dif= neuralNet.Learn(dataset, 100000);
            var res = new List<double>();

            foreach (var data in dataset)
            {
                 res.Add( neuralNet.FeedForward(data.Item2).Output);
            }

            for (var i=0; i<res.Count; i++)
            {
                var expected = Math.Round(dataset[i].Item1, 4);
                var actual = Math.Round(res[i], 3);
                Assert.AreEqual(expected, actual);
            }

        }
    }
}