using UnityEngine;
using System.Collections;

public class NoiseVertexInfo
{
	public readonly float value;
	public readonly Vector3 normal;

	public NoiseVertexInfo(float value, Vector3 normal){
		this.value = value;
		this.normal = normal;
	}

}

