using UnityEngine;
using System.Collections;

public class TerrainGenerator
{
	private int chunkSize = 5;
	private int horizontalChunks = 5;
	private int verticalChunks = 5;
	private Density density;
	private Vector3 position;

	public TerrainGenerator(Density density){
		this.density = density;
		position = new Vector3 (0f, 0f, 0f);
	}

	public TerrainGenerator (Density density, int horizontalChunks, int verticalChunks)
		:this(density)
	{
		this.horizontalChunks = horizontalChunks;
		this.verticalChunks = verticalChunks;
	}

	public TerrainGenerator(Density density, int horizontalChunks, int verticalChunks, int chunkSize)
		:this(density, horizontalChunks, verticalChunks)
	{
		this.chunkSize = chunkSize;
	}

	public void generate(){
		ChunkProcessor processor = new ChunkProcessor ();
		for (int i = 0; i < horizontalChunks; i++) {
			for (int j = 0; j < verticalChunks; j++) {
				for (int k = 0; k < horizontalChunks; k++) {
					Vector3 chunkPosition = new Vector3 (chunkSize*i+position.x,
					                                     chunkSize*j+position.y,
					                                     chunkSize*k+position.z);
					Chunk chunk = Chunk.Instantiate (chunkPosition);
					chunk.applyDensity (density);
					processor.process (chunk);
				}
			}
		}
	}

	public void setChunkSize(int size){
		chunkSize = size;
	}

	public void setPosition(float x, float y, float z){
		position.x = x;
		position.y = y;
		position.z = z;
	}

}

