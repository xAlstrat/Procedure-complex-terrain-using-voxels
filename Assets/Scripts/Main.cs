using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{

	[Header("   Sample Configuration")]
	//public Vector3 firstSampleScale = new Vector3 (1f, 1f, 1f);
	//public Vector3 secondSampleScale = new Vector3 (1f, 1f, 1f);
	[Range(0.0f, 100.0f)]
	public float noiseScale = 1;
	//public float weightFirstSample = 0.8f;
	//public float weightSecondSample = 0.2f;
	
	//public Vector2 noiseSaturation1 = new Vector2(-1, 1);
	//public Vector2 noiseSaturation2 = new Vector2(-1, 1);

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

	[Header("   Sample List")]
	public NoiseSample[] noiseSamples;


	// Use this for initialization
	void Start ()
	{
		Chunk.setSize (chunkSize);
		Chunk.setDiscretization (voxelsPerUnit);
		NoiseSampleManager sampleManager = new NoiseSampleManager (noiseScale);
		NoisedFlatDensity density = new NoisedFlatDensity (height, sampleManager);

		foreach (NoiseSample noiseSample in noiseSamples) {
			SampleRepository.registerSample (noiseSample.getPersistentSample ());
			sampleManager.addSample (noiseSample);
		}

		TerrainGenerator generator = new TerrainGenerator (density, horizontalChunks, verticalChunks);
		generator.generate ();

		/*NoiseSample noiseSample1 = new NoiseSample (
			SampleRepository.getSample ("sample1"), weightFirstSample,
			new DefaultSaturation (noiseSaturation1.x, noiseSaturation1.y), firstSampleScale);

		NoiseSample noiseSample2 = new NoiseSample (
			SampleRepository.getSample ("sample2"), weightSecondSample,
			new DefaultSaturation (noiseSaturation2.x, noiseSaturation2.y), secondSampleScale);*/



		/*ChunkProcessor processor = new ChunkProcessor ();
		for (int i = 0; i < horizontalChunks; i++) {
			for (int j = 0; j < verticalChunks; j++) {
				for (int k = 0; k < horizontalChunks; k++) {
					Chunk chunk = Chunk.Instantiate (new Vector3 (chunkSize*i, chunkSize*j, chunkSize*k));
					chunk.applyDensity (density);
					processor.process (chunk);
				}
			}
		}*/

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

