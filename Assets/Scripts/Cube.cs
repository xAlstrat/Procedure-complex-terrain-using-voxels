using UnityEngine;
using System.Collections;

public class Cube
{
	public static readonly int SIZE;
	private Vector3 position;

	public Cube(int x, int y, int z){
		position = new Vector3 (x, y, z);
	}
}

