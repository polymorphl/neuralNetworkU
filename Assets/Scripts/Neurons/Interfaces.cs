using UnityEngine;
using System.Collections.Generic;

public interface INeuronSignal {
    double Output { get; set; }
}

public interface INeuronReceptor {
    Dictionary<INeuronSignal, NeuralFactor> Input { get; }
}



public class NeuralFactor {

  #region Member Variables
  private double m_weight;
  private double m_delta;
  #endregion

  #region Constructors
  public NeuralFactor(double weight) {
      m_weight = weight;
      m_delta = 0;
  }
  #endregion

  #region Properties
  public double Weight {
      get { return m_weight; }
      set { m_weight = value; }
  }

  public double Delta {
      get { return m_delta; }
      set { m_delta = value; }
  }
  #endregion

  #region Methods
  public void ApplyDelta() {
      m_weight += m_delta;
      m_delta = 0;
  }
  #endregion
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
  INeuralLayer OnputLayer, InputLayer, HiddenLayer;

  void ApplyLearning();
  void Pulse();
}
