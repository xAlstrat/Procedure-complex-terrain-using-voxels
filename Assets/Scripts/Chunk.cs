using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Chunk.
/// </summary>
[RequireComponent (typeof (MeshRenderer))]
[RequireComponent (typeof (MeshFilter))]
public class Chunk : MonoBehaviour
{
	/// <summary>
	/// Chunk size.
	/// </summary>
	public static int SIZE = 3;

	/// <summary>
	/// Quantity of voxels per unit (an unit should have VOXELS_PER_UNIT+1 vertices);
	/// </summary>
	private static int VOXELS_PER_UNIT = 30;

	/// <summary>
	/// Number of vertices per side in the chunk.
	/// </summary>
	public static int LENGHT = SIZE * VOXELS_PER_UNIT + 1;

	/// <summary>
	/// Size of the side of a voxel.
	/// </summary>
	public static float DELTA = 1.0f / VOXELS_PER_UNIT;

	/// <summary>
	/// The chunk world position.
	/// </summary>
	private Vector3 position;

	/// <summary>
	/// The unit cubes of the chunk.
	/// </summary>
	private Cube[][][] cubes;

	/// <summary>
	/// The density of the volume in this chunk.
	/// </summary>
	private float[,,] chunkDensity = new float[LENGHT,LENGHT,LENGHT];

	/// <summary>
	/// The terrain mesh for this chunk.
	/// </summary>
	[HideInInspector]
	public Mesh mesh;

	/// <summary>
	/// Instantiates a chunk in the specified position.
	/// </summary>
	/// <param name="_parent">_parent.</param>
	/// <param name="_position">_position.</param>
	public static Chunk Instantiate(Vector3 _position){
		GameObject instance = new GameObject("Chunk ("+_position.x+", "+_position.y+", "+_position.z+")");
		instance.AddComponent<Chunk> ();
		Chunk chunkInstance = instance.GetComponent<Chunk> ();
		chunkInstance.mesh = instance.GetComponent<MeshFilter> ().mesh;
		Transform parent = GameObject.Find ("VoxelsTerrain").transform;
		Renderer renderer = chunkInstance.GetComponent<Renderer> ();
		renderer.material = (Material)Resources.Load("Earth", typeof(Material));
		chunkInstance.init (parent, _position);
		return chunkInstance;
	}

	public static void setSize(int size){
		SIZE = size;
		LENGHT = SIZE * VOXELS_PER_UNIT + 1;
	}

	public static void setDiscretization(int voxelsPerUnit){
		VOXELS_PER_UNIT = voxelsPerUnit;
		LENGHT = SIZE * VOXELS_PER_UNIT + 1;
		DELTA = 1.0f / VOXELS_PER_UNIT;
	}

	private void init(Transform _parent, Vector3 _position){
		transform.parent = _parent;
		transform.position = _position;
		initCubes ();
	}

	/// <summary>
	/// Initializes the unit cubes inside the chunk.
	/// </summary>
	private void initCubes(){
		int length = SIZE;
		cubes = new Cube[length][][];
		for (int x=0; x<length; x++) {
			cubes[x] = new Cube[length][];
			for (int y=0; y<length; y++) {
				cubes[x][y] = new Cube[length];
				for (int z=0; z<length; z++) {
					cubes[x][y][z] = new Cube(x, y, z);
				}
			}
		}
	}

	public float[,,] getDensity(){
		return chunkDensity;
	}

	public float getDensityAt(int x, int y, int z){
		return chunkDensity [x, y ,z];
	}

	public void setDensityAt(int x, int y, int z, float d){
		chunkDensity [x, y ,z] = d;
	}

	public void applyDensity(Density density){
		float xPos, yPos, zPos;
		Vector3 absPos = transform.position;
		for (int x=0; x<LENGHT; x++) {
			xPos = x*DELTA;
			for (int y=0; y<LENGHT; y++) {
				yPos = y*DELTA;
				for (int z=0; z<LENGHT; z++) {
					zPos = z*DELTA;
					setDensityAt(x, y, z, density.apply(xPos+absPos.x, yPos+absPos.y, zPos+absPos.z));
				}
			}
		}
	}
}

