using UnityEngine;
using System.Collections;
using UnityEditor;

public class FilterAssetWindow : EditorWindow {
	
	private float minHeight = 0;
	private float maxHeight = 0;
	
	private string name = "Filter";
	
	[MenuItem("Procedure Terrain/Create Filter/Vertical Flat Filter")]
	private static void defaultSaturationAssetWindow()
	{
		EditorWindow.GetWindow<FilterAssetWindow>(true, "Vertical Flat Filter Asset Creator");
	}
	
	void OnGUI(){
		name = EditorGUILayout.TextField ("Asset Name: ", name);
		minHeight = EditorGUILayout.FloatField ("MinHeight:", minHeight);
		maxHeight = EditorGUILayout.FloatField ("MaxHeight:", maxHeight);
		
		EditorGUILayout.BeginHorizontal ();
		{
			if (GUILayout.Button ("Accept")) {
				createAsset();
				this.Close ();
				EditorUtility.DisplayDialog ("Alert",  "Prefab "+name+".prefab has been created successfully.", "Accept");
			}
			if (GUILayout.Button ("Cancel")) {
				cancel();
			}
		}
		
	}
	
	private void createAsset(){		
		VerticalFlatNoiseFilter filter = ScriptableObject.CreateInstance<VerticalFlatNoiseFilter> ();
		filter.setHeightRange (minHeight, maxHeight);
		checkPath ();
		AssetDatabase.CreateAsset (filter, "Assets/ProcedureTerrain/Filter/"+name+".asset");
		
		AssetDatabase.SaveAssets ();
	}
	
	private void cancel(){
		this.Close ();
	}
	
	private void checkPath(){
		if(!AssetDatabase.IsValidFolder("Assets/ProcedureTerrain")){
			AssetDatabase.CreateFolder("Assets", "ProcedureTerrain");
		}
		if(!AssetDatabase.IsValidFolder("Assets/ProcedureTerrain/Filter")){
			AssetDatabase.CreateFolder("Assets/ProcedureTerrain", "Filter");
		}
	}
}
