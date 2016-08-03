using UnityEngine;
using System.Collections;
//using Extreme.Mathematics;
using System;
//using Extreme
using MathNet.Numerics.LinearAlgebra;
//using Mono;

public class NeuralNet2 : MonoBehaviour {

	void Start () {
        Matrix<double> matrix = Matrix<double>.Build.Random(1, 5);

        Debug.Log(matrix);
	}
	
	void Update () {
	
	}


    int OutputToClass(Matrix<double> matrix) {
        for(int i = 0; i < matrix.ColumnCount; i++) {
            if (matrix[0, i] == 1) return i;
        }
        return -1;        
    }

    Matrix<double> ClassToMatrix(int classNum) {
        return null;
    }
}
