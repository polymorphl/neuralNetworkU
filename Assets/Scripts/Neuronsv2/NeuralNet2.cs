using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;
using MathNet.Numerics.LinearAlgebra.Double;
using UnityEditor;

public class NeuralNet2 : MonoBehaviour {

    int classCount;
    public TextAsset test, training, validation;


	void Start () {
        //Matrix<double> matrix =
        Debug.Log(Application.dataPath + AssetDatabase.GetAssetPath(test));
        DataSet.BuildFromFile(Application.dataPath + AssetDatabase.GetAssetPath(test).Replace("Assets", ""));

        //Debug.Log(matrix);


        classCount = 5;

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
        Matrix<double> matrix = Matrix<double>.Build.Dense(1, classCount);

        for (int i = 0; i < matrix.ColumnCount; i++) {
            if (matrix[0, i] == classNum - 1) matrix[0, i] = 1;
            else matrix[0, i] = 0;
        }

        return matrix;
    }

    public class DataSet {
        int inputCount, outputCount;

        public static DataSet BuildFromFile(string filePath) {
            DataSet dataSet = new DataSet();

            StreamReader reader = new StreamReader(filePath);

            string input = File.ReadAllText(filePath);
            List<List<double>> list = new List<List<double>>();

            int i = 0;
            foreach (var row in input.Split('\n')) {
                list.Add(new List<double>());
                foreach (var col in row.Trim().Split('\t')) {
                    Debug.Log(double.Parse(col.Trim()));
                    list[i].Add(double.Parse(col.Trim()));
                }
            }


            double[][] result = list.Select(a => a.ToArray()).ToArray();


            Matrix<double> matrix = Matrix<double>.Build.DenseOfColumnArrays(result);

            Debug.Log(matrix);

            return dataSet;
        }
    }
    
    class DataSubSet {

        double[] inputs, outputs;
        //classes
        int count;
        int bias;
    }
}
