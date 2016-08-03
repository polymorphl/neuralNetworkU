using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class Neuron : INeuron {

    NeuralFactor bias;
    double biasWeight;
    double error;
    Dictionary<INeuronSignal, NeuralFactor> input;
    double output;

    public double BiasWeight {
        get {
            return biasWeight;
        }

        set {
            biasWeight = value;
        }
    }

    public double Error {
        get {
            return error;
        }

        set {
            error = value;
        }
    }

    public Dictionary<INeuronSignal, NeuralFactor> Input {
        get {
            return input;
        }

        set {
            input = value;
        }
    }

    public double Output {
        get {
            return output;
        }

        set {
            output = value;
        }
    }

    NeuralFactor INeuron.Bias {
        get {
            return bias;
        }

        set {
            bias = value;
        }
    }

    public Neuron() {
        input = new Dictionary<INeuronSignal, NeuralFactor>();
        bias = new NeuralFactor(0.4);
    }

    //void INeuron.ApplyLearning(INeuralLayer layer, ref double learningRate) {
    //    foreach (KeyValuePair<INeuronSignal, NeuralFactor> m in input)
    //        m.Value.ApplyWeightChange(ref learningRate);

    //    bias.ApplyWeightChange(ref learningRate);
    //}

    void INeuron.ApplyLearning(INeuralLayer layer) {
        //foreach (KeyValuePair<INeuronSignal, NeuralFactor> m in input)
        //    m.Value.ApplyWeightChange(ref learningRate);

        //bias.ApplyWeightChange(ref learningRate);
    }

    public void Pulse(INeuralLayer layer) {
        lock (this) {
            output = 0;

            foreach (KeyValuePair<INeuronSignal, NeuralFactor> item in input)
                output += item.Key.Output * item.Value.Weight;

            output += bias.Weight * BiasWeight;
            output = Sigmoid(output);
        }
    }

    private static double Sigmoid(double value) {
        return 1 / (1 + Math.Exp(-value));
    }
}

public class NeuralLayer : INeuralLayer {

    List<INeuron> neurons;

    public INeuron this[int index] {
        get {
            return neurons[index];
        }

        set {
            neurons[index] = value;
        }
    }

    public int Count {
        get {
            return Neurons.Count;
        }
    }

    public bool IsReadOnly {
        get {
            throw new NotImplementedException();
        }
    }

    public List<INeuron> Neurons {
        get {
            return neurons;
        }

        set {
            neurons = value;
        }
    }

    public void Add(INeuron item) {
        if (Neurons == null) Neurons = new List<INeuron>();
        Neurons.Add(item);
    }

    public void ApplyLearning(INeuralNet net) {
        Neurons.ForEach(n => n.ApplyLearning(this));
    }

    public void Clear() {
        if (Neurons == null) Neurons = new List<INeuron>();
        else Neurons.Clear();
    }

    public bool Contains(INeuron item) {
        if (Neurons == null) return false;
        return Neurons.Contains(item);
    }

    public void CopyTo(INeuron[] array, int arrayIndex) {
        throw new NotImplementedException();
    }

    public IEnumerator<INeuron> GetEnumerator() {
        throw new NotImplementedException();
    }

    public int IndexOf(INeuron item) {
        throw new NotImplementedException();
    }

    public void Insert(int index, INeuron item) {
        throw new NotImplementedException();
    }

    public void Pulse(INeuralNet net) {
        Neurons.ForEach(n => n.Pulse(this));
    }

    public bool Remove(INeuron item) {
        throw new NotImplementedException();
    }

    public void RemoveAt(int index) {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        throw new NotImplementedException();
    }
}

public class NeuralNet : INeuralNet {

    INeuralLayer hiddenLayer, inputLayer, outputLayer;
    double learningRate;

    public INeuralLayer HiddenLayer {
        get {
            return hiddenLayer;
        }

        set {
            hiddenLayer = value;
        }
    }

    public INeuralLayer InputLayer {
        get {
            return inputLayer;
        }

        set {
            inputLayer = value;
        }
    }

    public INeuralLayer OutputLayer {
        get {
            return outputLayer;
        }

        set {
            outputLayer = value;
        }
    }


    public NeuralNet() {
        Debug.Log("Instantiate NeuralNet");
    }

    public void ApplyLearning() {
        lock (this) {
            hiddenLayer.ApplyLearning(this);
            outputLayer.ApplyLearning(this);
        }
    }

    public void Pulse() {
        lock(this) {
            hiddenLayer.Pulse(this);
            outputLayer.Pulse(this);
        }
    }
    

    public void Initialize(int randomSeed, int inputNeuronCount, int hiddenNeuronCount, int outputNeuronCount) {
        Debug.Log("Start NeuralSet Initializae");
        int layerCount, i, j, k;
        System.Random rand;
        INeuralLayer layer;

        rand = new System.Random(randomSeed);

        inputLayer = new NeuralLayer();
        hiddenLayer = new NeuralLayer();
        outputLayer = new NeuralLayer();

        for (i = 0; i < inputNeuronCount; i++) inputLayer.Add(new Neuron());
        for (i = 0; i < hiddenNeuronCount; i++) hiddenLayer.Add(new Neuron());
        for (i = 0; i < outputNeuronCount; i++) outputLayer.Add(new Neuron());

        for (i = 0; i < hiddenLayer.Count; i++)
            for (j = 0; j < inputLayer.Count; j++)
                hiddenLayer[i].Input.Add(inputLayer[j],
                     new NeuralFactor(rand.NextDouble()));

        for (i = 0; i < outputLayer.Count; i++)
            for (j = 0; j < hiddenLayer.Count; j++)
                outputLayer[i].Input.Add(HiddenLayer[j],
                     new NeuralFactor(rand.NextDouble()));
    }
    
    private void BackPropagation(double[] desiredResults) {
        int i, j;
        double temp, error;
        INeuron outputNode, inputNode, hiddenNode, node, node2;

        // Calcualte output error values 
        for (i = 0; i < outputLayer.Count; i++) {
            temp = outputLayer[i].Output;
            outputLayer[i].Error = (desiredResults[i] - temp) * temp * (1.0F - temp);
        }

        // calculate hidden layer error values
        for (i = 0; i < hiddenLayer.Count; i++) {
            node = hiddenLayer[i];
            error = 0;

            for(j = 0; j < outputLayer.Count; j++) {
                outputNode = outputLayer[j];
                error += outputNode.Error * outputNode.Input[node].Weight * node.Output * (1.0 - node.Output);
            }

            node.Error = error;
        }

        // adjust output layer weight change
        for (i = 0; i < hiddenLayer.Count; i++) {
            node = hiddenLayer[i];

            for(j = 0; j < outputLayer.Count; j++) {
                outputNode = outputLayer[j];
                outputNode.Input[node].Weight += learningRate * outputLayer[j].Error * node.Output;
                outputNode.Bias.Delta += learningRate * outputLayer[j].Error * outputNode.Bias.Weight;
            }
        }

        // adjust hidden layer weight change
        for(i = 0; i < inputLayer.Count; i++) {
            inputNode = inputLayer[i];

            for(j = 0; j < hiddenLayer.Count; j++) {
                hiddenNode = hiddenLayer[j];
                hiddenNode.Input[inputNode].Weight += learningRate * hiddenNode.Error * inputNode.Output;
                hiddenNode.Bias.Delta += learningRate * hiddenNode.Error * inputNode.Bias.Weight;
            }
        }
    }

    public void Train(double[] input, double[] desiredResult) {
        int i;

        if (input.Length != inputLayer.Count)
            throw new ArgumentException(string.Format("Expecting {0} inputs for this net", inputLayer.Count));

        for(i = 0; i < inputLayer.Count; i++) {
            Neuron n = inputLayer[i] as Neuron;

            if (n != null) n.Output = input[i];
        }

        Pulse();
        BackPropagation(desiredResult);
    }

    public void Train(double[][] inputs, double[][] expected) {
        for (int i = 0; i < inputs.Length; i++) Train(inputs[i], expected[i]);
    }
}