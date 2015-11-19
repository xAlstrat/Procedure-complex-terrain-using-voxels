using UnityEngine;
using System.Collections;
using UnityEditor;

public class SamplePrefabCreator : EditorWindow {
	private int width = 50;
	private int height = 50;
	private int depth = 50;
	private int octave = 1;
	private string name = "Sample3D";

	[MenuItem("Procedure Terrain/Create Sample/Perlin Sample3D")]
	private static void perlinSampleWindow()
	{
		EditorWindow.GetWindow<SamplePrefabCreator>(true, "3D Sample Asset Creator");
	}

	public static void Store(string prefabName, float[,,] sample){

	}

	void OnGUI(){
		name = EditorGUILayout.TextField ("Prefab Name: ", name);
		width = EditorGUILayout.IntField ("Sample Width: ", width);
		height = EditorGUILayout.IntField ("Sample Height: ", height);
		depth = EditorGUILayout.IntField ("Sample Depth: ", depth);
		octave = EditorGUILayout.IntField ("Sample Octave: ", octave);
		EditorGUILayout.BeginHorizontal ();
		{
			if (GUILayout.Button ("Accept")) {
				createPerlinPrefab();
				this.Close ();
				EditorUtility.DisplayDialog ("Alert",  "Prefab "+name+".prefab has been created successfully.", "Accept");
			}
			if (GUILayout.Button ("Cancel")) {
				cancel();
			}
		}

	}

	private void createPerlinPrefab(){
		/*GameObject instance = getGameObject ();
		Object prefab = EditorUtility.CreateEmptyPrefab("Assets/Resources/Prefab/Sample3D/"+name+".prefab");
		EditorUtility.ReplacePrefab(instance, prefab, ReplacePrefabOptions.ConnectToPrefab);
		GameObject.DestroyImmediate (instance);*/

		PersistentSample3D sample3D = ScriptableObject.CreateInstance<PersistentSample3D>();

		sample3D.setSample (NoiseGenerator.Perlin (octave));
		sample3D.setName (name);
		checkPath ();
		AssetDatabase.CreateAsset (sample3D, "Assets/ProcedureTerrain/Sample3D/"+name+".asset");

		EditorUtility.SetDirty(sample3D);
		
		AssetDatabase.SaveAssets ();
		/*EditorUtility.FocusProjectWindow ();
		Selection.activeObject = asset;*/
	}

	private void cancel(){
		this.Close ();
	}

	private void checkPath(){
		if(!AssetDatabase.IsValidFolder("Assets/ProcedureTerrain")){
			AssetDatabase.CreateFolder("Assets", "ProcedureTerrain");
		}
		if(!AssetDatabase.IsValidFolder("Assets/ProcedureTerrain/Sample3D")){
			AssetDatabase.CreateFolder("Assets/ProcedureTerrain", "Sample3D");
		}
	}

	/*private GameObject getGameObject(){
		GameObject instance = new GameObject();
		instance.AddComponent<PersistentSample3D> ();
		PersistentSample3D sample3D = instance.GetComponent<PersistentSample3D> ();
		float[,,] sample = NoiseGenerator.Perlin (octave);
		sample3D.setSample (sample);
		return instance;
	}*/
}
