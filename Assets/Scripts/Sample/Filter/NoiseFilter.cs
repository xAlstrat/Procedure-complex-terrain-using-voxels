using UnityEngine;
using System.Collections;

public abstract class NoiseFilter : ScriptableObject
{

	public bool filterAt(Vector3 v, float h){
		return filterAt (v.x, v.y, v.z, h);
	}

	public abstract bool filterAt (float x, float y, float z, float h);

	public virtual bool uniqueNoiseRequired(){
		return false;
	}

}

