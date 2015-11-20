using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoiseSampleManager
{
	private List<NoiseSample> samples = new List<NoiseSample>();

	private DefaultInterpolator interpolator;
	
	private Vector3 sampleScale;
	private float groundLevel = 0;


	public NoiseSampleManager(float groundLevel){
		this.groundLevel = groundLevel;
		this.interpolator = new DefaultInterpolator ();
	}

	public void addSample(NoiseSample sample){
		samples.Add (sample);
	}

	public float getSampleAt(float x, float y, float z){
		float result = 0;
		float noise;
		float tx, ty, tz;
		bool noiseApplied = false;
		for (int c = 0; c < samples.Count; c++) {
			tx = x + samples [c].traslation.x;
			ty = y + samples [c].traslation.y;
			tz = z + samples [c].traslation.z;
			if(samples[c].filterAt(tx, ty, tz, groundLevel, noiseApplied))
				continue;
			sampleScale = samples[c].sampleScale;
			noise = interpolator.interpolateAt (samples[c], tx*sampleScale.x, ty*sampleScale.y, tz*sampleScale.z);
			result += samples[c].saturation.apply(noise) * samples[c].noiseScale;
			if(result != 0 && !noiseApplied)
				noiseApplied = true;
			//totalWeight += Mathf.Abs(samples[c].weight);
		}
		return result;
	}


}

