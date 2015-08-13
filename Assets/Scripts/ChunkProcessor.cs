using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Processes a chunk to generates its polygons with help of a marching cube.
/// </summary>
public class ChunkProcessor
{
	private static int chunkLenght = Chunk.LENGHT;
	private static int voxelCount = chunkLenght - 1;

	/// <summary>
	/// Holds the indeces for ALL the vertices created for this chunk
	/// 
	/// Every voxel can have from 0 to 5 vertices that we can reuse with neighbors.
	/// Here we'll hold all voxels with its 0 to 5 vertices inside a dictionary.
	/// 
	/// vertexIndex[edge] = vertex_index.
	/// </summary>
	private Dictionary<int, int>[,,] vertexIndex = new Dictionary<int, int> [voxelCount,voxelCount,voxelCount];

	/// <summary>
	/// Holds ALL vertices already created for the processed chunk.
	/// </summary>
	private List<Vector3> vertices = new List<Vector3>();

	/// <summary>
	/// The triangles.
	/// </summary>
	private List<int> triangles = new List<int>();

	/// <summary>
	/// The normals graph.
	/// 
	/// Every vertex (its index) has associated a list of normals of the three adyacents triangles.
	/// </summary>
	private Dictionary<int, List<Vector3>>  normalsGraph = new Dictionary<int, List<Vector3>> ();

	/// <summary>
	/// The chunk that will be processed.
	/// </summary>
	Chunk chunk;


	public ChunkProcessor(){
		for (int x=0; x<voxelCount; x++) {
			for (int y=0; y<voxelCount; y++) {
				for (int z=0; z<voxelCount; z++) {
					vertexIndex[x, y, z] = new Dictionary<int, int>();
				}
			}
		}
	}

	private void reset (){
		for (int x=0; x<voxelCount; x++) {
			for (int y=0; y<voxelCount; y++) {
				for (int z=0; z<voxelCount; z++) {
					vertexIndex[x, y, z] .Clear();
				}
			}
		}
		vertices.Clear ();
		triangles.Clear ();
		normalsGraph.Clear ();
		chunk = null;
	}

	public void process(Chunk chunk){
		this.chunk = chunk;
		MarchingCube marchingCube = MarchingCube.getInstance();
		float[,,] density = chunk.getDensity();

		for(int x=0; x<voxelCount; x++){
			for(int y=0; y<voxelCount; y++){
				for(int z=0; z<voxelCount; z++){
					marchingCube.march(x, y, z, density, this);
				}
			}
		}
		generateMesh ();
		reset ();
	}

	private void generateMesh(){
		Vector3[] mesh_vertices = new Vector3[vertices.Count];
		Vector3[] mesh_normals = new Vector3[vertices.Count];
		for(int c = 0; c < vertices.Count; c++){
			mesh_vertices[c] = vertices[c];
			mesh_normals[c] = calculateVertexNormal(c);
		}

		int[] mesh_triangles = new int[triangles.Count];
		for(int c = 0; c < triangles.Count; c++){
			mesh_triangles[c] = triangles[c];
		}

		Vector2[] uvs = new Vector2[vertices.Count];
		
		for (int i=0; i < uvs.Length; i++) {
			uvs[i] = new Vector2(mesh_vertices[i].x, mesh_vertices[i].z);
		}

		chunk.mesh.vertices = mesh_vertices;
		chunk.mesh.triangles = mesh_triangles;
		chunk.mesh.normals = mesh_normals;
		chunk.mesh.uv = uvs;
		chunk.mesh.Optimize ();
	}

	public bool existVertex(int x, int y, int z, int edge){
		if (x < 0 || y < 0 || z < 0 || x >= voxelCount || y >= voxelCount || z >= voxelCount) {
			return false;
		}
		return vertexIndex [x, y, z].ContainsKey (edge);
	}

	public void setVertexIndex (int x, int y, int z, int edge, int index){
		vertexIndex [x, y, z].Add (edge, index);
	}

	public void addVertexToGraph(int index){
		normalsGraph.Add (index, new List<Vector3> ());
	}

	public int getVertexIndex(int x, int y, int z, int edge){
		return vertexIndex [x, y, z][edge];
	}

	public List<Vector3> getVertices(){
		return vertices;
	}

	public Dictionary<int, int>[,,] getIndeces(){
		return vertexIndex;
	}

	public List<int> getTriangles(){
		return triangles;
	}

	public Chunk getChunk(){
		return chunk;
	}

	public void addTriangle(int v1, int v2, int v3){
		triangles.Add (v1);
		triangles.Add (v2);
		triangles.Add (v3);
		Vector3 normal = calculateTriangleNormal (v1, v2, v3);
		normalsGraph [v1].Add (normal);
		normalsGraph [v2].Add (normal);
		normalsGraph [v3].Add (normal);
	}

	private Vector3 calculateTriangleNormal(int v1, int v2, int v3){
		return Vector3.Normalize (Vector3.Cross (vertices [v2] - vertices [v1], vertices [v3] - vertices [v1]));
	}

	private Vector3 calculateVertexNormal(int vertex){
		Vector3 normal = normalsGraph[vertex][0];
		for(int c = 1; c < normalsGraph[vertex].Count; c++){
			normal.x += normalsGraph[vertex][c].x;
			normal.y += normalsGraph[vertex][c].y;
			normal.z += normalsGraph[vertex][c].z;
		}
		return Vector3.Normalize (normal);
	}

}

