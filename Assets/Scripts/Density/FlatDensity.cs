using UnityEngine;
using System.Collections;

public class FlatDensity : Density
{
	private float height;
	public FlatDensity(float height){
		this.height = height;
	}
	public virtual float apply(float x, float y, float z){
		return -y+height;
	}
	
	public virtual float apply(Vector3 position){
		return -position.y + height;
	}
}

