using UnityEngine;
using System.Collections;
using UnityEditor;

public class NoiseSampleWindow : EditorWindow {

	private Vector3 scale = new Vector3 (1f, 1f, 1f);
	public float weight = 1f;
	public Saturation saturation = null;
	public PersistentSample3D sample = null;

	private string name = "NoisedSample";
	
	[MenuItem("Procedure Terrain/Create Asset/Noised Sample")]
	private static void noisedSampleWindow()
	{
		EditorWindow.GetWindow<NoiseSampleWindow>(true, "Noised Sample Asset Creator");
	}
	
	void OnGUI(){
		name = EditorGUILayout.TextField ("Asset Name: ", name);
		scale = EditorGUILayout.Vector3Field ("Scale", scale);
		weight = EditorGUILayout.FloatField ("Weight", weight);
		saturation = EditorGUILayout.ObjectField ("Saturation:", saturation, typeof(Saturation), false) as Saturation;
		sample = EditorGUILayout.ObjectField ("Sample3D:", sample, typeof(PersistentSample3D), false) as PersistentSample3D;
		
		EditorGUILayout.BeginHorizontal ();
		{
			if (GUILayout.Button ("Accept")) {
				createNoiseAsset();
				this.Close ();
				EditorUtility.DisplayDialog ("Alert",  "Prefab "+name+".prefab has been created successfully.", "Accept");
			}
			if (GUILayout.Button ("Cancel")) {
				cancel();
			}
		}
		
	}
	
	private void createNoiseAsset(){		
		NoiseSample noiseSample = ScriptableObject.CreateInstance<NoiseSample> ();
		noiseSample.setScale (scale);
		noiseSample.setSaturation (saturation);
		noiseSample.setWeight (weight);
		noiseSample.setPersistentSample (sample);
		checkPath ();
		AssetDatabase.CreateAsset (noiseSample, "Assets/ProcedureTerrain/NoiseSample/"+name+".asset");
		
		//EditorUtility.SetDirty(sample3D);
		
		AssetDatabase.SaveAssets ();
	}
	
	private void cancel(){
		this.Close ();
	}

	private void checkPath(){
		if(!AssetDatabase.IsValidFolder("Assets/ProcedureTerrain")){
			AssetDatabase.CreateFolder("Assets", "ProcedureTerrain");
		}
		if(!AssetDatabase.IsValidFolder("Assets/ProcedureTerrain/NoiseSample")){
			AssetDatabase.CreateFolder("Assets/ProcedureTerrain", "NoiseSample");
		}
	}
}
