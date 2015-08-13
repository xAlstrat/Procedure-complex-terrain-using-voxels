using UnityEngine;
using System.Collections;

public class NoisedFlatDensity : FlatDensity
{
	private NoiseSampleManager sampleManager;

	public NoisedFlatDensity(float height, NoiseSampleManager sampleManager)
	: base(height){
		this.sampleManager = sampleManager;
	}
	public override float apply(float x, float y, float z){

		return base.apply(x, y, z)+sampleManager.getSampleAt(x, y, z);
	}
	
	public override float apply(Vector3 position){
		return apply (position.x, position.y, position.z);
	}

}

