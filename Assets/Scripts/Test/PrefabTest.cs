using UnityEngine;
using System.Collections;

public class PrefabTest : MonoBehaviour
{
	public PersistentSample3D data;
	// Use this for initialization
	void Start ()
	{
		Debug.Log( "in 000: "+data.getSample()[0,0,0]);
	}
}

