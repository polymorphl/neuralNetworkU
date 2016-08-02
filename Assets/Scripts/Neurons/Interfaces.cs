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

  private double m_weight;
  private double m_delta;

  public NeuralFactor(double weight) {
      m_weight = weight;
      m_delta = 0;
  }

  public double Weight {
      get { return m_weight; }
      set { m_weight = value; }
  }

  public double Delta {
      get { return m_delta; }
      set { m_delta = value; }
  }

  public void ApplyDelta() {
      m_weight += m_delta;
      m_delta = 0;
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
