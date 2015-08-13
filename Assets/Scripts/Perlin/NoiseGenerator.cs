using UnityEngine;
using System.Collections;

public class NoiseGenerator
{
	private static int SIZE_X = 50;
	private static int SIZE_Y = 50;
	private static int SIZE_Z = 50;

	public static void setSampleSize(Vector3 size){
		SIZE_X = (int)size.x;
		SIZE_Y = (int)size.y;
		SIZE_Z = (int)size.z;
	}

	public static float[,,] generateBaseNoise(){
		float[,,] noise = new float[SIZE_X, SIZE_Y, SIZE_Z];
		
		for (int i = 0; i < SIZE_X; i++)
			for (int j = 0; j < SIZE_Y; j++)
				for (int k = 0; k < SIZE_Z; k++)
					noise[i, j, k] = Random.Range(0, 2);

		return noise;
	}

	private static float interpolate(float x0, float x1, float alpha)
	{
		return x0 * (1 - alpha) + alpha * x1;
	}

	public static float[,,] generateSmoothNoise(float[,,] baseNoise, int octave){
		float[,,] smoothNoise = new float[SIZE_X, SIZE_Y, SIZE_Z];
		
		int samplePeriod = 1 << octave; // calculates 2 ^ k
		float sampleFrequency = 1.0f / samplePeriod;
		
		for (int i = 0; i < SIZE_X; i++)
		{
			//calculate the horizontal sampling indices
			int sample_i0 = (i / samplePeriod) * samplePeriod;
			int sample_i1 = (sample_i0 + samplePeriod) % SIZE_X; //wrap around
			float i_blend = (i - sample_i0) * sampleFrequency;
			
			for (int j = 0; j < SIZE_Y; j++)
			{
				//calculate the vertical sampling indices
				int sample_j0 = (j / samplePeriod) * samplePeriod;
				int sample_j1 = (sample_j0 + samplePeriod) % SIZE_Y; //wrap around
				float j_blend = (j - sample_j0) * sampleFrequency;

				for (int k = 0; k < SIZE_Z; k++)
				{
					//calculate the vertical sampling indices
					int sample_k0 = (k / samplePeriod) * samplePeriod;
					int sample_k1 = (sample_k0 + samplePeriod) % SIZE_Z; //wrap around
					float k_blend = (k - sample_k0) * sampleFrequency;
					
					//blend the top two corners
					float top =
						interpolate(
							interpolate(
								baseNoise[sample_i0, sample_j1, sample_k0],
							    baseNoise[sample_i1, sample_j1, sample_k0], i_blend),
							interpolate(
								baseNoise[sample_i0, sample_j1, sample_k1],
					            baseNoise[sample_i1, sample_j1, sample_k1], i_blend), k_blend);
					
					//blend the bottom two corners
					float bottom = 
						interpolate(
							interpolate(
								baseNoise[sample_i0, sample_j0, sample_k0],
								baseNoise[sample_i1, sample_j0, sample_k0], i_blend),
							interpolate(
								baseNoise[sample_i0, sample_j0, sample_k1],
								baseNoise[sample_i1, sample_j0, sample_k1], i_blend), k_blend);
					
					//final blend
					smoothNoise[i, j, k] = interpolate(bottom, top, j_blend);
				}
			}
		}
		
		return smoothNoise;
	}

	private static float[,,] generatePerlinNoise(float[,,] baseNoise, int octaveCount){
		
		float[][,,] smoothNoise = new float[octaveCount][,,]; //an array of 3D arrays containing
		
		float persistance = 0.5f;
		
		//generate smooth noise
		for (int i = 0; i < octaveCount; i++)
			smoothNoise[i] = generateSmoothNoise(baseNoise, i);
		
		float[,,] perlinNoise = new float[SIZE_X, SIZE_Y, SIZE_Z];
		float amplitude = 1.0f;
		float totalAmplitude = 0.0f;
		
		//blend noise together
		for (int octave = octaveCount - 1; octave >= 0; octave--)
		{
			amplitude *= persistance;
			totalAmplitude += amplitude;
			
			for (int i = 0; i < SIZE_X; i++)
				for (int j = 0; j < SIZE_Y; j++)
					for (int k = 0; k < SIZE_Z; k++)
						perlinNoise[i, j, k] += smoothNoise[octave][i, j, k] * amplitude;

		}
		
		//normalisation
		for (int i = 0; i < SIZE_X; i++)
			for (int j = 0; j < SIZE_Y; j++)
				for (int k = 0; k < SIZE_Z; k++)
				perlinNoise[i, j, k] /= totalAmplitude;

		
		return perlinNoise;
	}

	public static float[,,] Perlin(int octave){
		return generatePerlinNoise (generateBaseNoise (), octave);
	}
}

