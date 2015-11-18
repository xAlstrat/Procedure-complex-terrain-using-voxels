using UnityEngine;
using System.Collections;
using UnityEditor;

public class SaturationAssetWindow : EditorWindow {
	
	private Vector2 range = new Vector2(-1f, 1f);
	
	private string name = "Saturation";
	
	[MenuItem("Procedure Terrain/Create Asset/Default Saturation")]
	private static void defaultSaturationAssetWindow()
	{
		EditorWindow.GetWindow<SaturationAssetWindow>(true, "Default Saturation Asset Creator");
	}
	
	void OnGUI(){
		name = EditorGUILayout.TextField ("Asset Name: ", name);
		range = EditorGUILayout.Vector2Field ("Valid Noise Range:", range);
		
		EditorGUILayout.BeginHorizontal ();
		{
			if (GUILayout.Button ("Accept")) {
				createSaturationAsset();
				this.Close ();
				EditorUtility.DisplayDialog ("Alert",  "Prefab "+name+".prefab has been created successfully.", "Accept");
			}
			if (GUILayout.Button ("Cancel")) {
				cancel();
			}
		}
		
	}
	
	private void createSaturationAsset(){		
		DefaultSaturation saturation = ScriptableObject.CreateInstance<DefaultSaturation> ();
		saturation.setLimits (range);
		checkPath ();
		AssetDatabase.CreateAsset (saturation, "Assets/ProcedureTerrain/Saturation/"+name+".asset");
		
		AssetDatabase.SaveAssets ();
	}
	
	private void cancel(){
		this.Close ();
	}
	
	private void checkPath(){
		if(!AssetDatabase.IsValidFolder("Assets/ProcedureTerrain")){
			AssetDatabase.CreateFolder("Assets", "ProcedureTerrain");
		}
		if(!AssetDatabase.IsValidFolder("Assets/ProcedureTerrain/Saturation")){
			AssetDatabase.CreateFolder("Assets/ProcedureTerrain", "Saturation");
		}
	}
}
