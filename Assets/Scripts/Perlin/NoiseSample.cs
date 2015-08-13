using UnityEngine;
using System.Collections;

public class NoiseSample
{
	public float weight{ get; private set;}
	public float[,,] sample{ get; private set;}
	public Vector3 sampleScale;
	public Saturation saturation;

	public NoiseSample(float[,,] sample, float weight, Saturation saturation, Vector3 sampleScale){
		this.weight = weight;
		this.sample = sample;
		this.saturation = saturation;
		this.sampleScale = sampleScale;
	}

}

