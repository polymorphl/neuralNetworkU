using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Neurons {
    public class NewRon : MonoBehaviour {

        NeuralNet net;
        public double high, mid, low;

        public void test() {
            net = new NeuralNet();

            high = .9;
            low = .1;
            mid = .5;

            net.Initialize(1, 2, 2, 1);

            double[][] input = new double[4][];
            input[0] = new double[] { high, high };
            input[1] = new double[] { low, high };
            input[2] = new double[] { high, low };
            input[3] = new double[] { low, low };


            double[][] output = new double[4][];
            output[0] = new double[] { low };
            output[1] = new double[] { high };
            output[2] = new double[] { high };
            output[3] = new double[] { low };

            double ll, lh, hl, hh;
            int count = 0;

            do {
                count++;

                for (int i = 0; i < 100; i++) net.Train(input, output);

                net.ApplyLearning();

                net.InputLayer[0].Output = low;
                net.InputLayer[1].Output = low;
                net.Pulse();
                ll = net.OutputLayer[0].Output;

                net.InputLayer[0].Output = high;
                net.InputLayer[1].Output = low;
                net.Pulse();
                hl = net.OutputLayer[0].Output;

                net.InputLayer[0].Output = low;
                net.InputLayer[1].Output = high;
                net.Pulse();
                lh = net.OutputLayer[0].Output;

                net.InputLayer[0].Output = high;
                net.InputLayer[1].Output = high;
                net.Pulse();
                hh = net.OutputLayer[0].Output;
            } while (hh > mid || lh < mid || hl < mid || ll > mid);

            Debug.Log((count * 100) + " iteraions required for trainn");


        }
    }
}
