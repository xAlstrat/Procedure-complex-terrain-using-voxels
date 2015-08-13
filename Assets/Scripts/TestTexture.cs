using UnityEngine;
using System.Collections;

public class TestTexture : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		NoiseGenerator.setSampleSize (new Vector3(100, 100, 20));
		float[,,] noise = NoiseGenerator.Perlin(7);
		
		Texture2D texture = new Texture2D(100, 100);
		Renderer renderer = GetComponent<Renderer>();

		int mipCount = Mathf.Min(3, texture.mipmapCount);
		for( int mip = 0; mip < mipCount; ++mip ) {
			Color[] cols = texture.GetPixels( mip );
			for( int i = 0; i < cols.Length; ++i ) {
				float val = noise[i%100, i/100, 10];
				//Debug.Log(val);
				cols[i] = new Color(val, val, val);
			}
			texture.SetPixels( cols, mip );
		}
		// actually apply all SetPixels, don't recalculate mip levels
		texture.Apply(false);
		renderer.material.mainTexture = texture;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

