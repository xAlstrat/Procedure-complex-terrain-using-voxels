using UnityEngine;
using System.Collections;

public class DefaultSaturation : Saturation
{
	private float min = -1.0f;
	private float max = 1.0f;

	public DefaultSaturation(){
	}

	public DefaultSaturation(float min, float max){
		this.min = min;
		this.max = max;
	}

	public float apply(float value){
		return Mathf.Max (min, Mathf.Min (max, value));
	}
}

