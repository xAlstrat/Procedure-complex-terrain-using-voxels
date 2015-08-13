using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoiseSampleManager
{
	private List<NoiseSample> samples = new List<NoiseSample>();

	private SampleInterpolator interpolator;
	
	private Vector3 sampleScale;
	private float noiseScale;


	public NoiseSampleManager(float noiseScale){
		this.interpolator = new SampleInterpolator ();
		this.noiseScale = noiseScale;
	}

	public void addSample(NoiseSample sample){
		samples.Add (sample);
	}

	public float getSampleAt(float x, float y, float z){
		float totalWeight = 0;
		float result = 0;
		float noise;
		for (int c = 0; c < samples.Count; c++) {
			sampleScale = samples[c].sampleScale;
			noise = interpolator.interpolateAt (samples[c], x*sampleScale.x, y*sampleScale.y, z*sampleScale.z);
			result += samples[c].saturation.apply(noise) * samples[c].weight;
			totalWeight += Mathf.Abs(samples[c].weight);
		}
		return noiseScale * result / totalWeight;
	}


}

