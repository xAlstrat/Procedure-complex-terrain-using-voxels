using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{
	[Header("   Sample Configuration")]
	public Vector3 sampleSize = new Vector3(50, 50, 50);
	public Vector3 firstSampleScale = new Vector3 (1f, 1f, 1f);
	public Vector3 secondSampleScale = new Vector3 (1f, 1f, 1f);
	[Range(0.0f, 100.0f)]
	public float noiseScale = 1;

	[Range(1, 10)]
	public int firstSampleOctaves = 1;

	[Range(1, 10)]
	public int secondSampleOctaves = 1;

	public float weightFirstSample = 0.8f;
	public float weightSecondSample = 0.2f;

	[Range(-1.0f, 1.0f)]
	public float noiseSaturation1 = 0f;
	[Range(-1.0f, 1.0f)]
	public float noiseSaturation2 = 0f;

	public float height = 3f;

	[Header("   Chunk/Unit Configuration")]
	[Range(1, 50)]
	public int chunkSize = 3;
	[Range(1, 1000)]
	public int voxelsPerUnit = 30;
	[Range(1, 1000)]
	public int horizontalChunks = 2;
	[Range(1, 1000)]
	public int verticalChunks = 2;


	// Use this for initialization
	void Start ()
	{
		NoiseGenerator.setSampleSize (sampleSize);
		Chunk.setSize (chunkSize);
		Chunk.setDiscretization (voxelsPerUnit);

		SampleRepository.registerSample ("sample1", NoiseGenerator.Perlin (firstSampleOctaves));
		SampleRepository.registerSample ("sample2", NoiseGenerator.Perlin (secondSampleOctaves));

		NoiseSample noiseSample1 = new NoiseSample (
			SampleRepository.getSample ("sample1"), weightFirstSample,
			new DefaultSaturation (noiseSaturation1, 1f), firstSampleScale);

		NoiseSample noiseSample2 = new NoiseSample (
			SampleRepository.getSample ("sample2"), weightSecondSample,
			new DefaultSaturation (noiseSaturation2, 1f), secondSampleScale);


		NoiseSampleManager sampleManager = new NoiseSampleManager (noiseScale);
		sampleManager.addSample (noiseSample1);
		sampleManager.addSample (noiseSample2);

		NoisedFlatDensity density = new NoisedFlatDensity (height, sampleManager);

		ChunkProcessor processor = new ChunkProcessor ();
		for (int i = 0; i < horizontalChunks; i++) {
			for (int j = 0; j < verticalChunks; j++) {
				for (int k = 0; k < horizontalChunks; k++) {
					Chunk chunk = Chunk.Instantiate (new Vector3 (chunkSize*i, chunkSize*j, chunkSize*k));
					chunk.applyDensity (density);
					processor.process (chunk);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

