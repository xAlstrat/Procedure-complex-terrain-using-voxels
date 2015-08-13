using UnityEngine;
using System.Collections;

public class DefaultInterpolator : Interpolator
{
	private NoiseSample noiseSample;

	public DefaultInterpolator(){
	}

	public float interpolateAt(NoiseSample sample, float x, float y, float z){
		this.noiseSample = sample;
		int X0 = fastFloor(x) % (sample.sample.GetLength(0)-1);
		int Y0 = fastFloor(y) % (sample.sample.GetLength(1)-1);
		int Z0 = fastFloor(z) % (sample.sample.GetLength(2)-1);
		x -= fastFloor (x);
		y -= fastFloor (y);
		z -= fastFloor (z);
		float	u = fade (x),
				v = fade (y),
				w = fade (z);
		float interpolated = interpolateCube (X0, Y0, Z0, u, v, w);
		return interpolated;

	}

	private static float interpolate(float v1, float v2, float fade){
		return v1 * (1 - fade) + v2 * fade;
	}

	private float interpolateCube(int i, int j, int k, float fx, float fy, float fz){
		float[,,] sample = noiseSample.sample;
		float bottom = interpolate (interpolate (sample [i, j, k], sample [i + 1, j, k], fx),
		                           interpolate (sample [i, j, k + 1], sample [i + 1, j, k + 1], fx),
		                           fz);
		float top = interpolate (interpolate (sample [i, j + 1, k], sample [i + 1, j + 1, k], fx),
		                            interpolate (sample [i, j + 1, k + 1], sample [i + 1, j + 1, k + 1], fx),
		                            fz);
		return interpolate (bottom, top, fy) * 2 - 1;
	}

	private static int fastFloor(float val){
		return (val >= 0) ? (int)val : (int)val - 1;
	}

	private static float fade(float t) {
		return t * t * t * (t * (t * 6 - 15) + 10);
	}

}

