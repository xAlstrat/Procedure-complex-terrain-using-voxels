using UnityEngine;
using System.Collections;

/// <summary>
/// This class sets the configuration for a given sample for its use building the terrain. 
/// </summary>
public class NoiseSample : ScriptableObject
{
	/// <summary>
	/// 3D sample of smooth moise.
	/// </summary>
	/// <value>The sample.</value>
	public PersistentSample3D sample;

	/// <summary>
	/// The saturation applied to this sample.
	/// </summary>
	public Saturation saturation;

	/// <summary>
	/// The sample scale.
	/// Biggers values means an smaller sample.
	/// </summary>
	public Vector3 sampleScale;

	/// <summary>
	/// Scale of this noise in the final terrain.
	/// </summary>
	/// <value>The weight.</value>
	public float noiseScale;

	/// <summary>
	/// The filters this noise sample will use;
	/// </summary>
	public NoiseFilter[] filters;
	
	[Header("   Sample Transformation")]
	public Vector3 traslation = Vector3.zero;
	public Vector3 rotation = Vector3.zero;
	public Vector3 pivot = Vector3.zero;

	public void InitNoise(float weight, Saturation saturation, Vector3 sampleScale){
		setSaturation (saturation);
		setScale (sampleScale);
		setNoiseScale (weight);
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

	public void setPersistentSample(PersistentSample3D sample){
		this.sample = sample;
	}

	public PersistentSample3D getPersistentSample(){
		return sample;
	}

	public float[,,] getSample(){
		return SampleRepository.getSample (sample.getName());
	}

	public void setNoiseScale(float scale){
		this.noiseScale = scale;
	}

	public bool filterAt(float x, float y,  float z, float h, bool noiseApplied){
		foreach (NoiseFilter filter in filters) {
			if ((filter.uniqueNoiseRequired () && noiseApplied) || filter.filterAt (x, y, z, h))
				return true;
		}
		return false;
	}

}

