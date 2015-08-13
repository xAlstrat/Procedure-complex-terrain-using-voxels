using UnityEngine;
using System.Collections;

/// <summary>
/// This class sets the configuration for a given sample for its use building the terrain. 
/// </summary>
public class NoiseSample
{
	/// <summary>
	/// Weight of this noise in the final terrain.
	/// </summary>
	/// <value>The weight.</value>
	public float weight{ get; private set;}

	/// <summary>
	/// 3D sample of smooth moise.
	/// </summary>
	/// <value>The sample.</value>
	public float[,,] sample{ get; private set;}

	/// <summary>
	/// The sample scale.
	/// Biggers values means an smaller sample.
	/// </summary>
	public Vector3 sampleScale;

	/// <summary>
	/// The saturation applied to this sample.
	/// </summary>
	public Saturation saturation;
	
	public Vector3 rotation = Vector3.zero;
	public Vector3 pivot = Vector3.zero;

	public NoiseSample(float[,,] sample, float weight, Saturation saturation, Vector3 sampleScale){
		setSample (sample);
		setSaturation (saturation);
		setScale (sampleScale);
		setWeight (weight);
	}

	public void rotate(Vector3 rotation){
		this.rotation = rotation;
	}

	public void setScale(Vector3 sampleScale){
		this.sampleScale = sampleScale;
	}

	public void setSaturation(Saturation saturation){
		this.saturation = saturation;
	}

	public void setSample(float[,,] sample){
		this.sample = sample;
	}

	public void setWeight(float weight){
		this.weight = weight;
	}

}

