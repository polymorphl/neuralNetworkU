using UnityEngine;
using System.Collections.Generic;

//http://www.c-sharpcorner.com/article/C-Sharp-artificial-intelligence-ai-programming-a-basic-object/

public interface INeuronSignal {
    double Output { get; set; }
}

public interface INeuronReceptor {
    Dictionary<INeuronSignal, NeuralFactor> Input { get; }
}



public class NeuralFactor {

    private double weight;
    private double delta, lastDelta;

    public NeuralFactor(double weight) {
        weight = weight;
        delta = 0;
    }

    public double Weight {
    get { return weight; }
    set { weight = value; }
    }

    public double Delta {
    get { return delta; }
    set { delta = value; }
    }

    public void ApplyDelta() {
    weight += delta;
    delta = 0;
    }

    public void ApplyWeightChange(ref double learningRate) {
        lastDelta = delta;
        weight += delta * learningRate;
    }

    public void ResetWeightChange() {
        lastDelta = delta = 0;
    }
}

public interface INeuron : INeuronSignal, INeuronReceptor {

    void Pulse(INeuralLayer layer);
    void ApplyLearning(INeuralLayer layer);

    NeuralFactor Bias { get; set; }
    double BiasWeight { get; set; }
    double Error { get; set; }
}

public interface INeuralLayer : IList<INeuron> {

    void Pulse(INeuralNet net);
    void ApplyLearning(INeuralNet net);
}

public interface INeuralNet {
    INeuralLayer OutputLayer { get; set; }
    INeuralLayer InputLayer { get; set; }
    INeuralLayer HiddenLayer { get; set; }

  void ApplyLearning();
  void Pulse();
}
