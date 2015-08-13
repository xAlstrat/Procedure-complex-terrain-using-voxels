using UnityEngine;
using System.Collections;

public interface Density
{
	float apply (float x, float y, float z);
	float apply (Vector3 position);
}

