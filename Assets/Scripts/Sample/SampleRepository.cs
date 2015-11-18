using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SampleRepository
{
	private static Dictionary<string, float[,,]> repository = new Dictionary<string, float[,,]>();

	public static void registerSample(PersistentSample3D sample){
		if (repository.ContainsKey (sample.getName ()))
			return;
		repository.Add (sample.getName(), sample.getSample());
	}

	public static void registerSample(string key, PersistentSample3D sample){
		if (repository.ContainsKey (key))
			return;
		repository.Add (key, sample.getSample());
	}

	public static float[,,] getSample(string key){
		return repository [key];
	}

}

