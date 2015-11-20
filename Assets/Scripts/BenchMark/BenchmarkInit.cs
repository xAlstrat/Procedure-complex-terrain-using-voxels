using UnityEngine;
using System.Collections;

public class BenchmarkInit : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		Benchmark benchmark = new SignBenchmark("Sign");
		benchmark.runBenchmark (1);
	}
}

