using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// This class allows to use an 3D array as an Unity object and save it in
/// a prefab to preserv the sample.
/// </summary>
[Serializable]
public class PersistentSample3D : ScriptableObject
{
	public float[] sample;
	public string sampleName = "Sample3D";
	public int x_size;
	public int y_size;
	public int z_size;

	public void setName(string name){
		sampleName = name;
	}

	public string getName(){
		return sampleName;
	}

	public void setSample(float[,,] _sample){
		x_size = _sample.GetLength (0);
		y_size = _sample.GetLength (1);
		z_size = _sample.GetLength (2);
		sample = new float[x_size * y_size * z_size];

		int c1, c2;
		for (int k = 0; k < z_size; k++) {
			c1 = x_size*y_size*k;
			for (int j = 0; j < y_size; j++) {
				c2 = x_size*j;
				for (int i = 0; i < x_size; i++) {
					sample[i + c2 + c1] = _sample[i, j, k];
				}
			}
		}
	}

	public float[,,] getSample(){
		float [,,] sample_3d = new float[x_size, y_size, z_size];
		int c1, c2;
		for (int k = 0; k < z_size; k++) {
			c1 = x_size*y_size*k;
			for (int j = 0; j < y_size; j++) {
				c2 = x_size*j;
				for (int i = 0; i < x_size; i++) {
					sample_3d[i, j, k] = sample[i + c2 + c1];
				}
			}
		}
		return sample_3d;
	}
}

