using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SampleRepository
{
	private static Dictionary<string,float[,,]> repository = new Dictionary<string,float[,,]>();

	public static void registerSample(string key, float[,,] sample){
		repository.Add (key, sample);
	}

	public static float[,,] getSample(string key){
		return repository [key];
	}

}

