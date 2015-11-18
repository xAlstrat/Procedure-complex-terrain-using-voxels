using UnityEngine;
using System.Collections;

/// <summary>
/// A 3D sample interpolator.
/// </summary>
public interface Interpolator
{
	/// <summary>
	/// Returns the interpolated noise at the specified position given a noise sample.
	/// </summary>
	/// <returns>The <see cref="System.Single"/>.</returns>
	/// <param name="sample">Sample.</param>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	float interpolateAt(NoiseSample sample, float x, float y, float z);

}

