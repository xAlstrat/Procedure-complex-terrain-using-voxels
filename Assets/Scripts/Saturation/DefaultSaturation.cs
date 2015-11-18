using UnityEngine;
using System.Collections;

public class DefaultSaturation : Saturation
{
	public float min = -1.0f;
	public float max = 1.0f;

	public DefaultSaturation(){
	}

	public DefaultSaturation(Vector2 v):
		this (v.x, v.y)
	{

	}

	public DefaultSaturation(float min, float max){
		this.min = min;
		this.max = max;
	}

	public override float apply(float value){
		return Mathf.Max (min, Mathf.Min (max, value));
	}

	public void setLimits(Vector2 v){
		this.min = v.x;
		this.max = v.y;
	}
}

